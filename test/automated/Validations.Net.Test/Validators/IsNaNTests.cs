using Validations.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNaNTests
{
    #region CheckIsNaN Tests

    [Test]
    public void CheckIsNaN_WithNaN_ReturnsTrue()
    {
        Assert.That(double.NaN.CheckIsNaN(), Is.True);
    }

    [Test]
    public void CheckIsNaN_WithFiniteDouble_ReturnsFalse()
    {
        Assert.That(3.14.CheckIsNaN(), Is.False);
    }

    [Test]
    public void CheckIsNaN_WithInfinity_ReturnsFalse()
    {
        Assert.That(double.PositiveInfinity.CheckIsNaN(), Is.False);
    }

    [Test]
    public void CheckIsNaN_WithFloat_Works()
    {
        Assert.That(float.NaN.CheckIsNaN(), Is.True);
        Assert.That(1.5f.CheckIsNaN(), Is.False);
    }

    #endregion

    #region ValidateIsNaN Tests

    [Test]
    public void ValidateIsNaN_WithNaN_ReturnsValidResult()
    {
        var result = double.NaN.ValidateIsNaN();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsNaN_WithFiniteValue_ReturnsInvalidResult()
    {
        var result = 3.14.ValidateIsNaN();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsNaN.ValidatorName));
    }

    #endregion

    #region EnsureIsNaN Tests

    [Test]
    public void EnsureIsNaN_WithNaN_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => double.NaN.EnsureIsNaN());
    }

    [Test]
    public void EnsureIsNaN_WithFiniteValue_ThrowsValidationException()
    {
        Assert.Throws<ValidationException>(() => 3.14.EnsureIsNaN());
    }

    [Test]
    public void EnsureIsNaN_ReturnsValue()
    {
        var result = double.NaN.EnsureIsNaN();
        Assert.That(result, Is.EqualTo(double.NaN));
    }

    #endregion
}
