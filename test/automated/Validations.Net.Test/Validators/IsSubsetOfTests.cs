using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsSubsetOfTests
{
    [Test]
    public void CheckIsSubsetOf_WithSubset_ReturnsTrue()
    {
        var source = new List<int> { 1, 2 };
        var superset = new List<int> { 1, 2, 3 };
        Assert.That(source.CheckIsSubsetOf(superset), Is.True);
    }

    [Test]
    public void CheckIsSubsetOf_WithNonSubset_ReturnsFalse()
    {
        var source = new List<int> { 1, 4 };
        var superset = new List<int> { 1, 2, 3 };
        Assert.That(source.CheckIsSubsetOf(superset), Is.False);
    }

    [Test]
    public void CheckIsSubsetOf_WithNullSource_ReturnsTrue()
    {
        List<int>? nullList = null;
        var superset = new List<int> { 1, 2, 3 };
        Assert.That(nullList.CheckIsSubsetOf(superset), Is.True);
    }

    [Test]
    public void ValidateIsSubsetOf_WithSubset_ReturnsValid()
    {
        var source = new List<int> { 1 };
        var superset = new List<int> { 1, 2, 3 };
        var result = source.ValidateIsSubsetOf(superset);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsSubsetOf_WithNonSubset_ReturnsInvalid()
    {
        var source = new List<int> { 99 };
        var superset = new List<int> { 1, 2, 3 };
        var result = source.ValidateIsSubsetOf(superset);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureIsSubsetOf_WithSubset_DoesNotThrow()
    {
        var source = new List<int> { 1 };
        var superset = new List<int> { 1, 2 };
        Assert.DoesNotThrow(() => source.EnsureIsSubsetOf(superset));
    }

    [Test]
    public void EnsureIsSubsetOf_WithNonSubset_Throws()
    {
        var source = new List<int> { 99 };
        var superset = new List<int> { 1, 2 };
        Assert.Throws<ValidationException>(() => source.EnsureIsSubsetOf(superset));
    }
}
