using Validations.Net.Validators.DataFormat;

namespace Validations.Net.Test.ValidationAttributes.DataFormat;

[TestFixture]
public class ValidateIsValidEmailAttributeTests
{
    [Test]
    public void Validate_WithValidEmail_ReturnsSuccess()
    {
        var attr = new ValidateIsValidEmailAttribute();
        var result = attr.Validate("user@example.com");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithInvalidEmail_ReturnsFailure()
    {
        var attr = new ValidateIsValidEmailAttribute();
        var result = attr.Validate("not-an-email");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNull_ReturnsFailure()
    {
        var attr = new ValidateIsValidEmailAttribute();
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsValidEmailAttribute { Message = "Invalid email" };
        var result = attr.Validate("bad");
        Assert.That(result.ExceptionMessage, Does.Contain("Invalid email"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateIsValidEmailAttribute();
        var result = attr.Validate("bad", "Email");
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Email"));
    }
}
