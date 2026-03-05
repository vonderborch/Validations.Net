using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsLessThanAttributeTests
{
    [Test]
    public void Validate_WhenValueLessThanComparand_ReturnsSuccess()
    {
        var attr = new ValidateIsLessThanAttribute(5);
        var result = attr.Validate(3);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WhenValueGreaterThanComparand_ReturnsFailure()
    {
        var attr = new ValidateIsLessThanAttribute(5);
        var result = attr.Validate(10);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsLessThan.ValidatorName));
    }

    [Test]
    public void Validate_WhenValueEqualsComparand_ReturnsFailure()
    {
        var attr = new ValidateIsLessThanAttribute(5);
        var result = attr.Validate(5);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessageOnFailure()
    {
        var attr = new ValidateIsLessThanAttribute(5) { Message = "Must be less than 5" };
        var result = attr.Validate(10);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Must be less than 5"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsLessThanAttribute(5);
        var result = attr.Validate(10, "Limit");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Limit"));
    }
}
