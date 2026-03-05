using Validations.Net.OLD;
using Validations.Net.OLD.Validators.DateTime;

namespace Validations.Net.Test.Validators.DateTime;

[TestFixture]
public class IsInPastTests
{
    [Test]
    public void CheckIsInPast_WithPastDateTime_ReturnsTrue()
    {
        var past = System.DateTime.UtcNow.AddDays(-1);
        Assert.That(past.CheckIsInPast(), Is.True);
    }

    [Test]
    public void CheckIsInPast_WithFutureDateTime_ReturnsFalse()
    {
        var future = System.DateTime.UtcNow.AddDays(1);
        Assert.That(future.CheckIsInPast(), Is.False);
    }

    [Test]
    public void CheckIsInPast_WithPastDateTimeOffset_ReturnsTrue()
    {
        var past = DateTimeOffset.UtcNow.AddDays(-1);
        Assert.That(past.CheckIsInPast(), Is.True);
    }

    [Test]
    public void CheckIsInPast_WithFutureDateTimeOffset_ReturnsFalse()
    {
        var future = DateTimeOffset.UtcNow.AddDays(1);
        Assert.That(future.CheckIsInPast(), Is.False);
    }

    [Test]
    public void ValidateIsInPast_WithPastDateTime_ReturnsValid()
    {
        var past = System.DateTime.UtcNow.AddDays(-1);
        var result = past.ValidateIsInPast();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsInPast_WithFutureDateTime_ReturnsInvalid()
    {
        var future = System.DateTime.UtcNow.AddDays(1);
        var result = future.ValidateIsInPast();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsInPast.ValidatorName));
    }

    [Test]
    public void EnsureIsInPast_WithFutureDateTime_Throws()
    {
        var future = System.DateTime.UtcNow.AddDays(1);
        Assert.Throws<ValidationException>(() => future.EnsureIsInPast());
    }

    [Test]
    public void EnsureIsInPast_WithPastDateTime_ReturnsValue()
    {
        var past = System.DateTime.UtcNow.AddDays(-1);
        var result = past.EnsureIsInPast();
        Assert.That(result, Is.EqualTo(past));
    }
}
