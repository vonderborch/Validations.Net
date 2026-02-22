namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateDoesContainAttributeTests
{
    [Test]
    public void Validate_WithStringContainingSubstring_ReturnsSuccess()
    {
        var attr = new ValidateDoesContainAttribute("world");
        var result = attr.Validate("hello world");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithStringNotContainingSubstring_ReturnsFailure()
    {
        var attr = new ValidateDoesContainAttribute("xyz");
        var result = attr.Validate("hello world");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNull_ReturnsFailure()
    {
        var attr = new ValidateDoesContainAttribute("test");
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateDoesContainAttribute("xyz") { Message = "Must contain xyz" };
        var result = attr.Validate("abc");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must contain xyz"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateDoesContainAttribute("xyz");
        var result = attr.Validate("abc", "Text");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Text"));
    }
}
