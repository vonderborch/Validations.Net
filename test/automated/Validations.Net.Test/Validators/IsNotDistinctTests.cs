using Validations.Net.OLD;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNotDistinctTests
{
    [Test]
    public void CheckIsNotDistinct_WithDuplicateList_ReturnsTrue()
    {
        var list = new List<int> { 1, 2, 1 };
        Assert.That(list.CheckIsNotDistinct(), Is.True);
    }

    [Test]
    public void CheckIsNotDistinct_WithDistinctList_ReturnsFalse()
    {
        var list = new List<int> { 1, 2, 3 };
        Assert.That(list.CheckIsNotDistinct(), Is.False);
    }

    [Test]
    public void CheckIsNotDistinct_WithArray_ReturnsTrue()
    {
        int[] arr = [1, 2, 2, 3];
        Assert.That(arr.CheckIsNotDistinct(), Is.True);
    }

    [Test]
    public void ValidateIsNotDistinct_WithDuplicateList_ReturnsValid()
    {
        var list = new List<int> { 1, 1 };
        var result = list.ValidateIsNotDistinct();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsNotDistinct_WithDistinctList_ReturnsInvalid()
    {
        var list = new List<int> { 1, 2, 3 };
        var result = list.ValidateIsNotDistinct();
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureIsNotDistinct_WithDuplicateList_DoesNotThrow()
    {
        var list = new List<int> { 1, 2, 1 };
        Assert.DoesNotThrow(() => list.EnsureIsNotDistinct());
    }

    [Test]
    public void EnsureIsNotDistinct_WithDistinctList_Throws()
    {
        var list = new List<int> { 1, 2, 3 };
        Assert.Throws<ValidationException>(() => list.EnsureIsNotDistinct());
    }
}
