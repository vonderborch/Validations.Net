namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsNotElementOfAttributeTests
{
    [Test]
    public void Validate_WithValueNotInDisallowedValues_ReturnsSuccess()
    {
        var attr = new ValidateIsNotElementOfAttribute("a", "b", "c");
        var result = attr.Validate("x");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithValueInDisallowedValues_ReturnsFailure()
    {
        var attr = new ValidateIsNotElementOfAttribute("a", "b", "c");
        var result = attr.Validate("b");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNull_WhenNullNotDisallowed_ReturnsSuccess()
    {
        var attr = new ValidateIsNotElementOfAttribute("a", "b");
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithNull_WhenNullDisallowed_ReturnsFailure()
    {
        var attr = new ValidateIsNotElementOfAttribute("a", null, "c");
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithEmptyDisallowedValues_ReturnsSuccess()
    {
        var attr = new ValidateIsNotElementOfAttribute();
        var result = attr.Validate("anything");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsNotElementOfAttribute("a", "b") { Message = "Must not be a or b" };
        var result = attr.Validate("a");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must not be a or b"));
    }
}
