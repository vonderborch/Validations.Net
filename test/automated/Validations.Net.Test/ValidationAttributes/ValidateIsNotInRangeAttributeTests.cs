namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsNotInRangeAttributeTests
{
    [Test]
    public void Validate_WhenValueBelowMin_ReturnsSuccess()
    {
        var attr = new ValidateIsNotInRangeAttribute(1, 10);
        var result = attr.Validate(0);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WhenValueAboveMax_ReturnsSuccess()
    {
        var attr = new ValidateIsNotInRangeAttribute(1, 10);
        var result = attr.Validate(11);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WhenValueInRange_ReturnsFailure()
    {
        var attr = new ValidateIsNotInRangeAttribute(1, 10);
        var result = attr.Validate(5);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsNotInRange.ValidatorName));
    }

    [Test]
    public void Validate_WithInclusiveBounds_RejectsMinAndMax()
    {
        var attr = new ValidateIsNotInRangeAttribute(1, 10, minInclusive: true, maxInclusive: true);
        Assert.That(attr.Validate(1).IsValid, Is.False);
        Assert.That(attr.Validate(10).IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessageOnFailure()
    {
        var attr = new ValidateIsNotInRangeAttribute(1, 10) { Message = "Must not be between 1 and 10" };
        var result = attr.Validate(5);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Must not be between 1 and 10"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsNotInRangeAttribute(1, 10);
        var result = attr.Validate(5, "ExcludedValue");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("ExcludedValue"));
    }
}
