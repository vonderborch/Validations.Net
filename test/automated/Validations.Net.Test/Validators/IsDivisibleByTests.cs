using Validations.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsDivisibleByTests
{
    #region CheckIsDivisibleBy Tests

    [Test]
    public void CheckIsDivisibleBy_WithDivisibleValue_ReturnsTrue()
    {
        Assert.That(10.CheckIsDivisibleBy(2), Is.True);
    }

    [Test]
    public void CheckIsDivisibleBy_WithNonDivisibleValue_ReturnsFalse()
    {
        Assert.That(10.CheckIsDivisibleBy(3), Is.False);
    }

    [Test]
    public void CheckIsDivisibleBy_WithZeroDivisor_ReturnsFalse()
    {
        Assert.That(10.CheckIsDivisibleBy(0), Is.False);
    }

    [Test]
    public void CheckIsDivisibleBy_WithZeroValue_ReturnsTrue()
    {
        Assert.That(0.CheckIsDivisibleBy(5), Is.True);
    }

    [Test]
    public void CheckIsDivisibleBy_WithLong_Works()
    {
        Assert.That(100L.CheckIsDivisibleBy(25L), Is.True);
        Assert.That(100L.CheckIsDivisibleBy(7L), Is.False);
    }

    #endregion

    #region ValidateIsDivisibleBy Tests

    [Test]
    public void ValidateIsDivisibleBy_WithDivisibleValue_ReturnsValidResult()
    {
        var result = 10.ValidateIsDivisibleBy(2);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsDivisibleBy_WithNonDivisibleValue_ReturnsInvalidResult()
    {
        var result = 10.ValidateIsDivisibleBy(3);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsDivisibleBy.ValidatorName));
    }

    [Test]
    public void ValidateIsDivisibleBy_WithZeroDivisor_ReturnsInvalidResult()
    {
        var result = 10.ValidateIsDivisibleBy(0);
        Assert.That(result.IsValid, Is.False);
    }

    #endregion

    #region EnsureIsDivisibleBy Tests

    [Test]
    public void EnsureIsDivisibleBy_WithDivisibleValue_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => 10.EnsureIsDivisibleBy(2));
    }

    [Test]
    public void EnsureIsDivisibleBy_WithNonDivisibleValue_ThrowsValidationException()
    {
        Assert.Throws<ValidationException>(() => 10.EnsureIsDivisibleBy(3));
    }

    [Test]
    public void EnsureIsDivisibleBy_ReturnsValue()
    {
        var result = 42.EnsureIsDivisibleBy(7);
        Assert.That(result, Is.EqualTo(42));
    }

    [Test]
    public void EnsureIsDivisibleBy_WithZeroDivisor_Throws()
    {
        Assert.Throws<ValidationException>(() => 10.EnsureIsDivisibleBy(0));
    }

    #endregion
}
