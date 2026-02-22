using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

/// <summary>
/// Unit tests for the IsEquals validator class.
/// </summary>
[TestFixture]
public class IsEqualsTests
{
    #region CheckIsEquals Tests

    [Test]
    public void CheckIsEquals_WithEqualValues_ReturnsTrue()
    {
        Assert.That(42.CheckIsEquals(42), Is.True);
        Assert.That("test".CheckIsEquals("test"), Is.True);
    }

    [Test]
    public void CheckIsEquals_WithUnequalValues_ReturnsFalse()
    {
        Assert.That(42.CheckIsEquals(43), Is.False);
        Assert.That("test".CheckIsEquals("other"), Is.False);
    }

    [Test]
    public void CheckIsEquals_WithBothNull_ReturnsTrue()
    {
        string? a = null;
        string? b = null;
        Assert.That(a.CheckIsEquals(b), Is.True);
    }

    #endregion

    #region ValidateIsEquals Tests

    [Test]
    public void ValidateIsEquals_WithEqualValues_ReturnsValidResult()
    {
        var result = 42.ValidateIsEquals(42);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsEquals_WithUnequalValues_ReturnsInvalidResult()
    {
        var result = 42.ValidateIsEquals(43);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsEquals.ValidatorName));
    }

    [Test]
    public void ValidateIsEquals_WithCustomMessage_UsesCustomMessage()
    {
        var result = 42.ValidateIsEquals(43, validationFailureMessage: "Custom");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Custom"));
    }

    #endregion

    #region EnsureIsEquals Tests

    [Test]
    public void EnsureIsEquals_WithEqualValues_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => 42.EnsureIsEquals(42));
    }

    [Test]
    public void EnsureIsEquals_WithEqualValues_ReturnsValue()
    {
        var result = 42.EnsureIsEquals(42);
        Assert.That(result, Is.EqualTo(42));
    }

    [Test]
    public void EnsureIsEquals_WithUnequalValues_ThrowsValidationException()
    {
        var ex = Assert.Throws<ValidationException>(() => 42.EnsureIsEquals(43));
        Assert.That(ex!.Validator, Is.EqualTo(IsEquals.ValidatorName));
    }

    #endregion
}
