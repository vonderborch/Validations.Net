using Validations.Net.OLD;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsAssignableToTests
{
    private class BaseClass { }
    private class DerivedClass : BaseClass { }

    [Test]
    public void CheckIsAssignableTo_WithMatchingType_ReturnsTrue()
    {
        var obj = new DerivedClass();
        Assert.That(obj.CheckIsAssignableTo<BaseClass>(), Is.True);
    }

    [Test]
    public void CheckIsAssignableTo_WithExactType_ReturnsTrue()
    {
        var obj = new DerivedClass();
        Assert.That(obj.CheckIsAssignableTo<DerivedClass>(), Is.True);
    }

    [Test]
    public void CheckIsAssignableTo_WithNull_ReturnsFalse()
    {
        object? obj = null;
        Assert.That(obj.CheckIsAssignableTo<object>(), Is.False);
    }

    [Test]
    public void CheckIsAssignableTo_WithWrongType_ReturnsFalse()
    {
        var obj = new BaseClass();
        Assert.That(obj.CheckIsAssignableTo<DerivedClass>(), Is.False);
    }

    [Test]
    public void CheckIsAssignableTo_WithTypeParameter_ReturnsTrue()
    {
        var obj = new DerivedClass();
        Assert.That(obj.CheckIsAssignableTo(typeof(BaseClass)), Is.True);
    }

    [Test]
    public void ValidateIsAssignableTo_WithMatchingType_ReturnsValid()
    {
        var obj = new DerivedClass();
        var result = obj.ValidateIsAssignableTo<BaseClass>();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsAssignableTo_WithWrongType_ReturnsInvalid()
    {
        var obj = new BaseClass();
        var result = obj.ValidateIsAssignableTo<DerivedClass>();
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureIsAssignableTo_WithMatchingType_DoesNotThrow()
    {
        var obj = new DerivedClass();
        Assert.DoesNotThrow(() => obj.EnsureIsAssignableTo<BaseClass>());
    }

    [Test]
    public void EnsureIsAssignableTo_WithWrongType_Throws()
    {
        var obj = new BaseClass();
        Assert.Throws<ValidationException>(() => obj.EnsureIsAssignableTo<DerivedClass>());
    }
}
