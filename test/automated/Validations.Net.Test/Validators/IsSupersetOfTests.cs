using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsSupersetOfTests
{
    [Test]
    public void CheckIsSupersetOf_WithSuperset_ReturnsTrue()
    {
        var source = new List<int> { 1, 2, 3 };
        var subset = new List<int> { 1, 2 };
        Assert.That(source.CheckIsSupersetOf(subset), Is.True);
    }

    [Test]
    public void CheckIsSupersetOf_WithNonSuperset_ReturnsFalse()
    {
        var source = new List<int> { 1, 2 };
        var subset = new List<int> { 1, 2, 4 };
        Assert.That(source.CheckIsSupersetOf(subset), Is.False);
    }

    [Test]
    public void CheckIsSupersetOf_WithNullSource_ReturnsFalse()
    {
        List<int>? nullList = null;
        var subset = new List<int> { 1 };
        Assert.That(nullList.CheckIsSupersetOf(subset), Is.False);
    }

    [Test]
    public void ValidateIsSupersetOf_WithSuperset_ReturnsValid()
    {
        var source = new List<int> { 1, 2, 3 };
        var subset = new List<int> { 1 };
        var result = source.ValidateIsSupersetOf(subset);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsSupersetOf_WithNonSuperset_ReturnsInvalid()
    {
        var source = new List<int> { 1, 2 };
        var subset = new List<int> { 99 };
        var result = source.ValidateIsSupersetOf(subset);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureIsSupersetOf_WithSuperset_DoesNotThrow()
    {
        var source = new List<int> { 1, 2 };
        var subset = new List<int> { 1 };
        Assert.DoesNotThrow(() => source.EnsureIsSupersetOf(subset));
    }

    [Test]
    public void EnsureIsSupersetOf_WithNonSuperset_Throws()
    {
        var source = new List<int> { 1, 2 };
        var subset = new List<int> { 99 };
        Assert.Throws<ValidationException>(() => source.EnsureIsSupersetOf(subset));
    }
}
