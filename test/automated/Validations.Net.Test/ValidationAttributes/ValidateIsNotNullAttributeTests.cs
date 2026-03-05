using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsNotNullAttributeTests
{
    [Test]
    public void Validate_WithNonNullValue_ReturnsSuccess()
    {
        var attr = new ValidateIsNotNullAttribute();
        var result = attr.Validate("hello");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithNullValue_ReturnsFailure()
    {
        var attr = new ValidateIsNotNullAttribute();
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsNotNullAttribute { Message = "Custom error" };
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Custom error"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateIsNotNullAttribute();
        var result = attr.Validate(null, "MyProperty");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("MyProperty"));
    }
}
