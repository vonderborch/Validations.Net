namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsGreaterThanAttributeTests
{
    [Test]
    public void Validate_WhenValueGreaterThanComparand_ReturnsSuccess()
    {
        var attr = new ValidateIsGreaterThanAttribute(5);
        var result = attr.Validate(10);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WhenValueLessThanComparand_ReturnsFailure()
    {
        var attr = new ValidateIsGreaterThanAttribute(5);
        var result = attr.Validate(3);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsGreaterThan.ValidatorName));
    }

    [Test]
    public void Validate_WhenValueEqualsComparand_ReturnsFailure()
    {
        var attr = new ValidateIsGreaterThanAttribute(5);
        var result = attr.Validate(5);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessageOnFailure()
    {
        var attr = new ValidateIsGreaterThanAttribute(5) { Message = "Must be greater than 5" };
        var result = attr.Validate(3);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Must be greater than 5"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsGreaterThanAttribute(5);
        var result = attr.Validate(3, "Score");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Score"));
    }
}
