using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNotSortedTests
{
    [Test]
    public void CheckIsNotSorted_WithUnsortedList_ReturnsTrue()
    {
        var list = new List<int> { 1, 3, 2 };
        Assert.That(list.CheckIsNotSorted(), Is.True);
    }

    [Test]
    public void CheckIsNotSorted_WithAscendingList_ReturnsFalse()
    {
        var list = new List<int> { 1, 2, 3 };
        Assert.That(list.CheckIsNotSorted(), Is.False);
    }

    [Test]
    public void CheckIsNotSorted_WithDescendingListWhenCheckingAscending_ReturnsTrue()
    {
        var list = new List<int> { 3, 2, 1 };
        Assert.That(list.CheckIsNotSorted(descending: false), Is.True);
    }

    [Test]
    public void CheckIsNotSorted_WithArray_ReturnsTrue()
    {
        int[] arr = [2, 1, 3];
        Assert.That(arr.CheckIsNotSorted(), Is.True);
    }

    [Test]
    public void ValidateIsNotSorted_WithUnsortedList_ReturnsValid()
    {
        var list = new List<int> { 1, 3, 2 };
        var result = list.ValidateIsNotSorted();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsNotSorted_WithSortedList_ReturnsInvalid()
    {
        var list = new List<int> { 1, 2, 3 };
        var result = list.ValidateIsNotSorted();
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureIsNotSorted_WithUnsortedList_DoesNotThrow()
    {
        var list = new List<int> { 1, 3, 2 };
        Assert.DoesNotThrow(() => list.EnsureIsNotSorted());
    }

    [Test]
    public void EnsureIsNotSorted_WithSortedList_Throws()
    {
        var list = new List<int> { 1, 2, 3 };
        Assert.Throws<ValidationException>(() => list.EnsureIsNotSorted());
    }
}
