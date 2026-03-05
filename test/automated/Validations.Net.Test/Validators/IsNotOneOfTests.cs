using Validations.Net.OLD;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.Validators;

/// <summary>
/// Unit tests for the IsNotOneOf validator class.
/// </summary>
[TestFixture]
public class IsNotOneOfTests
{
    [Test]
    public void CheckIsNotOneOf_WithValueNotInParams_ReturnsTrue()
    {
        Assert.That(4.CheckIsNotOneOf(1, 2, 3), Is.True);
        Assert.That("x".CheckIsNotOneOf("a", "b", "c"), Is.True);
    }

    [Test]
    public void CheckIsNotOneOf_WithValueInParams_ReturnsFalse()
    {
        Assert.That(2.CheckIsNotOneOf(1, 2, 3), Is.False);
        Assert.That("b".CheckIsNotOneOf("a", "b", "c"), Is.False);
    }

    [Test]
    public void ValidateIsNotOneOf_WithValueNotInSet_ReturnsValidResult()
    {
        var result = 4.ValidateIsNotOneOf(1, 2, 3);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsNotOneOf_WithValueInSet_ReturnsInvalidResult()
    {
        var result = 2.ValidateIsNotOneOf(1, 2, 3);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsNotOneOf.ValidatorName));
    }

    [Test]
    public void EnsureIsNotOneOf_WithValueNotInSet_DoesNotThrow() => Assert.DoesNotThrow(() => 4.EnsureIsNotOneOf(1, 2, 3));

    [Test]
    public void EnsureIsNotOneOf_WithValueInSet_ThrowsValidationException()
    {
        var ex = Assert.Throws<ValidationException>(() => 2.EnsureIsNotOneOf(1, 2, 3));
        Assert.That(ex!.Validator, Is.EqualTo(IsNotOneOf.ValidatorName));
    }
}
