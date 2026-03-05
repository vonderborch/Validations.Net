using Validations.Net.OLD;
using Validations.Net.OLD.Validators.DateTime;

namespace Validations.Net.Test.Validators.DateTime;

[TestFixture]
public class IsInFutureTests
{
    [Test]
    public void CheckIsInFuture_WithFutureDateTime_ReturnsTrue()
    {
        var future = System.DateTime.UtcNow.AddDays(1);
        Assert.That(future.CheckIsInFuture(), Is.True);
    }

    [Test]
    public void CheckIsInFuture_WithPastDateTime_ReturnsFalse()
    {
        var past = System.DateTime.UtcNow.AddDays(-1);
        Assert.That(past.CheckIsInFuture(), Is.False);
    }

    [Test]
    public void CheckIsInFuture_WithFutureDateTimeOffset_ReturnsTrue()
    {
        var future = DateTimeOffset.UtcNow.AddDays(1);
        Assert.That(future.CheckIsInFuture(), Is.True);
    }

    [Test]
    public void CheckIsInFuture_WithPastDateTimeOffset_ReturnsFalse()
    {
        var past = DateTimeOffset.UtcNow.AddDays(-1);
        Assert.That(past.CheckIsInFuture(), Is.False);
    }

    [Test]
    public void ValidateIsInFuture_WithFutureDateTime_ReturnsValid()
    {
        var future = System.DateTime.UtcNow.AddDays(1);
        var result = future.ValidateIsInFuture();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsInFuture_WithPastDateTime_ReturnsInvalid()
    {
        var past = System.DateTime.UtcNow.AddDays(-1);
        var result = past.ValidateIsInFuture();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsInFuture.ValidatorName));
    }

    [Test]
    public void EnsureIsInFuture_WithPastDateTime_Throws()
    {
        var past = System.DateTime.UtcNow.AddDays(-1);
        Assert.Throws<ValidationException>(() => past.EnsureIsInFuture());
    }

    [Test]
    public void EnsureIsInFuture_WithFutureDateTime_ReturnsValue()
    {
        var future = System.DateTime.UtcNow.AddDays(1);
        var result = future.EnsureIsInFuture();
        Assert.That(result, Is.EqualTo(future));
    }
}
