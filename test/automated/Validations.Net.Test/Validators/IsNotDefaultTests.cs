using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

/// <summary>
/// Unit tests for the IsNotDefault validator class.
/// </summary>
[TestFixture]
public class IsNotDefaultTests
{
    [Test]
    public void CheckIsNotDefault_WithNonDefaultValue_ReturnsTrue()
    {
        Assert.That(42.CheckIsNotDefault(), Is.True);
        Assert.That("test".CheckIsNotDefault(), Is.True);
    }

    [Test]
    public void CheckIsNotDefault_WithDefaultValue_ReturnsFalse()
    {
        Assert.That(0.CheckIsNotDefault(), Is.False);
        string? n = null;
        Assert.That(n.CheckIsNotDefault(), Is.False);
    }

    [Test]
    public void ValidateIsNotDefault_WithNonDefaultValue_ReturnsValidResult()
    {
        var result = 42.ValidateIsNotDefault();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsNotDefault_WithDefaultValue_ReturnsInvalidResult()
    {
        var result = 0.ValidateIsNotDefault();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsNotDefault.ValidatorName));
    }

    [Test]
    public void EnsureIsNotDefault_WithNonDefaultValue_DoesNotThrow() => Assert.DoesNotThrow(() => 42.EnsureIsNotDefault());

    [Test]
    public void EnsureIsNotDefault_WithDefaultValue_ThrowsValidationException()
    {
        var ex = Assert.Throws<ValidationException>(() => 0.EnsureIsNotDefault());
        Assert.That(ex!.Validator, Is.EqualTo(IsNotDefault.ValidatorName));
    }
}
