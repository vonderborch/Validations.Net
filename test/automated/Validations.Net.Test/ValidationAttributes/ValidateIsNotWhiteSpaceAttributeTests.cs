using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsNotWhiteSpaceAttributeTests
{
    [Test]
    public void Validate_WithNonWhitespaceString_ReturnsSuccess()
    {
        var attr = new ValidateIsNotWhiteSpaceAttribute();
        var result = attr.Validate("hello");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithNull_ReturnsSuccess()
    {
        var attr = new ValidateIsNotWhiteSpaceAttribute();
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithWhitespaceOnlyString_ReturnsFailure()
    {
        var attr = new ValidateIsNotWhiteSpaceAttribute();
        var result = attr.Validate("   ");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsNotWhiteSpaceAttribute { Message = "Must not be whitespace" };
        var result = attr.Validate("   ");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must not be whitespace"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateIsNotWhiteSpaceAttribute();
        var result = attr.Validate("   ", "MyProperty");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("MyProperty"));
    }
}
