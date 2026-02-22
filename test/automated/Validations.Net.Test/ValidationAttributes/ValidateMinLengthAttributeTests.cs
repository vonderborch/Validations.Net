namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateMinLengthAttributeTests
{
    [Test]
    public void Validate_WithStringOfSufficientLength_ReturnsSuccess()
    {
        var attr = new ValidateMinLengthAttribute(3);
        var result = attr.Validate("abc");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithStringExceedingMinLength_ReturnsSuccess()
    {
        var attr = new ValidateMinLengthAttribute(3);
        var result = attr.Validate("abcd");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithStringTooShort_ReturnsFailure()
    {
        var attr = new ValidateMinLengthAttribute(5);
        var result = attr.Validate("abc");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateMinLengthAttribute(5) { Message = "At least 5 characters" };
        var result = attr.Validate("ab");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("At least 5 characters"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateMinLengthAttribute(5);
        var result = attr.Validate("ab", "Text");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Text"));
    }
}
