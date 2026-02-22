using Validations.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsOddTests
{
    #region CheckIsOdd Tests

    [Test]
    public void CheckIsOdd_WithOddInt_ReturnsTrue()
    {
        Assert.That(41.CheckIsOdd(), Is.True);
    }

    [Test]
    public void CheckIsOdd_WithEvenInt_ReturnsFalse()
    {
        Assert.That(42.CheckIsOdd(), Is.False);
    }

    [Test]
    public void CheckIsOdd_WithZero_ReturnsFalse()
    {
        Assert.That(0.CheckIsOdd(), Is.False);
    }

    [Test]
    public void CheckIsOdd_WithNegativeOdd_ReturnsTrue()
    {
        Assert.That((-3).CheckIsOdd(), Is.True);
    }

    [Test]
    public void CheckIsOdd_WithLong_Works()
    {
        Assert.That(101L.CheckIsOdd(), Is.True);
        Assert.That(100L.CheckIsOdd(), Is.False);
    }

    #endregion

    #region ValidateIsOdd Tests

    [Test]
    public void ValidateIsOdd_WithOddValue_ReturnsValidResult()
    {
        var result = 41.ValidateIsOdd();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsOdd_WithEvenValue_ReturnsInvalidResult()
    {
        var result = 42.ValidateIsOdd();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsOdd.ValidatorName));
    }

    #endregion

    #region EnsureIsOdd Tests

    [Test]
    public void EnsureIsOdd_WithOddValue_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => 41.EnsureIsOdd());
    }

    [Test]
    public void EnsureIsOdd_WithEvenValue_ThrowsValidationException()
    {
        Assert.Throws<ValidationException>(() => 42.EnsureIsOdd());
    }

    [Test]
    public void EnsureIsOdd_ReturnsValue()
    {
        var result = 41.EnsureIsOdd();
        Assert.That(result, Is.EqualTo(41));
    }

    #endregion
}
