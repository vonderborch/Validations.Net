using Validations.Net.OLD;
using Validations.Net.OLD.Validators.DateTime;

namespace Validations.Net.Test.Validators.DateTime;

[TestFixture]
public class IsTimeOnlyTests
{
    [Test]
    public void CheckIsTimeOnly_WithMinValueDate_ReturnsTrue()
    {
        var timeOnly = System.DateTime.MinValue.Add(new TimeSpan(12, 30, 0));
        Assert.That(timeOnly.CheckIsTimeOnly(), Is.True);
    }

    [Test]
    public void CheckIsTimeOnly_WithNonMinValueDate_ReturnsFalse()
    {
        var withDate = new System.DateTime(2025, 2, 21, 12, 30, 0, DateTimeKind.Utc);
        Assert.That(withDate.CheckIsTimeOnly(), Is.False);
    }

    [Test]
    public void CheckIsTimeOnly_WithMinValueDateDateTimeOffset_ReturnsTrue()
    {
        var timeOnly = DateTimeOffset.MinValue.Add(new TimeSpan(12, 30, 0));
        Assert.That(timeOnly.CheckIsTimeOnly(), Is.True);
    }

    [Test]
    public void CheckIsTimeOnly_WithNonMinValueDateDateTimeOffset_ReturnsFalse()
    {
        var withDate = new DateTimeOffset(2025, 2, 21, 12, 30, 0, TimeSpan.Zero);
        Assert.That(withDate.CheckIsTimeOnly(), Is.False);
    }

    [Test]
    public void ValidateIsTimeOnly_WithMinValueDate_ReturnsValid()
    {
        var timeOnly = System.DateTime.MinValue.Add(new TimeSpan(12, 30, 0));
        var result = timeOnly.ValidateIsTimeOnly();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsTimeOnly_WithNonMinValueDate_ReturnsInvalid()
    {
        var withDate = new System.DateTime(2025, 2, 21, 12, 0, 0, DateTimeKind.Utc);
        var result = withDate.ValidateIsTimeOnly();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsTimeOnly.ValidatorName));
    }

    [Test]
    public void EnsureIsTimeOnly_WithNonMinValueDate_Throws()
    {
        var withDate = new System.DateTime(2025, 2, 21, 12, 0, 0, DateTimeKind.Utc);
        Assert.Throws<ValidationException>(() => withDate.EnsureIsTimeOnly());
    }

    [Test]
    public void EnsureIsTimeOnly_WithMinValueDate_ReturnsValue()
    {
        var timeOnly = System.DateTime.MinValue.Add(new TimeSpan(12, 30, 0));
        var result = timeOnly.EnsureIsTimeOnly();
        Assert.That(result, Is.EqualTo(timeOnly));
    }
}
