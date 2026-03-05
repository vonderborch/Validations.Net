using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsWhiteSpaceAttributeTests
{
    [Test]
    public void Validate_WithWhitespaceOnlyString_ReturnsSuccess()
    {
        var attr = new ValidateIsWhiteSpaceAttribute();
        var result = attr.Validate("   \t\n");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithEmptyString_ReturnsFailure()
    {
        var attr = new ValidateIsWhiteSpaceAttribute();
        var result = attr.Validate("");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNonWhitespaceString_ReturnsFailure()
    {
        var attr = new ValidateIsWhiteSpaceAttribute();
        var result = attr.Validate("hello");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsWhiteSpaceAttribute { Message = "Must be whitespace only" };
        var result = attr.Validate("x");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must be whitespace only"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateIsWhiteSpaceAttribute();
        var result = attr.Validate("x", "MyProperty");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("MyProperty"));
    }
}
