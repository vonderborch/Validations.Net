using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsNullAttributeTests
{
    [Test]
    public void Validate_WithNullValue_ReturnsSuccess()
    {
        var attr = new ValidateIsNullAttribute();
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithNonNullValue_ReturnsFailure()
    {
        var attr = new ValidateIsNullAttribute();
        var result = attr.Validate("hello");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsNullAttribute { Message = "Custom error" };
        var result = attr.Validate("hello");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Custom error"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateIsNullAttribute();
        var result = attr.Validate("hello", "MyProperty");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("MyProperty"));
    }
}
