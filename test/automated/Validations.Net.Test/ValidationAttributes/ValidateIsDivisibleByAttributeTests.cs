namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsDivisibleByAttributeTests
{
    [Test]
    public void Validate_WhenDivisible_ReturnsSuccess()
    {
        var attr = new ValidateIsDivisibleByAttribute(5);
        var result = attr.Validate(10);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WhenNotDivisible_ReturnsFailure()
    {
        var attr = new ValidateIsDivisibleByAttribute(5);
        var result = attr.Validate(7);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsDivisibleBy.ValidatorName));
    }

    [Test]
    public void Validate_WhenNull_ReturnsSuccess()
    {
        var attr = new ValidateIsDivisibleByAttribute(5);
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessageOnFailure()
    {
        var attr = new ValidateIsDivisibleByAttribute(5) { Message = "Must be divisible by 5" };
        var result = attr.Validate(7);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Must be divisible by 5"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsDivisibleByAttribute(5);
        var result = attr.Validate(7, "Quantity");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Quantity"));
    }
}
