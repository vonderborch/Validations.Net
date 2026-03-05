using Validations.Net.OLD;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNotSameAsTests
{
    [Test]
    public void CheckIsNotSameAs_WithDifferentReferences_ReturnsTrue()
    {
        var a = new object();
        var b = new object();
        Assert.That(a.CheckIsNotSameAs(b), Is.True);
    }

    [Test]
    public void CheckIsNotSameAs_WithSameReference_ReturnsFalse()
    {
        var obj = new object();
        Assert.That(obj.CheckIsNotSameAs(obj), Is.False);
    }

    [Test]
    public void CheckIsNotSameAs_WithBothNull_ReturnsFalse()
    {
        object? a = null;
        object? b = null;
        Assert.That(a.CheckIsNotSameAs(b), Is.False);
    }

    [Test]
    public void CheckIsNotSameAs_WithOneNull_ReturnsTrue()
    {
        var a = new object();
        object? b = null;
        Assert.That(a.CheckIsNotSameAs(b), Is.True);
        Assert.That(b.CheckIsNotSameAs(a), Is.True);
    }

    [Test]
    public void ValidateIsNotSameAs_WithDifferentReferences_ReturnsValidResult()
    {
        var a = new object();
        var b = new object();
        var result = a.ValidateIsNotSameAs(b);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsNotSameAs_WithSameReference_ReturnsInvalidResult()
    {
        var obj = new object();
        var result = obj.ValidateIsNotSameAs(obj);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsNotSameAs.ValidatorName));
    }

    [Test]
    public void EnsureIsNotSameAs_WithDifferentReferences_DoesNotThrow()
    {
        var a = new object();
        var b = new object();
        Assert.DoesNotThrow(() => a.EnsureIsNotSameAs(b));
    }

    [Test]
    public void EnsureIsNotSameAs_WithSameReference_ThrowsValidationException()
    {
        var obj = new object();
        var ex = Assert.Throws<ValidationException>(() => obj.EnsureIsNotSameAs(obj));
        Assert.That(ex!.Validator, Is.EqualTo(IsNotSameAs.ValidatorName));
    }
}
