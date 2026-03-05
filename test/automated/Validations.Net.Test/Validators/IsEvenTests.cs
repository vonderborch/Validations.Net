using Validations.Net;
using Validations.Net.OLD;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsEvenTests
{
    #region CheckIsEven Tests

    [Test]
    public void CheckIsEven_WithEvenInt_ReturnsTrue()
    {
        Assert.That(42.CheckIsEven(), Is.True);
    }

    [Test]
    public void CheckIsEven_WithOddInt_ReturnsFalse()
    {
        Assert.That(41.CheckIsEven(), Is.False);
    }

    [Test]
    public void CheckIsEven_WithZero_ReturnsTrue()
    {
        Assert.That(0.CheckIsEven(), Is.True);
    }

    [Test]
    public void CheckIsEven_WithNegativeEven_ReturnsTrue()
    {
        Assert.That((-4).CheckIsEven(), Is.True);
    }

    [Test]
    public void CheckIsEven_WithLong_Works()
    {
        Assert.That(100L.CheckIsEven(), Is.True);
        Assert.That(101L.CheckIsEven(), Is.False);
    }

    #endregion

    #region ValidateIsEven Tests

    [Test]
    public void ValidateIsEven_WithEvenValue_ReturnsValidResult()
    {
        var result = 42.ValidateIsEven();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsEven_WithOddValue_ReturnsInvalidResult()
    {
        var result = 41.ValidateIsEven();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsEven.ValidatorName));
    }

    #endregion

    #region EnsureIsEven Tests

    [Test]
    public void EnsureIsEven_WithEvenValue_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => 42.EnsureIsEven());
    }

    [Test]
    public void EnsureIsEven_WithOddValue_ThrowsValidationException()
    {
        Assert.Throws<ValidationException>(() => 41.EnsureIsEven());
    }

    [Test]
    public void EnsureIsEven_ReturnsValue()
    {
        var result = 42.EnsureIsEven();
        Assert.That(result, Is.EqualTo(42));
    }

    #endregion
}
