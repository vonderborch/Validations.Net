using Validations.Net.OLD;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.Validators;

/// <summary>
/// Unit tests for the IsNotInRange validator class.
/// </summary>
[TestFixture]
public class IsNotInRangeTests
{
    [Test]
    public void CheckIsNotInRange_WithValueOutOfRange_ReturnsTrue()
    {
        Assert.That(0.CheckIsNotInRange(1, 10), Is.True);
        Assert.That(11.CheckIsNotInRange(1, 10), Is.True);
    }

    [Test]
    public void CheckIsNotInRange_WithValueInRange_ReturnsFalse()
    {
        Assert.That(5.CheckIsNotInRange(1, 10), Is.False);
        Assert.That(1.CheckIsNotInRange(1, 10), Is.False);
        Assert.That(10.CheckIsNotInRange(1, 10), Is.False);
    }

    [Test]
    public void ValidateIsNotInRange_WithValueOutOfRange_ReturnsValidResult()
    {
        var result = 0.ValidateIsNotInRange(1, 10);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsNotInRange_WithValueInRange_ReturnsInvalidResult()
    {
        var result = 5.ValidateIsNotInRange(1, 10);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsNotInRange.ValidatorName));
    }

    [Test]
    public void EnsureIsNotInRange_WithValueOutOfRange_DoesNotThrow() => Assert.DoesNotThrow(() => 0.EnsureIsNotInRange(1, 10));

    [Test]
    public void EnsureIsNotInRange_WithValueInRange_ThrowsValidationException()
    {
        var ex = Assert.Throws<ValidationException>(() => 5.EnsureIsNotInRange(1, 10));
        Assert.That(ex!.Validator, Is.EqualTo(IsNotInRange.ValidatorName));
    }
}
