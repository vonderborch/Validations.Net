namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsNullOrWhiteSpaceAttributeTests
{
    [Test]
    public void Validate_WithNull_ReturnsSuccess()
    {
        var attr = new ValidateIsNullOrWhiteSpaceAttribute();
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithEmptyString_ReturnsSuccess()
    {
        var attr = new ValidateIsNullOrWhiteSpaceAttribute();
        var result = attr.Validate("");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithWhitespaceString_ReturnsSuccess()
    {
        var attr = new ValidateIsNullOrWhiteSpaceAttribute();
        var result = attr.Validate("   ");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithNonWhitespaceString_ReturnsFailure()
    {
        var attr = new ValidateIsNullOrWhiteSpaceAttribute();
        var result = attr.Validate("hello");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsNullOrWhiteSpaceAttribute { Message = "Must be null or whitespace" };
        var result = attr.Validate("x");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must be null or whitespace"));
    }
}
