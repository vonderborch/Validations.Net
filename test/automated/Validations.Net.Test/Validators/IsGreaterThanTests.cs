using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

/// <summary>
/// Unit tests for the IsGreaterThan validator class.
/// </summary>
[TestFixture]
public class IsGreaterThanTests
{
    [Test]
    public void CheckIsGreaterThan_WithGreaterValue_ReturnsTrue() => Assert.That(10.CheckIsGreaterThan(5), Is.True);

    [Test]
    public void CheckIsGreaterThan_WithEqualValue_ReturnsFalse() => Assert.That(5.CheckIsGreaterThan(5), Is.False);

    [Test]
    public void CheckIsGreaterThan_WithLessValue_ReturnsFalse() => Assert.That(3.CheckIsGreaterThan(5), Is.False);

    [Test]
    public void ValidateIsGreaterThan_WithGreaterValue_ReturnsValidResult()
    {
        var result = 10.ValidateIsGreaterThan(5);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsGreaterThan_WithLessValue_ReturnsInvalidResult()
    {
        var result = 3.ValidateIsGreaterThan(5);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsGreaterThan.ValidatorName));
    }

    [Test]
    public void EnsureIsGreaterThan_WithGreaterValue_DoesNotThrow() => Assert.DoesNotThrow(() => 10.EnsureIsGreaterThan(5));

    [Test]
    public void EnsureIsGreaterThan_WithLessValue_ThrowsValidationException()
    {
        var ex = Assert.Throws<ValidationException>(() => 3.EnsureIsGreaterThan(5));
        Assert.That(ex!.Validator, Is.EqualTo(IsGreaterThan.ValidatorName));
    }
}
