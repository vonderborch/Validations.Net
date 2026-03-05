using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsNotLengthAttributeTests
{
    [Test]
    public void Validate_WithStringOfDifferentLength_ReturnsSuccess()
    {
        var attr = new ValidateIsNotLengthAttribute(3);
        var result = attr.Validate("ab");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithStringOfExactLength_ReturnsFailure()
    {
        var attr = new ValidateIsNotLengthAttribute(3);
        var result = attr.Validate("abc");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNull_ReturnsSuccess()
    {
        var attr = new ValidateIsNotLengthAttribute(3);
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsNotLengthAttribute(3) { Message = "Must not be 3 chars" };
        var result = attr.Validate("abc");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must not be 3 chars"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateIsNotLengthAttribute(3);
        var result = attr.Validate("abc", "Text");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Text"));
    }
}
