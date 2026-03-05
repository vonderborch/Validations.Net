using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsLessThanOrEqualsAttributeTests
{
    [Test]
    public void Validate_WhenValueLessThanComparand_ReturnsSuccess()
    {
        var attr = new ValidateIsLessThanOrEqualsAttribute(5);
        var result = attr.Validate(3);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WhenValueEqualsComparand_ReturnsSuccess()
    {
        var attr = new ValidateIsLessThanOrEqualsAttribute(5);
        var result = attr.Validate(5);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WhenValueGreaterThanComparand_ReturnsFailure()
    {
        var attr = new ValidateIsLessThanOrEqualsAttribute(5);
        var result = attr.Validate(10);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsLessThanOrEquals.ValidatorName));
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessageOnFailure()
    {
        var attr = new ValidateIsLessThanOrEqualsAttribute(5) { Message = "Must be <= 5" };
        var result = attr.Validate(10);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Must be <= 5"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsLessThanOrEqualsAttribute(5);
        var result = attr.Validate(10, "MaxValue");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("MaxValue"));
    }
}
