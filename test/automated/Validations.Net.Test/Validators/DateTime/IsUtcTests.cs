using Validations.Net.OLD;
using Validations.Net.OLD.Validators.DateTime;

namespace Validations.Net.Test.Validators.DateTime;

[TestFixture]
public class IsUtcTests
{
    [Test]
    public void CheckIsUtc_WithUtcDateTime_ReturnsTrue()
    {
        var utc = new System.DateTime(2025, 2, 21, 12, 0, 0, DateTimeKind.Utc);
        Assert.That(utc.CheckIsUtc(), Is.True);
    }

    [Test]
    public void CheckIsUtc_WithLocalDateTime_ReturnsFalse()
    {
        var local = new System.DateTime(2025, 2, 21, 12, 0, 0, DateTimeKind.Local);
        Assert.That(local.CheckIsUtc(), Is.False);
    }

    [Test]
    public void CheckIsUtc_WithUnspecifiedDateTime_ReturnsFalse()
    {
        var unspecified = new System.DateTime(2025, 2, 21, 12, 0, 0, DateTimeKind.Unspecified);
        Assert.That(unspecified.CheckIsUtc(), Is.False);
    }

    [Test]
    public void CheckIsUtc_WithUtcDateTimeOffset_ReturnsTrue()
    {
        var utc = DateTimeOffset.UtcNow;
        Assert.That(utc.CheckIsUtc(), Is.True);
    }

    [Test]
    public void ValidateIsUtc_WithUtcDateTime_ReturnsValid()
    {
        var utc = new System.DateTime(2025, 2, 21, 12, 0, 0, DateTimeKind.Utc);
        var result = utc.ValidateIsUtc();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void EnsureIsUtc_WithLocalDateTime_Throws()
    {
        var local = new System.DateTime(2025, 2, 21, 12, 0, 0, DateTimeKind.Local);
        Assert.Throws<ValidationException>(() => local.EnsureIsUtc());
    }
}
