using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsInstanceOfTests
{
    private class BaseClass { }
    private class DerivedClass : BaseClass { }

    [Test]
    public void CheckIsInstanceOf_WithExactType_ReturnsTrue()
    {
        var obj = new DerivedClass();
        Assert.That(obj.CheckIsInstanceOf<DerivedClass>(), Is.True);
    }

    [Test]
    public void CheckIsInstanceOf_WithDerivedType_ReturnsFalse()
    {
        var obj = new DerivedClass();
        Assert.That(obj.CheckIsInstanceOf<BaseClass>(), Is.False);
    }

    [Test]
    public void CheckIsInstanceOf_WithNull_ReturnsFalse()
    {
        object? obj = null;
        Assert.That(obj.CheckIsInstanceOf<object>(), Is.False);
    }

    [Test]
    public void CheckIsInstanceOf_WithWrongType_ReturnsFalse()
    {
        var obj = new BaseClass();
        Assert.That(obj.CheckIsInstanceOf<DerivedClass>(), Is.False);
    }

    [Test]
    public void CheckIsInstanceOf_WithTypeParameter_ReturnsTrue()
    {
        var obj = new DerivedClass();
        Assert.That(obj.CheckIsInstanceOf(typeof(DerivedClass)), Is.True);
    }

    [Test]
    public void ValidateIsInstanceOf_WithExactType_ReturnsValid()
    {
        var obj = new DerivedClass();
        var result = obj.ValidateIsInstanceOf<DerivedClass>();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsInstanceOf_WithDerivedType_ReturnsInvalid()
    {
        var obj = new DerivedClass();
        var result = obj.ValidateIsInstanceOf<BaseClass>();
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureIsInstanceOf_WithExactType_DoesNotThrow()
    {
        var obj = new DerivedClass();
        Assert.DoesNotThrow(() => obj.EnsureIsInstanceOf<DerivedClass>());
    }

    [Test]
    public void EnsureIsInstanceOf_WithWrongType_Throws()
    {
        var obj = new BaseClass();
        Assert.Throws<ValidationException>(() => obj.EnsureIsInstanceOf<DerivedClass>());
    }
}
