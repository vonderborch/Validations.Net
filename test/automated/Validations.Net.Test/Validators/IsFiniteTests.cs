using Validations.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsFiniteTests
{
    #region CheckIsFinite Tests

    [Test]
    public void CheckIsFinite_WithFiniteDouble_ReturnsTrue()
    {
        Assert.That(3.14.CheckIsFinite(), Is.True);
    }

    [Test]
    public void CheckIsFinite_WithNaN_ReturnsFalse()
    {
        Assert.That(double.NaN.CheckIsFinite(), Is.False);
    }

    [Test]
    public void CheckIsFinite_WithPositiveInfinity_ReturnsFalse()
    {
        Assert.That(double.PositiveInfinity.CheckIsFinite(), Is.False);
    }

    [Test]
    public void CheckIsFinite_WithNegativeInfinity_ReturnsFalse()
    {
        Assert.That(double.NegativeInfinity.CheckIsFinite(), Is.False);
    }

    [Test]
    public void CheckIsFinite_WithFloat_Works()
    {
        Assert.That(1.5f.CheckIsFinite(), Is.True);
        Assert.That(float.NaN.CheckIsFinite(), Is.False);
    }

    #endregion

    #region ValidateIsFinite Tests

    [Test]
    public void ValidateIsFinite_WithFiniteValue_ReturnsValidResult()
    {
        var result = 3.14.ValidateIsFinite();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsFinite_WithNaN_ReturnsInvalidResult()
    {
        var result = double.NaN.ValidateIsFinite();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsFinite.ValidatorName));
    }

    #endregion

    #region EnsureIsFinite Tests

    [Test]
    public void EnsureIsFinite_WithFiniteValue_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => 3.14.EnsureIsFinite());
    }

    [Test]
    public void EnsureIsFinite_WithNaN_ThrowsValidationException()
    {
        Assert.Throws<ValidationException>(() => double.NaN.EnsureIsFinite());
    }

    [Test]
    public void EnsureIsFinite_WithInfinity_ThrowsValidationException()
    {
        Assert.Throws<ValidationException>(() => double.PositiveInfinity.EnsureIsFinite());
    }

    [Test]
    public void EnsureIsFinite_ReturnsValue()
    {
        var result = 3.14.EnsureIsFinite();
        Assert.That(result, Is.EqualTo(3.14));
    }

    #endregion
}
