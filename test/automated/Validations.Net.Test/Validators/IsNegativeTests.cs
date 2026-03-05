using Validations.Net;
using Validations.Net.OLD;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNegativeTests
{
    #region CheckIsNegative Tests

    [Test]
    public void CheckIsNegative_WithNegativeInt_ReturnsTrue()
    {
        Assert.That((-42).CheckIsNegative(), Is.True);
    }

    [Test]
    public void CheckIsNegative_WithPositiveInt_ReturnsFalse()
    {
        Assert.That(42.CheckIsNegative(), Is.False);
    }

    [Test]
    public void CheckIsNegative_WithZero_ReturnsFalse()
    {
        Assert.That(0.CheckIsNegative(), Is.False);
    }

    [Test]
    public void CheckIsNegative_WithNegativeDouble_ReturnsTrue()
    {
        Assert.That((-3.14).CheckIsNegative(), Is.True);
    }

    [Test]
    public void CheckIsNegative_WithPositiveDecimal_ReturnsFalse()
    {
        Assert.That(1.5m.CheckIsNegative(), Is.False);
    }

    #endregion

    #region ValidateIsNegative Tests

    [Test]
    public void ValidateIsNegative_WithNegativeValue_ReturnsValidResult()
    {
        var result = (-42).ValidateIsNegative();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsNegative_WithPositiveValue_ReturnsInvalidResult()
    {
        var result = 1.ValidateIsNegative();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsNegative.ValidatorName));
    }

    #endregion

    #region EnsureIsNegative Tests

    [Test]
    public void EnsureIsNegative_WithNegativeValue_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => (-1).EnsureIsNegative());
    }

    [Test]
    public void EnsureIsNegative_WithPositiveValue_ThrowsValidationException()
    {
        Assert.Throws<ValidationException>(() => 1.EnsureIsNegative());
    }

    [Test]
    public void EnsureIsNegative_WithDouble_Works()
    {
        Assert.DoesNotThrow(() => (-3.14).EnsureIsNegative());
        Assert.Throws<ValidationException>(() => 2.5.EnsureIsNegative());
    }

    #endregion
}
