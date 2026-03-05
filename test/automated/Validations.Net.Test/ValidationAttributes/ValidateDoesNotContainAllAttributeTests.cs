using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateDoesNotContainAllAttributeTests
{
    [Test]
    public void Validate_WithStringMissingAtLeastOneValue_ReturnsSuccess()
    {
        var attr = new ValidateDoesNotContainAllAttribute("foo", "bar", "baz");
        var result = attr.Validate("foobar");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithStringContainingAllValues_ReturnsFailure()
    {
        var attr = new ValidateDoesNotContainAllAttribute("foo", "bar");
        var result = attr.Validate("foobar");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNull_ReturnsFailure()
    {
        var attr = new ValidateDoesNotContainAllAttribute("a", "b");
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNonStringEnumerable_ReturnsFailure()
    {
        var attr = new ValidateDoesNotContainAllAttribute("1", "2");
        var result = attr.Validate(new[] { 1, 2, 3 });
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateDoesNotContainAllAttribute("foo", "bar") { Message = "Must not contain both foo and bar" };
        var result = attr.Validate("foobar");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must not contain both foo and bar"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateDoesNotContainAllAttribute("foo", "bar");
        var result = attr.Validate("foobar", "Text");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Text"));
    }
}
