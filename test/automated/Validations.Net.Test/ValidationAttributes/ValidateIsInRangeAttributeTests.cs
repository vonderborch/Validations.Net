namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsInRangeAttributeTests
{
    [Test]
    public void Validate_WhenValueInRange_ReturnsSuccess()
    {
        var attr = new ValidateIsInRangeAttribute(1, 10);
        var result = attr.Validate(5);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WhenValueBelowMin_ReturnsFailure()
    {
        var attr = new ValidateIsInRangeAttribute(1, 10);
        var result = attr.Validate(0);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsInRange.ValidatorName));
    }

    [Test]
    public void Validate_WhenValueAboveMax_ReturnsFailure()
    {
        var attr = new ValidateIsInRangeAttribute(1, 10);
        var result = attr.Validate(11);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithInclusiveBounds_AcceptsMinAndMax()
    {
        var attr = new ValidateIsInRangeAttribute(1, 10, minInclusive: true, maxInclusive: true);
        Assert.That(attr.Validate(1).IsValid, Is.True);
        Assert.That(attr.Validate(10).IsValid, Is.True);
    }

    [Test]
    public void Validate_WithExclusiveBounds_RejectsMinAndMax()
    {
        var attr = new ValidateIsInRangeAttribute(1, 10, minInclusive: false, maxInclusive: false);
        Assert.That(attr.Validate(1).IsValid, Is.False);
        Assert.That(attr.Validate(10).IsValid, Is.False);
        Assert.That(attr.Validate(5).IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessageOnFailure()
    {
        var attr = new ValidateIsInRangeAttribute(1, 10) { Message = "Must be between 1 and 10" };
        var result = attr.Validate(0);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Must be between 1 and 10"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsInRangeAttribute(1, 10);
        var result = attr.Validate(0, "Amount");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Amount"));
    }
}
