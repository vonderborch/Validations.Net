using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

/// <summary>
/// Unit tests for the IsLessThanOrEquals validator class.
/// </summary>
[TestFixture]
public class IsLessThanOrEqualsTests
{
    [Test]
    public void CheckIsLessThanOrEquals_WithLessValue_ReturnsTrue() => Assert.That(3.CheckIsLessThanOrEquals(5), Is.True);

    [Test]
    public void CheckIsLessThanOrEquals_WithEqualValue_ReturnsTrue() => Assert.That(5.CheckIsLessThanOrEquals(5), Is.True);

    [Test]
    public void CheckIsLessThanOrEquals_WithGreaterValue_ReturnsFalse() => Assert.That(10.CheckIsLessThanOrEquals(5), Is.False);

    [Test]
    public void ValidateIsLessThanOrEquals_WithEqualValue_ReturnsValidResult()
    {
        var result = 5.ValidateIsLessThanOrEquals(5);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsLessThanOrEquals_WithGreaterValue_ReturnsInvalidResult()
    {
        var result = 10.ValidateIsLessThanOrEquals(5);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsLessThanOrEquals.ValidatorName));
    }

    [Test]
    public void EnsureIsLessThanOrEquals_WithEqualValue_DoesNotThrow() => Assert.DoesNotThrow(() => 5.EnsureIsLessThanOrEquals(5));

    [Test]
    public void EnsureIsLessThanOrEquals_WithGreaterValue_ThrowsValidationException()
    {
        var ex = Assert.Throws<ValidationException>(() => 10.EnsureIsLessThanOrEquals(5));
        Assert.That(ex!.Validator, Is.EqualTo(IsLessThanOrEquals.ValidatorName));
    }
}
