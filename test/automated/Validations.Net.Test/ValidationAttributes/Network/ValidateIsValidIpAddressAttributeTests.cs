namespace Validations.Net.Test.ValidationAttributes.Network;

[TestFixture]
public class ValidateIsValidIpAddressAttributeTests
{
    [Test]
    public void Validate_WithValidIpv4_ReturnsSuccess()
    {
        var attr = new ValidateIsValidIpAddressAttribute();
        var result = attr.Validate("192.168.1.1");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithInvalidIp_ReturnsFailure()
    {
        var attr = new ValidateIsValidIpAddressAttribute();
        var result = attr.Validate("not.an.ip");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithValidIpv6_ReturnsSuccess()
    {
        var attr = new ValidateIsValidIpAddressAttribute();
        var result = attr.Validate("::1");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsValidIpAddressAttribute { Message = "Must be valid IP" };
        var result = attr.Validate("invalid");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must be valid IP"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsValidIpAddressAttribute();
        var result = attr.Validate("invalid", "Address");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Address"));
    }
}
