using Validations.Net;
using Validations.Net.OLD;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNonZeroTests
{
    #region CheckIsNonZero Tests

    [Test]
    public void CheckIsNonZero_WithNonZeroInt_ReturnsTrue()
    {
        Assert.That(42.CheckIsNonZero(), Is.True);
    }

    [Test]
    public void CheckIsNonZero_WithZero_ReturnsFalse()
    {
        Assert.That(0.CheckIsNonZero(), Is.False);
    }

    [Test]
    public void CheckIsNonZero_WithNegativeInt_ReturnsTrue()
    {
        Assert.That((-1).CheckIsNonZero(), Is.True);
    }

    [Test]
    public void CheckIsNonZero_WithDouble_Works()
    {
        Assert.That(3.14.CheckIsNonZero(), Is.True);
        Assert.That(0.0.CheckIsNonZero(), Is.False);
    }

    #endregion

    #region ValidateIsNonZero Tests

    [Test]
    public void ValidateIsNonZero_WithNonZero_ReturnsValidResult()
    {
        var result = 1.ValidateIsNonZero();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsNonZero_WithZero_ReturnsInvalidResult()
    {
        var result = 0.ValidateIsNonZero();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsNonZero.ValidatorName));
    }

    #endregion

    #region EnsureIsNonZero Tests

    [Test]
    public void EnsureIsNonZero_WithNonZero_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => 1.EnsureIsNonZero());
    }

    [Test]
    public void EnsureIsNonZero_WithZero_ThrowsValidationException()
    {
        Assert.Throws<ValidationException>(() => 0.EnsureIsNonZero());
    }

    [Test]
    public void EnsureIsNonZero_ReturnsValue()
    {
        var result = 42.EnsureIsNonZero();
        Assert.That(result, Is.EqualTo(42));
    }

    #endregion
}
