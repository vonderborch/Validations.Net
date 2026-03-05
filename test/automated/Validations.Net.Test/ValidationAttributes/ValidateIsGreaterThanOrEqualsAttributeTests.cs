using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsGreaterThanOrEqualsAttributeTests
{
    [Test]
    public void Validate_WhenValueGreaterThanComparand_ReturnsSuccess()
    {
        var attr = new ValidateIsGreaterThanOrEqualsAttribute(5);
        var result = attr.Validate(10);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WhenValueEqualsComparand_ReturnsSuccess()
    {
        var attr = new ValidateIsGreaterThanOrEqualsAttribute(5);
        var result = attr.Validate(5);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WhenValueLessThanComparand_ReturnsFailure()
    {
        var attr = new ValidateIsGreaterThanOrEqualsAttribute(5);
        var result = attr.Validate(3);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsGreaterThanOrEquals.ValidatorName));
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessageOnFailure()
    {
        var attr = new ValidateIsGreaterThanOrEqualsAttribute(5) { Message = "Must be >= 5" };
        var result = attr.Validate(3);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Must be >= 5"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsGreaterThanOrEqualsAttribute(5);
        var result = attr.Validate(3, "Count");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Count"));
    }
}
