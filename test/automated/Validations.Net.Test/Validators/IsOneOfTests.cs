using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

/// <summary>
/// Unit tests for the IsOneOf validator class.
/// </summary>
[TestFixture]
public class IsOneOfTests
{
    [Test]
    public void CheckIsOneOf_WithValueInParams_ReturnsTrue()
    {
        Assert.That(2.CheckIsOneOf(1, 2, 3), Is.True);
        Assert.That("b".CheckIsOneOf("a", "b", "c"), Is.True);
    }

    [Test]
    public void CheckIsOneOf_WithValueNotInParams_ReturnsFalse()
    {
        Assert.That(4.CheckIsOneOf(1, 2, 3), Is.False);
        Assert.That("x".CheckIsOneOf("a", "b", "c"), Is.False);
    }

    [Test]
    public void CheckIsOneOf_WithIEnumerable_ReturnsTrueWhenMatch()
    {
        Assert.That(2.CheckIsOneOf(new[] { 1, 2, 3 }), Is.True);
    }

    [Test]
    public void ValidateIsOneOf_WithValueInSet_ReturnsValidResult()
    {
        var result = 2.ValidateIsOneOf(1, 2, 3);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsOneOf_WithValueNotInSet_ReturnsInvalidResult()
    {
        var result = 4.ValidateIsOneOf(1, 2, 3);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsOneOf.ValidatorName));
    }

    [Test]
    public void EnsureIsOneOf_WithValueInSet_DoesNotThrow() => Assert.DoesNotThrow(() => 2.EnsureIsOneOf(1, 2, 3));

    [Test]
    public void EnsureIsOneOf_WithValueNotInSet_ThrowsValidationException()
    {
        var ex = Assert.Throws<ValidationException>(() => 4.EnsureIsOneOf(1, 2, 3));
        Assert.That(ex!.Validator, Is.EqualTo(IsOneOf.ValidatorName));
    }
}
