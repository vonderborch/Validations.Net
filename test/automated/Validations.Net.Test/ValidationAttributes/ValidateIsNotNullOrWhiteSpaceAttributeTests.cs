using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsNotNullOrWhiteSpaceAttributeTests
{
    [Test]
    public void Validate_WithNonWhitespaceString_ReturnsSuccess()
    {
        var attr = new ValidateIsNotNullOrWhiteSpaceAttribute();
        var result = attr.Validate("hello");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithNull_ReturnsFailure()
    {
        var attr = new ValidateIsNotNullOrWhiteSpaceAttribute();
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithWhitespaceString_ReturnsFailure()
    {
        var attr = new ValidateIsNotNullOrWhiteSpaceAttribute();
        var result = attr.Validate("   ");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsNotNullOrWhiteSpaceAttribute { Message = "Required non-whitespace" };
        var result = attr.Validate("   ");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Required non-whitespace"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateIsNotNullOrWhiteSpaceAttribute();
        var result = attr.Validate(null, "MyProperty");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("MyProperty"));
    }
}
