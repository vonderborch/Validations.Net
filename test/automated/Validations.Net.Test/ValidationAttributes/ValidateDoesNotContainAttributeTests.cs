namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateDoesNotContainAttributeTests
{
    [Test]
    public void Validate_WithStringNotContainingSubstring_ReturnsSuccess()
    {
        var attr = new ValidateDoesNotContainAttribute("xyz");
        var result = attr.Validate("hello world");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithStringContainingSubstring_ReturnsFailure()
    {
        var attr = new ValidateDoesNotContainAttribute("world");
        var result = attr.Validate("hello world");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNull_ReturnsFailure()
    {
        var attr = new ValidateDoesNotContainAttribute("test");
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNonStringEnumerable_ReturnsFailure()
    {
        var attr = new ValidateDoesNotContainAttribute("2");
        var result = attr.Validate(new[] { 1, 2, 3 });
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateDoesNotContainAttribute("bad") { Message = "Must not contain bad" };
        var result = attr.Validate("bad word");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must not contain bad"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateDoesNotContainAttribute("bad");
        var result = attr.Validate("bad", "Text");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Text"));
    }
}
