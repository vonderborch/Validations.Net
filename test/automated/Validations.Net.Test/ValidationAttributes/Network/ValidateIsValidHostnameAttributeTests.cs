namespace Validations.Net.Test.ValidationAttributes.Network;

[TestFixture]
public class ValidateIsValidHostnameAttributeTests
{
    [Test]
    public void Validate_WithValidHostname_ReturnsSuccess()
    {
        var attr = new ValidateIsValidHostnameAttribute();
        var result = attr.Validate("example.com");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithInvalidHostname_ReturnsFailure()
    {
        var attr = new ValidateIsValidHostnameAttribute();
        var result = attr.Validate("---");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithSubdomain_ReturnsSuccess()
    {
        var attr = new ValidateIsValidHostnameAttribute();
        var result = attr.Validate("www.example.com");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsValidHostnameAttribute { Message = "Must be valid hostname" };
        var result = attr.Validate("---");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must be valid hostname"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsValidHostnameAttribute();
        var result = attr.Validate("---", "Host");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Host"));
    }
}
