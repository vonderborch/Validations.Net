using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

/// <summary>
/// Unit tests for the IsDefault validator class.
/// </summary>
[TestFixture]
public class IsDefaultTests
{
    [Test]
    public void CheckIsDefault_WithDefaultValue_ReturnsTrue()
    {
        Assert.That(0.CheckIsDefault(), Is.True);
        Assert.That(default(int).CheckIsDefault(), Is.True);
        string? n = null;
        Assert.That(n.CheckIsDefault(), Is.True);
    }

    [Test]
    public void CheckIsDefault_WithNonDefaultValue_ReturnsFalse()
    {
        Assert.That(42.CheckIsDefault(), Is.False);
        Assert.That("test".CheckIsDefault(), Is.False);
    }

    [Test]
    public void ValidateIsDefault_WithDefaultValue_ReturnsValidResult()
    {
        var result = 0.ValidateIsDefault();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsDefault_WithNonDefaultValue_ReturnsInvalidResult()
    {
        var result = 42.ValidateIsDefault();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsDefault.ValidatorName));
    }

    [Test]
    public void EnsureIsDefault_WithDefaultValue_DoesNotThrow() => Assert.DoesNotThrow(() => 0.EnsureIsDefault());

    [Test]
    public void EnsureIsDefault_WithNonDefaultValue_ThrowsValidationException()
    {
        var ex = Assert.Throws<ValidationException>(() => 42.EnsureIsDefault());
        Assert.That(ex!.Validator, Is.EqualTo(IsDefault.ValidatorName));
    }
}
