using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsSortedTests
{
    [Test]
    public void CheckIsSorted_WithAscendingList_ReturnsTrue()
    {
        var list = new List<int> { 1, 2, 3 };
        Assert.That(list.CheckIsSorted(), Is.True);
    }

    [Test]
    public void CheckIsSorted_WithDescendingList_ReturnsFalse()
    {
        var list = new List<int> { 3, 2, 1 };
        Assert.That(list.CheckIsSorted(), Is.False);
    }

    [Test]
    public void CheckIsSorted_WithDescendingFlag_ReturnsTrue()
    {
        var list = new List<int> { 3, 2, 1 };
        Assert.That(list.CheckIsSorted(descending: true), Is.True);
    }

    [Test]
    public void CheckIsSorted_WithUnsortedList_ReturnsFalse()
    {
        var list = new List<int> { 1, 3, 2 };
        Assert.That(list.CheckIsSorted(), Is.False);
    }

    [Test]
    public void CheckIsSorted_WithEmptyList_ReturnsTrue()
    {
        var list = new List<int>();
        Assert.That(list.CheckIsSorted(), Is.True);
    }

    [Test]
    public void CheckIsSorted_WithNull_ReturnsFalse()
    {
        List<int>? nullList = null;
        Assert.That(nullList.CheckIsSorted(), Is.False);
    }

    [Test]
    public void CheckIsSorted_WithArray_ReturnsTrue()
    {
        int[] arr = [1, 2, 3, 4];
        Assert.That(arr.CheckIsSorted(), Is.True);
    }

    [Test]
    public void ValidateIsSorted_WithSortedList_ReturnsValid()
    {
        var list = new List<int> { 1, 2, 3 };
        var result = list.ValidateIsSorted();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsSorted_WithUnsortedList_ReturnsInvalid()
    {
        var list = new List<int> { 1, 3, 2 };
        var result = list.ValidateIsSorted();
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureIsSorted_WithSortedList_DoesNotThrow()
    {
        var list = new List<int> { 1, 2, 3 };
        Assert.DoesNotThrow(() => list.EnsureIsSorted());
    }

    [Test]
    public void EnsureIsSorted_WithUnsortedList_Throws()
    {
        var list = new List<int> { 1, 3, 2 };
        Assert.Throws<ValidationException>(() => list.EnsureIsSorted());
    }
}
