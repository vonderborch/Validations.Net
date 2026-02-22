using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

/// <summary>
/// Unit tests for the IsInRange validator class.
/// </summary>
[TestFixture]
public class IsInRangeTests
{
    [Test]
    public void CheckIsInRange_WithValueInRange_ReturnsTrue()
    {
        Assert.That(5.CheckIsInRange(1, 10), Is.True);
        Assert.That(1.CheckIsInRange(1, 10), Is.True);
        Assert.That(10.CheckIsInRange(1, 10), Is.True);
    }

    [Test]
    public void CheckIsInRange_WithValueBelowMin_ReturnsFalse() => Assert.That(0.CheckIsInRange(1, 10), Is.False);

    [Test]
    public void CheckIsInRange_WithValueAboveMax_ReturnsFalse() => Assert.That(11.CheckIsInRange(1, 10), Is.False);

    [Test]
    public void CheckIsInRange_WithExclusiveBounds_RespectsExclusion()
    {
        Assert.That(1.CheckIsInRange(1, 10, minInclusive: false, maxInclusive: true), Is.False);
        Assert.That(10.CheckIsInRange(1, 10, minInclusive: true, maxInclusive: false), Is.False);
        Assert.That(5.CheckIsInRange(1, 10, minInclusive: false, maxInclusive: false), Is.True);
    }

    [Test]
    public void ValidateIsInRange_WithValueInRange_ReturnsValidResult()
    {
        var result = 5.ValidateIsInRange(1, 10);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsInRange_WithValueOutOfRange_ReturnsInvalidResult()
    {
        var result = 0.ValidateIsInRange(1, 10);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsInRange.ValidatorName));
    }

    [Test]
    public void EnsureIsInRange_WithValueInRange_DoesNotThrow() => Assert.DoesNotThrow(() => 5.EnsureIsInRange(1, 10));

    [Test]
    public void EnsureIsInRange_WithValueOutOfRange_ThrowsValidationException()
    {
        var ex = Assert.Throws<ValidationException>(() => 0.EnsureIsInRange(1, 10));
        Assert.That(ex!.Validator, Is.EqualTo(IsInRange.ValidatorName));
    }
}
