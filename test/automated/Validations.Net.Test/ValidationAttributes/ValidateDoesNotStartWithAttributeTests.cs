using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateDoesNotStartWithAttributeTests
{
    [Test]
    public void Validate_WithStringNotStartingWithPrefix_ReturnsSuccess()
    {
        var attr = new ValidateDoesNotStartWithAttribute("hello");
        var result = attr.Validate("goodbye world");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithStringStartingWithPrefix_ReturnsFailure()
    {
        var attr = new ValidateDoesNotStartWithAttribute("hello");
        var result = attr.Validate("hello world");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNull_ReturnsSuccess()
    {
        var attr = new ValidateDoesNotStartWithAttribute("hello");
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateDoesNotStartWithAttribute("hello") { Message = "Must not start with hello" };
        var result = attr.Validate("hello");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must not start with hello"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateDoesNotStartWithAttribute("hello");
        var result = attr.Validate("hello", "Name");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Name"));
    }
}
