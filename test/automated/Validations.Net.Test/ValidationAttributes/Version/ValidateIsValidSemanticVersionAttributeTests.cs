namespace Validations.Net.Test.ValidationAttributes.Version;

[TestFixture]
public class ValidateIsValidSemanticVersionAttributeTests
{
    [Test]
    public void Validate_WithValidSemVer_ReturnsSuccess()
    {
        var attr = new ValidateIsValidSemanticVersionAttribute();
        var result = attr.Validate("1.2.3");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithInvalidSemVer_ReturnsFailure()
    {
        var attr = new ValidateIsValidSemanticVersionAttribute();
        var result = attr.Validate("abc");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithPrereleaseVersion_ReturnsSuccess()
    {
        var attr = new ValidateIsValidSemanticVersionAttribute();
        var result = attr.Validate("1.0.0-alpha");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsValidSemanticVersionAttribute { Message = "Must be semver" };
        var result = attr.Validate("abc");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must be semver"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsValidSemanticVersionAttribute();
        var result = attr.Validate("abc", "Version");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Version"));
    }
}
