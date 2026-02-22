namespace Validations.Net.Test.ValidationAttributes.Security;

[TestFixture]
public class ValidateIsSecurePasswordAttributeTests
{
    [Test]
    public void Validate_WithSecurePassword_ReturnsSuccess()
    {
        var attr = new ValidateIsSecurePasswordAttribute();
        var result = attr.Validate("P@ssw0rd!");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithWeakPassword_ReturnsFailure()
    {
        var attr = new ValidateIsSecurePasswordAttribute();
        var result = attr.Validate("weak");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithPasswordMissingSpecialChar_ReturnsFailure()
    {
        var attr = new ValidateIsSecurePasswordAttribute();
        var result = attr.Validate("Password1");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsSecurePasswordAttribute { Message = "Password too weak" };
        var result = attr.Validate("weak");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Password too weak"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsSecurePasswordAttribute();
        var result = attr.Validate("weak", "Password");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Password"));
    }
}
