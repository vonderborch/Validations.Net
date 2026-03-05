using Validations.Net.OLD;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.Validators;

/// <summary>
/// Unit tests for the IsLessThan validator class.
/// </summary>
[TestFixture]
public class IsLessThanTests
{
    [Test]
    public void CheckIsLessThan_WithLessValue_ReturnsTrue() => Assert.That(3.CheckIsLessThan(5), Is.True);

    [Test]
    public void CheckIsLessThan_WithEqualValue_ReturnsFalse() => Assert.That(5.CheckIsLessThan(5), Is.False);

    [Test]
    public void CheckIsLessThan_WithGreaterValue_ReturnsFalse() => Assert.That(10.CheckIsLessThan(5), Is.False);

    [Test]
    public void ValidateIsLessThan_WithLessValue_ReturnsValidResult()
    {
        var result = 3.ValidateIsLessThan(5);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsLessThan_WithGreaterValue_ReturnsInvalidResult()
    {
        var result = 10.ValidateIsLessThan(5);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsLessThan.ValidatorName));
    }

    [Test]
    public void EnsureIsLessThan_WithLessValue_DoesNotThrow() => Assert.DoesNotThrow(() => 3.EnsureIsLessThan(5));

    [Test]
    public void EnsureIsLessThan_WithGreaterValue_ThrowsValidationException()
    {
        var ex = Assert.Throws<ValidationException>(() => 10.EnsureIsLessThan(5));
        Assert.That(ex!.Validator, Is.EqualTo(IsLessThan.ValidatorName));
    }
}
