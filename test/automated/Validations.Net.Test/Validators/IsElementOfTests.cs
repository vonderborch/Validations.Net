using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsElementOfTests
{
    [Test]
    public void CheckIsElementOf_Collection_WithNullCollection_ReturnsFalse() =>
        Assert.That(1.CheckIsElementOf((List<int>?)null), Is.False);

    [Test]
    public void CheckIsElementOf_Collection_WithContainedItem_ReturnsTrue()
    {
        var list = new List<int> { 1, 2, 3 };
        Assert.That(2.CheckIsElementOf(list), Is.True);
    }

    [Test]
    public void CheckIsElementOf_Collection_WithMissingItem_ReturnsFalse()
    {
        var list = new List<int> { 1, 2, 3 };
        Assert.That(4.CheckIsElementOf(list), Is.False);
    }

    [Test]
    public void CheckIsElementOf_Collection_WithNullValue_WhenNullInCollection_ReturnsTrue()
    {
        var list = new List<string?> { "a", null, "c" };
        Assert.That(((string?)null).CheckIsElementOf(list), Is.True);
    }

    [Test]
    public void CheckIsElementOf_Collection_WithNullValue_WhenNullNotInCollection_ReturnsFalse()
    {
        var list = new List<string?> { "a", "b", "c" };
        Assert.That(((string?)null).CheckIsElementOf(list), Is.False);
    }

    [Test]
    public void CheckIsElementOf_String_WithNullValue_ReturnsFalse() =>
        Assert.That(((string?)null).CheckIsElementOf("sub"), Is.False);

    [Test]
    public void CheckIsElementOf_String_WithContainedSubstring_ReturnsTrue() =>
        Assert.That("hello world".CheckIsElementOf("world"), Is.True);

    [Test]
    public void CheckIsElementOf_String_WithMissingSubstring_ReturnsFalse() =>
        Assert.That("hello".CheckIsElementOf("xyz"), Is.False);

    [Test]
    public void CheckIsElementOf_String_WithComparison_RespectsCase()
    {
        Assert.That("Hello".CheckIsElementOf("ell", StringComparison.Ordinal), Is.True);
        Assert.That("Hello".CheckIsElementOf("ELL", StringComparison.Ordinal), Is.False);
        Assert.That("Hello".CheckIsElementOf("ELL", StringComparison.OrdinalIgnoreCase), Is.True);
    }

    [Test]
    public void ValidateIsElementOf_Collection_WithContainedItem_ReturnsValid()
    {
        var list = new[] { "a", "b", "c" };
        Assert.That("b".ValidateIsElementOf(list).IsValid, Is.True);
    }

    [Test]
    public void ValidateIsElementOf_Collection_WithMissingItem_ReturnsInvalid()
    {
        var result = 5.ValidateIsElementOf(new List<int> { 1, 2 });
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsElementOf.ValidatorName));
    }

    [Test]
    public void ValidateIsElementOf_String_WithContainedSubstring_ReturnsValid() =>
        Assert.That("abc".ValidateIsElementOf("b").IsValid, Is.True);

    [Test]
    public void EnsureIsElementOf_Collection_WithContainedItem_DoesNotThrow() =>
        Assert.DoesNotThrow(() => 2.EnsureIsElementOf(new List<int> { 1, 2, 3 }));

    [Test]
    public void EnsureIsElementOf_Collection_WithMissingItem_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => 5.EnsureIsElementOf(new List<int> { 1, 2 }));
        Assert.That(ex!.Validator, Is.EqualTo(IsElementOf.ValidatorName));
    }

    [Test]
    public void EnsureIsElementOf_String_WithContainedSubstring_DoesNotThrow() =>
        Assert.DoesNotThrow(() => "hello".EnsureIsElementOf("ell"));
}
