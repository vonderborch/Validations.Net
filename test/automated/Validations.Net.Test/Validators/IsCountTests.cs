using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsCountTests
{
    [Test]
    public void CheckIsCount_WithExactCountList_ReturnsTrue()
    {
        var list = new List<int> { 1, 2, 3 };
        Assert.That(list.CheckIsCount(3), Is.True);
    }

    [Test]
    public void CheckIsCount_WithWrongCountList_ReturnsFalse()
    {
        var list = new List<int> { 1, 2 };
        Assert.That(list.CheckIsCount(3), Is.False);
    }

    [Test]
    public void CheckIsCount_WithNullList_ReturnsFalse()
    {
        List<int>? nullList = null;
        Assert.That(nullList.CheckIsCount(0), Is.False);
    }

    [Test]
    public void CheckIsCount_WithEnumerable_ReturnsTrue()
    {
        int[] arr = [1, 2, 3];
        Assert.That(arr.CheckIsCount(3), Is.True);
    }

    [Test]
    public void CheckIsCount_WithEnumerableExceedingCount_ReturnsFalse()
    {
        int[] arr = [1, 2, 3, 4];
        Assert.That(arr.CheckIsCount(3), Is.False);
    }

    [Test]
    public void ValidateIsCount_WithExactCount_ReturnsValid()
    {
        var list = new List<int> { 1, 2 };
        var result = list.ValidateIsCount(2);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsCount_WithWrongCount_ReturnsInvalid()
    {
        var list = new List<int> { 1 };
        var result = list.ValidateIsCount(2);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureIsCount_WithExactCount_DoesNotThrow()
    {
        var list = new List<int> { 1 };
        Assert.DoesNotThrow(() => list.EnsureIsCount(1));
    }

    [Test]
    public void EnsureIsCount_WithWrongCount_Throws()
    {
        var list = new List<int> { 1, 2 };
        Assert.Throws<ValidationException>(() => list.EnsureIsCount(1));
    }
}
