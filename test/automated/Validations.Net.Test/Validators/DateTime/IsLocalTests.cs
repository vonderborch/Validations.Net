namespace Validations.Net.Test.Validators.DateTime;

[TestFixture]
public class IsLocalTests
{
    [Test]
    public void CheckIsLocal_WithLocalDateTime_ReturnsTrue()
    {
        var local = new System.DateTime(2025, 2, 21, 12, 0, 0, DateTimeKind.Local);
        Assert.That(local.CheckIsLocal(), Is.True);
    }

    [Test]
    public void CheckIsLocal_WithUtcDateTime_ReturnsFalse()
    {
        var utc = new System.DateTime(2025, 2, 21, 12, 0, 0, DateTimeKind.Utc);
        Assert.That(utc.CheckIsLocal(), Is.False);
    }

    [Test]
    public void CheckIsLocal_WithUnspecifiedDateTime_ReturnsFalse()
    {
        var unspecified = new System.DateTime(2025, 2, 21, 12, 0, 0, DateTimeKind.Unspecified);
        Assert.That(unspecified.CheckIsLocal(), Is.False);
    }

    [Test]
    public void ValidateIsLocal_WithLocalDateTime_ReturnsValid()
    {
        var local = new System.DateTime(2025, 2, 21, 12, 0, 0, DateTimeKind.Local);
        var result = local.ValidateIsLocal();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void EnsureIsLocal_WithUtcDateTime_Throws()
    {
        var utc = new System.DateTime(2025, 2, 21, 12, 0, 0, DateTimeKind.Utc);
        Assert.Throws<ValidationException>(() => utc.EnsureIsLocal());
    }
}
