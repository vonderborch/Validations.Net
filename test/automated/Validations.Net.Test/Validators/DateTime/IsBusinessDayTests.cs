using Validations.Net.OLD;
using Validations.Net.OLD.Validators.DateTime;

namespace Validations.Net.Test.Validators.DateTime;

[TestFixture]
public class IsBusinessDayTests
{
    [Test]
    public void CheckIsBusinessDay_WithMonday_ReturnsTrue()
    {
        var monday = new System.DateTime(2025, 2, 17, 12, 0, 0, DateTimeKind.Utc);
        Assert.That(monday.CheckIsBusinessDay(), Is.True);
    }

    [Test]
    public void CheckIsBusinessDay_WithFriday_ReturnsTrue()
    {
        var friday = new System.DateTime(2025, 2, 21, 12, 0, 0, DateTimeKind.Utc);
        Assert.That(friday.CheckIsBusinessDay(), Is.True);
    }

    [Test]
    public void CheckIsBusinessDay_WithSaturday_ReturnsFalse()
    {
        var saturday = new System.DateTime(2025, 2, 22, 12, 0, 0, DateTimeKind.Utc);
        Assert.That(saturday.CheckIsBusinessDay(), Is.False);
    }

    [Test]
    public void CheckIsBusinessDay_WithSunday_ReturnsFalse()
    {
        var sunday = new System.DateTime(2025, 2, 23, 12, 0, 0, DateTimeKind.Utc);
        Assert.That(sunday.CheckIsBusinessDay(), Is.False);
    }

    [Test]
    public void ValidateIsBusinessDay_WithBusinessDay_ReturnsValid()
    {
        var monday = new System.DateTime(2025, 2, 17, 12, 0, 0, DateTimeKind.Utc);
        var result = monday.ValidateIsBusinessDay();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void EnsureIsBusinessDay_WithWeekend_Throws()
    {
        var saturday = new System.DateTime(2025, 2, 22, 12, 0, 0, DateTimeKind.Utc);
        Assert.Throws<ValidationException>(() => saturday.EnsureIsBusinessDay());
    }
}
