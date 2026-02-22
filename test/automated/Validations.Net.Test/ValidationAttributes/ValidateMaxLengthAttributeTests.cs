namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateMaxLengthAttributeTests
{
    [Test]
    public void Validate_WithStringWithinLimit_ReturnsSuccess()
    {
        var attr = new ValidateMaxLengthAttribute(5);
        var result = attr.Validate("abc");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithStringAtExactLimit_ReturnsSuccess()
    {
        var attr = new ValidateMaxLengthAttribute(5);
        var result = attr.Validate("abcde");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithStringTooLong_ReturnsFailure()
    {
        var attr = new ValidateMaxLengthAttribute(5);
        var result = attr.Validate("abcdef");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateMaxLengthAttribute(3) { Message = "Max 3 characters" };
        var result = attr.Validate("abcd");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Max 3 characters"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateMaxLengthAttribute(3);
        var result = attr.Validate("abcd", "Code");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Code"));
    }
}
