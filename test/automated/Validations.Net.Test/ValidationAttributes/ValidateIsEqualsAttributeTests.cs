namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsEqualsAttributeTests
{
    [Test]
    public void Validate_WithMatchingInt_ReturnsSuccess()
    {
        var attr = new ValidateIsEqualsAttribute(42);
        var result = attr.Validate(42);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithNonMatchingInt_ReturnsFailure()
    {
        var attr = new ValidateIsEqualsAttribute(42);
        var result = attr.Validate(43);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsEquals.ValidatorName));
    }

    [Test]
    public void Validate_WithMatchingString_ReturnsSuccess()
    {
        var attr = new ValidateIsEqualsAttribute("hello");
        var result = attr.Validate("hello");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessageOnFailure()
    {
        var attr = new ValidateIsEqualsAttribute(42) { Message = "Value must be 42" };
        var result = attr.Validate(43);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Value must be 42"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsEqualsAttribute(42);
        var result = attr.Validate(43, "MyProperty");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("MyProperty"));
    }
}
