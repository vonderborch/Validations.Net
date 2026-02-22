using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

/// <summary>
/// Unit tests for the IsSameAs validator class.
/// </summary>
[TestFixture]
public class IsSameAsTests
{
    [Test]
    public void CheckIsSameAs_WithSameReference_ReturnsTrue()
    {
        var obj = new object();
        Assert.That(obj.CheckIsSameAs(obj), Is.True);
    }

    [Test]
    public void CheckIsSameAs_WithBothNull_ReturnsTrue()
    {
        object? a = null;
        object? b = null;
        Assert.That(a.CheckIsSameAs(b), Is.True);
    }

    [Test]
    public void CheckIsSameAs_WithDifferentReferences_ReturnsFalse()
    {
        var a = new object();
        var b = new object();
        Assert.That(a.CheckIsSameAs(b), Is.False);
    }

    [Test]
    public void ValidateIsSameAs_WithSameReference_ReturnsValidResult()
    {
        var obj = new object();
        var result = obj.ValidateIsSameAs(obj);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsSameAs_WithDifferentReferences_ReturnsInvalidResult()
    {
        var a = new object();
        var b = new object();
        var result = a.ValidateIsSameAs(b);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsSameAs.ValidatorName));
    }

    [Test]
    public void EnsureIsSameAs_WithSameReference_DoesNotThrow()
    {
        var obj = new object();
        Assert.DoesNotThrow(() => obj.EnsureIsSameAs(obj));
    }

    [Test]
    public void EnsureIsSameAs_WithDifferentReferences_ThrowsValidationException()
    {
        var a = new object();
        var b = new object();
        var ex = Assert.Throws<ValidationException>(() => a.EnsureIsSameAs(b));
        Assert.That(ex!.Validator, Is.EqualTo(IsSameAs.ValidatorName));
    }
}
