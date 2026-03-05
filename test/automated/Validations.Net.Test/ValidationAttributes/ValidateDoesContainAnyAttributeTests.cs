using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateDoesContainAnyAttributeTests
{
    [Test]
    public void Validate_WithStringContainingOneOfValues_ReturnsSuccess()
    {
        var attr = new ValidateDoesContainAnyAttribute("apple", "banana", "cherry");
        var result = attr.Validate("I like banana");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithStringContainingNoneOfValues_ReturnsFailure()
    {
        var attr = new ValidateDoesContainAnyAttribute("apple", "banana", "cherry");
        var result = attr.Validate("I like grapes");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNull_ReturnsFailure()
    {
        var attr = new ValidateDoesContainAnyAttribute("a", "b");
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNonStringEnumerable_ReturnsFailure()
    {
        var attr = new ValidateDoesContainAnyAttribute("2", "3");
        var result = attr.Validate(new[] { 1, 2, 3 });
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateDoesContainAnyAttribute("a", "b") { Message = "Must contain a or b" };
        var result = attr.Validate("xyz");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must contain a or b"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateDoesContainAnyAttribute("a", "b");
        var result = attr.Validate("xyz", "Fruit");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Fruit"));
    }
}
