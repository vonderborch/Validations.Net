using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateDoesNotContainAnyAttributeTests
{
    [Test]
    public void Validate_WithStringContainingNoneOfValues_ReturnsSuccess()
    {
        var attr = new ValidateDoesNotContainAnyAttribute("apple", "banana", "cherry");
        var result = attr.Validate("I like grapes");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithStringContainingOneOfValues_ReturnsFailure()
    {
        var attr = new ValidateDoesNotContainAnyAttribute("apple", "banana", "cherry");
        var result = attr.Validate("I like banana");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNull_ReturnsFailure()
    {
        var attr = new ValidateDoesNotContainAnyAttribute("a", "b");
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNonStringEnumerable_ReturnsFailure()
    {
        var attr = new ValidateDoesNotContainAnyAttribute("2", "3");
        var result = attr.Validate(new[] { 1, 2, 3 });
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateDoesNotContainAnyAttribute("bad", "evil") { Message = "Must not contain bad words" };
        var result = attr.Validate("bad word");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must not contain bad words"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateDoesNotContainAnyAttribute("bad", "evil");
        var result = attr.Validate("bad", "Text");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Text"));
    }
}
