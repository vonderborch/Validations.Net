using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

/// <summary>
/// Unit tests for the IsNotEquals validator class.
/// </summary>
[TestFixture]
public class IsNotEqualsTests
{
    #region CheckIsNotEquals Tests

    [Test]
    public void CheckIsNotEquals_WithUnequalValues_ReturnsTrue()
    {
        Assert.That(42.CheckIsNotEquals(43), Is.True);
        Assert.That("test".CheckIsNotEquals("other"), Is.True);
    }

    [Test]
    public void CheckIsNotEquals_WithEqualValues_ReturnsFalse()
    {
        Assert.That(42.CheckIsNotEquals(42), Is.False);
        Assert.That("test".CheckIsNotEquals("test"), Is.False);
    }

    [Test]
    public void CheckIsNotEquals_WithNullAndNonNull_ReturnsTrue()
    {
        string? a = null;
        Assert.That(a.CheckIsNotEquals("x"), Is.True);
    }

    #endregion

    #region ValidateIsNotEquals Tests

    [Test]
    public void ValidateIsNotEquals_WithUnequalValues_ReturnsValidResult()
    {
        var result = 42.ValidateIsNotEquals(43);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsNotEquals_WithEqualValues_ReturnsInvalidResult()
    {
        var result = 42.ValidateIsNotEquals(42);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsNotEquals.ValidatorName));
    }

    [Test]
    public void ValidateIsNotEquals_WithCustomMessage_UsesCustomMessage()
    {
        var result = 42.ValidateIsNotEquals(42, validationFailureMessage: "Custom");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Custom"));
    }

    #endregion

    #region EnsureIsNotEquals Tests

    [Test]
    public void EnsureIsNotEquals_WithUnequalValues_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => 42.EnsureIsNotEquals(43));
    }

    [Test]
    public void EnsureIsNotEquals_WithUnequalValues_ReturnsValue()
    {
        var result = 42.EnsureIsNotEquals(43);
        Assert.That(result, Is.EqualTo(42));
    }

    [Test]
    public void EnsureIsNotEquals_WithEqualValues_ThrowsValidationException()
    {
        var ex = Assert.Throws<ValidationException>(() => 42.EnsureIsNotEquals(42));
        Assert.That(ex!.Validator, Is.EqualTo(IsNotEquals.ValidatorName));
    }

    #endregion
}
