using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsNotSingleAttributeTests
{
    [Test]
    public void Validate_WithEmptyString_ReturnsSuccess()
    {
        var attr = new ValidateIsNotSingleAttribute();
        var result = attr.Validate("");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCollectionOfTwo_ReturnsSuccess()
    {
        var attr = new ValidateIsNotSingleAttribute();
        var result = attr.Validate(new[] { 1, 2 });
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithStringOfLengthOne_ReturnsFailure()
    {
        var attr = new ValidateIsNotSingleAttribute();
        var result = attr.Validate("x");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsNotSingleAttribute { Message = "Must not have exactly one" };
        var result = attr.Validate("x");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must not have exactly one"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateIsNotSingleAttribute();
        var result = attr.Validate("x", "MyProperty");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("MyProperty"));
    }
}
