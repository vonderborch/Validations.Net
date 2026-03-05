using Validations.Net.OLD;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsDistinctTests
{
    [Test]
    public void CheckIsDistinct_WithDistinctList_ReturnsTrue()
    {
        var list = new List<int> { 1, 2, 3 };
        Assert.That(list.CheckIsDistinct(), Is.True);
    }

    [Test]
    public void CheckIsDistinct_WithDuplicateList_ReturnsFalse()
    {
        var list = new List<int> { 1, 2, 1 };
        Assert.That(list.CheckIsDistinct(), Is.False);
    }

    [Test]
    public void CheckIsDistinct_WithNull_ReturnsFalse()
    {
        List<int>? nullList = null;
        Assert.That(nullList.CheckIsDistinct(), Is.False);
    }

    [Test]
    public void CheckIsDistinct_WithArray_ReturnsTrue()
    {
        int[] arr = [1, 2, 3, 4];
        Assert.That(arr.CheckIsDistinct(), Is.True);
    }

    [Test]
    public void CheckIsDistinct_WithArrayWithDuplicates_ReturnsFalse()
    {
        int[] arr = [1, 2, 2, 3];
        Assert.That(arr.CheckIsDistinct(), Is.False);
    }

    [Test]
    public void ValidateIsDistinct_WithDistinctList_ReturnsValid()
    {
        var list = new List<int> { 1, 2, 3 };
        var result = list.ValidateIsDistinct();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsDistinct_WithDuplicateList_ReturnsInvalid()
    {
        var list = new List<int> { 1, 1 };
        var result = list.ValidateIsDistinct();
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureIsDistinct_WithDistinctList_DoesNotThrow()
    {
        var list = new List<int> { 1, 2, 3 };
        Assert.DoesNotThrow(() => list.EnsureIsDistinct());
    }

    [Test]
    public void EnsureIsDistinct_WithDuplicateList_Throws()
    {
        var list = new List<int> { 1, 2, 1 };
        Assert.Throws<ValidationException>(() => list.EnsureIsDistinct());
    }
}
