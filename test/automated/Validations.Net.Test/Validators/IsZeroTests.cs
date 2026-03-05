using Validations.Net;
using Validations.Net.OLD;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsZeroTests
{
    #region CheckIsZero Tests

    [Test]
    public void CheckIsZero_WithZeroInt_ReturnsTrue()
    {
        Assert.That(0.CheckIsZero(), Is.True);
    }

    [Test]
    public void CheckIsZero_WithNonZeroInt_ReturnsFalse()
    {
        Assert.That(42.CheckIsZero(), Is.False);
    }

    [Test]
    public void CheckIsZero_WithZeroDouble_ReturnsTrue()
    {
        Assert.That(0.0.CheckIsZero(), Is.True);
    }

    [Test]
    public void CheckIsZero_WithZeroDecimal_ReturnsTrue()
    {
        Assert.That(0m.CheckIsZero(), Is.True);
    }

    #endregion

    #region ValidateIsZero Tests

    [Test]
    public void ValidateIsZero_WithZero_ReturnsValidResult()
    {
        var result = 0.ValidateIsZero();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsZero_WithNonZero_ReturnsInvalidResult()
    {
        var result = 1.ValidateIsZero();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsZero.ValidatorName));
    }

    #endregion

    #region EnsureIsZero Tests

    [Test]
    public void EnsureIsZero_WithZero_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => 0.EnsureIsZero());
    }

    [Test]
    public void EnsureIsZero_WithNonZero_ThrowsValidationException()
    {
        Assert.Throws<ValidationException>(() => 1.EnsureIsZero());
    }

    [Test]
    public void EnsureIsZero_WithDecimal_Works()
    {
        Assert.DoesNotThrow(() => 0m.EnsureIsZero());
        Assert.Throws<ValidationException>(() => 0.1m.EnsureIsZero());
    }

    #endregion
}
