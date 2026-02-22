using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNotSubsetOfTests
{
    [Test]
    public void CheckIsNotSubsetOf_WithNonSubset_ReturnsTrue()
    {
        var source = new List<int> { 1, 4 };
        var superset = new List<int> { 1, 2, 3 };
        Assert.That(source.CheckIsNotSubsetOf(superset), Is.True);
    }

    [Test]
    public void CheckIsNotSubsetOf_WithSubset_ReturnsFalse()
    {
        var source = new List<int> { 1, 2 };
        var superset = new List<int> { 1, 2, 3 };
        Assert.That(source.CheckIsNotSubsetOf(superset), Is.False);
    }

    [Test]
    public void ValidateIsNotSubsetOf_WithNonSubset_ReturnsValid()
    {
        var source = new List<int> { 99 };
        var superset = new List<int> { 1, 2, 3 };
        var result = source.ValidateIsNotSubsetOf(superset);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsNotSubsetOf_WithSubset_ReturnsInvalid()
    {
        var source = new List<int> { 1 };
        var superset = new List<int> { 1, 2, 3 };
        var result = source.ValidateIsNotSubsetOf(superset);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureIsNotSubsetOf_WithNonSubset_DoesNotThrow()
    {
        var source = new List<int> { 99 };
        var superset = new List<int> { 1, 2 };
        Assert.DoesNotThrow(() => source.EnsureIsNotSubsetOf(superset));
    }

    [Test]
    public void EnsureIsNotSubsetOf_WithSubset_Throws()
    {
        var source = new List<int> { 1 };
        var superset = new List<int> { 1, 2 };
        Assert.Throws<ValidationException>(() => source.EnsureIsNotSubsetOf(superset));
    }
}
