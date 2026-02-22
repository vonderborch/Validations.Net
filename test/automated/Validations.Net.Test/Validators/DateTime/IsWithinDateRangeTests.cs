namespace Validations.Net.Test.Validators.DateTime;

[TestFixture]
public class IsWithinDateRangeTests
{
    [Test]
    public void CheckIsWithinDateRange_WithDateInRange_ReturnsTrue()
    {
        var min = new System.DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var max = new System.DateTime(2025, 12, 31, 23, 59, 59, DateTimeKind.Utc);
        var value = new System.DateTime(2025, 6, 15, 12, 0, 0, DateTimeKind.Utc);
        Assert.That(value.CheckIsWithinDateRange(min, max), Is.True);
    }

    [Test]
    public void CheckIsWithinDateRange_WithDateAtBounds_ReturnsTrue()
    {
        var min = new System.DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var max = new System.DateTime(2025, 12, 31, 0, 0, 0, DateTimeKind.Utc);
        Assert.That(min.CheckIsWithinDateRange(min, max), Is.True);
        Assert.That(max.CheckIsWithinDateRange(min, max), Is.True);
    }

    [Test]
    public void CheckIsWithinDateRange_WithDateBeforeMin_ReturnsFalse()
    {
        var min = new System.DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var max = new System.DateTime(2025, 12, 31, 0, 0, 0, DateTimeKind.Utc);
        var value = new System.DateTime(2024, 12, 31, 23, 59, 59, DateTimeKind.Utc);
        Assert.That(value.CheckIsWithinDateRange(min, max), Is.False);
    }

    [Test]
    public void ValidateIsWithinDateRange_WithDateInRange_ReturnsValid()
    {
        var min = new System.DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var max = new System.DateTime(2025, 12, 31, 0, 0, 0, DateTimeKind.Utc);
        var value = new System.DateTime(2025, 6, 15, 0, 0, 0, DateTimeKind.Utc);
        var result = value.ValidateIsWithinDateRange(min, max);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void EnsureIsWithinDateRange_WithDateOutOfRange_Throws()
    {
        var min = new System.DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var max = new System.DateTime(2025, 12, 31, 0, 0, 0, DateTimeKind.Utc);
        var value = new System.DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        Assert.Throws<ValidationException>(() => value.EnsureIsWithinDateRange(min, max));
    }
}
