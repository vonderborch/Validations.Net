using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsSingleTests
{
    [Test]
    public void CheckIsSingle_WithSingleCharString_ReturnsTrue()
    {
        Assert.That("x".CheckIsSingle(), Is.True);
    }

    [Test]
    public void CheckIsSingle_WithMultiCharString_ReturnsFalse()
    {
        Assert.That("xy".CheckIsSingle(), Is.False);
    }

    [Test]
    public void CheckIsSingle_WithEmptyString_ReturnsFalse()
    {
        Assert.That(string.Empty.CheckIsSingle(), Is.False);
    }

    [Test]
    public void CheckIsSingle_WithSingleElementList_ReturnsTrue()
    {
        var list = new List<int> { 1 };
        Assert.That(list.CheckIsSingle(), Is.True);
    }

    [Test]
    public void CheckIsSingle_WithMultiElementList_ReturnsFalse()
    {
        var list = new List<int> { 1, 2 };
        Assert.That(list.CheckIsSingle(), Is.False);
    }

    [Test]
    public void CheckIsSingle_WithSingleElementEnumerable_ReturnsTrue()
    {
        int[] arr = [42];
        Assert.That(arr.CheckIsSingle(), Is.True);
    }

    [Test]
    public void ValidateIsSingle_WithSingleElement_ReturnsValid()
    {
        var result = "x".ValidateIsSingle();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsSingle_WithMultipleElements_ReturnsInvalid()
    {
        var list = new List<int> { 1, 2 };
        var result = list.ValidateIsSingle();
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureIsSingle_WithSingleElement_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => "x".EnsureIsSingle());
    }

    [Test]
    public void EnsureIsSingle_WithMultipleElements_Throws()
    {
        var list = new List<int> { 1, 2 };
        Assert.Throws<ValidationException>(() => list.EnsureIsSingle());
    }
}
