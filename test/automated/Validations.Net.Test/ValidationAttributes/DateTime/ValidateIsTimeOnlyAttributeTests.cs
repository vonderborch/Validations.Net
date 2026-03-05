using Validations.Net.OLD.Validators.DateTime;

namespace Validations.Net.Test.ValidationAttributes.DateTime;

[TestFixture]
public class ValidateIsTimeOnlyAttributeTests
{
    [Test]
    public void Validate_WithMinValueDate_ReturnsSuccess()
    {
        var attr = new ValidateIsTimeOnlyAttribute();
        var timeOnly = System.DateTime.MinValue.Add(new TimeSpan(12, 30, 0));
        var result = attr.Validate(timeOnly);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithNonMinValueDate_ReturnsFailure()
    {
        var attr = new ValidateIsTimeOnlyAttribute();
        var withDate = new System.DateTime(2025, 2, 21, 12, 0, 0, DateTimeKind.Utc);
        var result = attr.Validate(withDate);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsTimeOnly.ValidatorName));
    }

    [Test]
    public void Validate_WithMinValueDateDateTimeOffset_ReturnsSuccess()
    {
        var attr = new ValidateIsTimeOnlyAttribute();
        var timeOnly = DateTimeOffset.MinValue.Add(new TimeSpan(12, 30, 0));
        var result = attr.Validate(timeOnly);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithNonDateTime_ReturnsFailure()
    {
        var attr = new ValidateIsTimeOnlyAttribute();
        var result = attr.Validate("not a date");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessageOnFailure()
    {
        var attr = new ValidateIsTimeOnlyAttribute { Message = "Must be time only" };
        var withDate = new System.DateTime(2025, 2, 21, 12, 0, 0, DateTimeKind.Utc);
        var result = attr.Validate(withDate);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Must be time only"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsTimeOnlyAttribute();
        var withDate = new System.DateTime(2025, 2, 21, 12, 0, 0, DateTimeKind.Utc);
        var result = attr.Validate(withDate, "StartTime");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("StartTime"));
    }
}
