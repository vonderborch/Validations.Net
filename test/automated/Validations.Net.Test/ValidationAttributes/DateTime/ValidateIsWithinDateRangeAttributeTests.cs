using Validations.Net.OLD.Validators.DateTime;

namespace Validations.Net.Test.ValidationAttributes.DateTime;

[TestFixture]
public class ValidateIsWithinDateRangeAttributeTests
{
    [Test]
    public void Validate_WithDateInRange_ReturnsSuccess()
    {
        var min = new System.DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var max = new System.DateTime(2025, 12, 31, 23, 59, 59, DateTimeKind.Utc);
        var attr = new ValidateIsWithinDateRangeAttribute(min, max);
        var value = new System.DateTime(2025, 6, 15, 12, 0, 0, DateTimeKind.Utc);
        var result = attr.Validate(value);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithDateBeforeMin_ReturnsFailure()
    {
        var min = new System.DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var max = new System.DateTime(2025, 12, 31, 0, 0, 0, DateTimeKind.Utc);
        var attr = new ValidateIsWithinDateRangeAttribute(min, max);
        var value = new System.DateTime(2024, 12, 31, 23, 59, 59, DateTimeKind.Utc);
        var result = attr.Validate(value);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithDateAfterMax_ReturnsFailure()
    {
        var min = new System.DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var max = new System.DateTime(2025, 12, 31, 0, 0, 0, DateTimeKind.Utc);
        var attr = new ValidateIsWithinDateRangeAttribute(min, max);
        var value = new System.DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var result = attr.Validate(value);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var min = new System.DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var max = new System.DateTime(2025, 12, 31, 0, 0, 0, DateTimeKind.Utc);
        var attr = new ValidateIsWithinDateRangeAttribute(min, max) { Message = "Date must be in 2025" };
        var value = new System.DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var result = attr.Validate(value);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Date must be in 2025"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var min = new System.DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var max = new System.DateTime(2025, 12, 31, 0, 0, 0, DateTimeKind.Utc);
        var attr = new ValidateIsWithinDateRangeAttribute(min, max);
        var value = new System.DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var result = attr.Validate(value, "StartDate");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("StartDate"));
    }
}
