using Validations.Net.OLD;
using Validations.Net.OLD.Validators.DateTime;

namespace Validations.Net.Test.Validators.DateTime;

[TestFixture]
public class IsDateOnlyTests
{
    [Test]
    public void CheckIsDateOnly_WithMidnightDateTime_ReturnsTrue()
    {
        var dateOnly = new System.DateTime(2025, 2, 21, 0, 0, 0, DateTimeKind.Utc);
        Assert.That(dateOnly.CheckIsDateOnly(), Is.True);
    }

    [Test]
    public void CheckIsDateOnly_WithNonMidnightDateTime_ReturnsFalse()
    {
        var withTime = new System.DateTime(2025, 2, 21, 12, 30, 0, DateTimeKind.Utc);
        Assert.That(withTime.CheckIsDateOnly(), Is.False);
    }

    [Test]
    public void CheckIsDateOnly_WithMidnightDateTimeOffset_ReturnsTrue()
    {
        var dateOnly = new DateTimeOffset(2025, 2, 21, 0, 0, 0, TimeSpan.Zero);
        Assert.That(dateOnly.CheckIsDateOnly(), Is.True);
    }

    [Test]
    public void CheckIsDateOnly_WithNonMidnightDateTimeOffset_ReturnsFalse()
    {
        var withTime = new DateTimeOffset(2025, 2, 21, 12, 30, 0, TimeSpan.Zero);
        Assert.That(withTime.CheckIsDateOnly(), Is.False);
    }

    [Test]
    public void ValidateIsDateOnly_WithMidnightDateTime_ReturnsValid()
    {
        var dateOnly = new System.DateTime(2025, 2, 21, 0, 0, 0, DateTimeKind.Utc);
        var result = dateOnly.ValidateIsDateOnly();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsDateOnly_WithNonMidnightDateTime_ReturnsInvalid()
    {
        var withTime = new System.DateTime(2025, 2, 21, 12, 0, 0, DateTimeKind.Utc);
        var result = withTime.ValidateIsDateOnly();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsDateOnly.ValidatorName));
    }

    [Test]
    public void EnsureIsDateOnly_WithNonMidnightDateTime_Throws()
    {
        var withTime = new System.DateTime(2025, 2, 21, 12, 0, 0, DateTimeKind.Utc);
        Assert.Throws<ValidationException>(() => withTime.EnsureIsDateOnly());
    }

    [Test]
    public void EnsureIsDateOnly_WithMidnightDateTime_ReturnsValue()
    {
        var dateOnly = new System.DateTime(2025, 2, 21, 0, 0, 0, DateTimeKind.Utc);
        var result = dateOnly.EnsureIsDateOnly();
        Assert.That(result, Is.EqualTo(dateOnly));
    }
}
