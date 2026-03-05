using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsNotEqualsAttributeTests
{
    [Test]
    public void Validate_WithDifferentValue_ReturnsSuccess()
    {
        var attr = new ValidateIsNotEqualsAttribute(42);
        var result = attr.Validate(43);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithMatchingValue_ReturnsFailure()
    {
        var attr = new ValidateIsNotEqualsAttribute(42);
        var result = attr.Validate(42);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsNotEquals.ValidatorName));
    }

    [Test]
    public void Validate_WithDifferentString_ReturnsSuccess()
    {
        var attr = new ValidateIsNotEqualsAttribute("hello");
        var result = attr.Validate("world");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessageOnFailure()
    {
        var attr = new ValidateIsNotEqualsAttribute(42) { Message = "Value must not be 42" };
        var result = attr.Validate(42);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Value must not be 42"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsNotEqualsAttribute(42);
        var result = attr.Validate(42, "MyProperty");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("MyProperty"));
    }
}
