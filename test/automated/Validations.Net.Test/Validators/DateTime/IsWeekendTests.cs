namespace Validations.Net.Test.Validators.DateTime;

[TestFixture]
public class IsWeekendTests
{
    [Test]
    public void CheckIsWeekend_WithSaturday_ReturnsTrue()
    {
        var saturday = new System.DateTime(2025, 2, 22, 12, 0, 0, DateTimeKind.Utc);
        Assert.That(saturday.CheckIsWeekend(), Is.True);
    }

    [Test]
    public void CheckIsWeekend_WithSunday_ReturnsTrue()
    {
        var sunday = new System.DateTime(2025, 2, 23, 12, 0, 0, DateTimeKind.Utc);
        Assert.That(sunday.CheckIsWeekend(), Is.True);
    }

    [Test]
    public void CheckIsWeekend_WithMonday_ReturnsFalse()
    {
        var monday = new System.DateTime(2025, 2, 17, 12, 0, 0, DateTimeKind.Utc);
        Assert.That(monday.CheckIsWeekend(), Is.False);
    }

    [Test]
    public void ValidateIsWeekend_WithWeekend_ReturnsValid()
    {
        var saturday = new System.DateTime(2025, 2, 22, 12, 0, 0, DateTimeKind.Utc);
        var result = saturday.ValidateIsWeekend();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void EnsureIsWeekend_WithBusinessDay_Throws()
    {
        var monday = new System.DateTime(2025, 2, 17, 12, 0, 0, DateTimeKind.Utc);
        Assert.Throws<ValidationException>(() => monday.EnsureIsWeekend());
    }
}
