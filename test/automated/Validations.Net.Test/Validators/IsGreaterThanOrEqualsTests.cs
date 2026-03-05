using Validations.Net.OLD;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.Validators;

/// <summary>
/// Unit tests for the IsGreaterThanOrEquals validator class.
/// </summary>
[TestFixture]
public class IsGreaterThanOrEqualsTests
{
    [Test]
    public void CheckIsGreaterThanOrEquals_WithGreaterValue_ReturnsTrue() => Assert.That(10.CheckIsGreaterThanOrEquals(5), Is.True);

    [Test]
    public void CheckIsGreaterThanOrEquals_WithEqualValue_ReturnsTrue() => Assert.That(5.CheckIsGreaterThanOrEquals(5), Is.True);

    [Test]
    public void CheckIsGreaterThanOrEquals_WithLessValue_ReturnsFalse() => Assert.That(3.CheckIsGreaterThanOrEquals(5), Is.False);

    [Test]
    public void ValidateIsGreaterThanOrEquals_WithEqualValue_ReturnsValidResult()
    {
        var result = 5.ValidateIsGreaterThanOrEquals(5);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsGreaterThanOrEquals_WithLessValue_ReturnsInvalidResult()
    {
        var result = 3.ValidateIsGreaterThanOrEquals(5);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsGreaterThanOrEquals.ValidatorName));
    }

    [Test]
    public void EnsureIsGreaterThanOrEquals_WithEqualValue_DoesNotThrow() => Assert.DoesNotThrow(() => 5.EnsureIsGreaterThanOrEquals(5));

    [Test]
    public void EnsureIsGreaterThanOrEquals_WithLessValue_ThrowsValidationException()
    {
        var ex = Assert.Throws<ValidationException>(() => 3.EnsureIsGreaterThanOrEquals(5));
        Assert.That(ex!.Validator, Is.EqualTo(IsGreaterThanOrEquals.ValidatorName));
    }
}
