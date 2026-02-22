using Validations.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsPositiveTests
{
    #region CheckIsPositive Tests

    [Test]
    public void CheckIsPositive_WithPositiveInt_ReturnsTrue()
    {
        Assert.That(42.CheckIsPositive(), Is.True);
    }

    [Test]
    public void CheckIsPositive_WithNegativeInt_ReturnsFalse()
    {
        Assert.That((-42).CheckIsPositive(), Is.False);
    }

    [Test]
    public void CheckIsPositive_WithZero_ReturnsTrue()
    {
        // .NET's INumber<T>.IsPositive considers 0 as positive (non-negative)
        Assert.That(0.CheckIsPositive(), Is.True);
    }

    [Test]
    public void CheckIsPositive_WithPositiveDouble_ReturnsTrue()
    {
        Assert.That(3.14.CheckIsPositive(), Is.True);
    }

    [Test]
    public void CheckIsPositive_WithNegativeDecimal_ReturnsFalse()
    {
        Assert.That((-1.5m).CheckIsPositive(), Is.False);
    }

    #endregion

    #region ValidateIsPositive Tests

    [Test]
    public void ValidateIsPositive_WithPositiveValue_ReturnsValidResult()
    {
        var result = 42.ValidateIsPositive();
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsPositive_WithNegativeValue_ReturnsInvalidResult()
    {
        var result = (-1).ValidateIsPositive();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsPositive.ValidatorName));
    }

    [Test]
    public void ValidateIsPositive_WithCustomMessage_UsesCustomMessage()
    {
        var result = (-1).ValidateIsPositive(validationFailureMessage: "Custom");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Custom"));
    }

    #endregion

    #region EnsureIsPositive Tests

    [Test]
    public void EnsureIsPositive_WithPositiveValue_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => 42.EnsureIsPositive());
    }

    [Test]
    public void EnsureIsPositive_WithPositiveValue_ReturnsValue()
    {
        var result = 42.EnsureIsPositive();
        Assert.That(result, Is.EqualTo(42));
    }

    [Test]
    public void EnsureIsPositive_WithNegativeValue_ThrowsValidationException()
    {
        var ex = Assert.Throws<ValidationException>(() => (-1).EnsureIsPositive());
        Assert.That(ex!.Validator, Is.EqualTo(IsPositive.ValidatorName));
    }

    [Test]
    public void EnsureIsPositive_WithDouble_Works()
    {
        Assert.DoesNotThrow(() => 3.14.EnsureIsPositive());
        Assert.Throws<ValidationException>(() => (-2.5).EnsureIsPositive());
    }

    [Test]
    public void EnsureIsPositive_WithDecimal_Works()
    {
        Assert.DoesNotThrow(() => 1.5m.EnsureIsPositive());
        Assert.Throws<ValidationException>(() => (-0.1m).EnsureIsPositive());
    }

    #endregion
}
