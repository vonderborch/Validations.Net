namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateDoesContainAllAttributeTests
{
    [Test]
    public void Validate_WithStringContainingAllValues_ReturnsSuccess()
    {
        var attr = new ValidateDoesContainAllAttribute("foo", "bar");
        var result = attr.Validate("foobar contains foo and bar");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithStringMissingOneValue_ReturnsFailure()
    {
        var attr = new ValidateDoesContainAllAttribute("foo", "bar", "baz");
        var result = attr.Validate("foobar");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNull_ReturnsFailure()
    {
        var attr = new ValidateDoesContainAllAttribute("a", "b");
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateDoesContainAllAttribute("foo", "bar") { Message = "Must contain foo and bar" };
        var result = attr.Validate("foo only");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must contain foo and bar"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateDoesContainAllAttribute("foo", "bar");
        var result = attr.Validate("foo only", "Text");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Text"));
    }
}
