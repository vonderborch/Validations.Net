using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsEmptyTests
{
    [Test]
    public void CheckIsEmpty_WithEmptyString_ReturnsTrue()
    {
        Assert.That(string.Empty.CheckIsEmpty(), Is.True);
    }

    [Test]
    public void CheckIsEmpty_WithNonEmptyString_ReturnsFalse()
    {
        Assert.That("abc".CheckIsEmpty(), Is.False);
    }

    [Test]
    public void CheckIsEmpty_WithNullString_ReturnsFalse()
    {
        Assert.That(((string?)null).CheckIsEmpty(), Is.False);
    }

    [Test]
    public void CheckIsEmpty_WithEmptyList_ReturnsTrue()
    {
        System.Collections.ICollection list = new List<int>();
        Assert.That(list.CheckIsEmpty(), Is.True);
    }

    [Test]
    public void CheckIsEmpty_WithNonEmptyList_ReturnsFalse()
    {
        System.Collections.ICollection list = new List<int> { 1 };
        Assert.That(list.CheckIsEmpty(), Is.False);
    }

    [Test]
    public void CheckIsEmpty_WithEmptyEnumerable_ReturnsTrue()
    {
        IEnumerable<int> empty = Array.Empty<int>();
        Assert.That(empty.CheckIsEmpty(), Is.True);
    }

    [Test]
    public void ValidateIsEmpty_WithEmptyString_ReturnsValid()
    {
        var result = string.Empty.ValidateIsEmpty();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsEmpty_WithNonEmptyString_ReturnsInvalid()
    {
        var result = "x".ValidateIsEmpty();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsEmpty.ValidatorName));
    }

    [Test]
    public void EnsureIsEmpty_WithEmptyString_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => string.Empty.EnsureIsEmpty());
    }

    [Test]
    public void EnsureIsEmpty_WithNonEmptyString_Throws()
    {
        Assert.Throws<ValidationException>(() => "x".EnsureIsEmpty());
    }

    [Test]
    public void CheckIsEmpty_WithEmptySpan_ReturnsTrue()
    {
        ReadOnlySpan<int> span = [];
        Assert.That(IsEmpty.CheckIsEmpty(span), Is.True);
    }
}
