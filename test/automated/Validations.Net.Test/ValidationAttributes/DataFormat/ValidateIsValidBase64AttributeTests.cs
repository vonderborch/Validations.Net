using Validations.Net.Validators.DataFormat;

namespace Validations.Net.Test.ValidationAttributes.DataFormat;

[TestFixture]
public class ValidateIsValidBase64AttributeTests
{
    [Test]
    public void Validate_WithValidBase64_ReturnsSuccess()
    {
        var attr = new ValidateIsValidBase64Attribute();
        var result = attr.Validate(Convert.ToBase64String("Hello"u8.ToArray()));
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithInvalidBase64_ReturnsFailure()
    {
        var attr = new ValidateIsValidBase64Attribute();
        var result = attr.Validate("!!not-base64!!");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNull_ReturnsFailure()
    {
        var attr = new ValidateIsValidBase64Attribute();
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsValidBase64Attribute { Message = "Must be base64" };
        var result = attr.Validate("!!!");
        Assert.That(result.ExceptionMessage, Does.Contain("Must be base64"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateIsValidBase64Attribute();
        var result = attr.Validate("!!!", "Data");
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Data"));
    }
}
