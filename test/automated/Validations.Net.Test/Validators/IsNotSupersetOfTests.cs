using Validations.Net.OLD;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNotSupersetOfTests
{
    [Test]
    public void CheckIsNotSupersetOf_WithNonSuperset_ReturnsTrue()
    {
        var source = new List<int> { 1, 2 };
        var subset = new List<int> { 1, 2, 4 };
        Assert.That(source.CheckIsNotSupersetOf(subset), Is.True);
    }

    [Test]
    public void CheckIsNotSupersetOf_WithSuperset_ReturnsFalse()
    {
        var source = new List<int> { 1, 2, 3 };
        var subset = new List<int> { 1, 2 };
        Assert.That(source.CheckIsNotSupersetOf(subset), Is.False);
    }

    [Test]
    public void ValidateIsNotSupersetOf_WithNonSuperset_ReturnsValid()
    {
        var source = new List<int> { 1, 2 };
        var subset = new List<int> { 99 };
        var result = source.ValidateIsNotSupersetOf(subset);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsNotSupersetOf_WithSuperset_ReturnsInvalid()
    {
        var source = new List<int> { 1, 2, 3 };
        var subset = new List<int> { 1 };
        var result = source.ValidateIsNotSupersetOf(subset);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureIsNotSupersetOf_WithNonSuperset_DoesNotThrow()
    {
        var source = new List<int> { 1, 2 };
        var subset = new List<int> { 99 };
        Assert.DoesNotThrow(() => source.EnsureIsNotSupersetOf(subset));
    }

    [Test]
    public void EnsureIsNotSupersetOf_WithSuperset_Throws()
    {
        var source = new List<int> { 1, 2 };
        var subset = new List<int> { 1 };
        Assert.Throws<ValidationException>(() => source.EnsureIsNotSupersetOf(subset));
    }
}
