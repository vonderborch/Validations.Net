namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsElementOfAttributeTests
{
    [Test]
    public void Validate_WithValueInAllowedValues_ReturnsSuccess()
    {
        var attr = new ValidateIsElementOfAttribute("a", "b", "c");
        var result = attr.Validate("b");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithValueNotInAllowedValues_ReturnsFailure()
    {
        var attr = new ValidateIsElementOfAttribute("a", "b", "c");
        var result = attr.Validate("x");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNull_WhenNullNotAllowed_ReturnsFailure()
    {
        var attr = new ValidateIsElementOfAttribute("a", "b");
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNull_WhenNullAllowed_ReturnsSuccess()
    {
        var attr = new ValidateIsElementOfAttribute("a", null, "c");
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithIntInAllowedValues_ReturnsSuccess()
    {
        var attr = new ValidateIsElementOfAttribute(1, 2, 3);
        var result = attr.Validate(2);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithEmptyAllowedValues_ReturnsFailure()
    {
        var attr = new ValidateIsElementOfAttribute();
        var result = attr.Validate("anything");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsElementOfAttribute("a", "b") { Message = "Must be a or b" };
        var result = attr.Validate("x");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must be a or b"));
    }
}
