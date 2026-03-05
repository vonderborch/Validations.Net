using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsLengthAttributeTests
{
    [Test]
    public void Validate_WithStringOfExactLength_ReturnsSuccess()
    {
        var attr = new ValidateIsLengthAttribute(3);
        var result = attr.Validate("abc");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithStringOfDifferentLength_ReturnsFailure()
    {
        var attr = new ValidateIsLengthAttribute(3);
        var result = attr.Validate("ab");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCollectionOfExactCount_ReturnsSuccess()
    {
        var attr = new ValidateIsLengthAttribute(3);
        var result = attr.Validate(new[] { 1, 2, 3 });
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsLengthAttribute(3) { Message = "Must be exactly 3 chars" };
        var result = attr.Validate("ab");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must be exactly 3 chars"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateIsLengthAttribute(3);
        var result = attr.Validate("ab", "Text");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Text"));
    }
}
