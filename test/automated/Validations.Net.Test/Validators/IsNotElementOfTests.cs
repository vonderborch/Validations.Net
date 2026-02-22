using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNotElementOfTests
{
    [Test]
    public void CheckIsNotElementOf_Collection_WithNullCollection_ReturnsTrue() =>
        Assert.That(1.CheckIsNotElementOf((List<int>?)null), Is.True);

    [Test]
    public void CheckIsNotElementOf_Collection_WithContainedItem_ReturnsFalse()
    {
        var list = new List<int> { 1, 2, 3 };
        Assert.That(2.CheckIsNotElementOf(list), Is.False);
    }

    [Test]
    public void CheckIsNotElementOf_Collection_WithMissingItem_ReturnsTrue()
    {
        var list = new List<int> { 1, 2, 3 };
        Assert.That(4.CheckIsNotElementOf(list), Is.True);
    }

    [Test]
    public void CheckIsNotElementOf_String_WithNullValue_ReturnsTrue() =>
        Assert.That(((string?)null).CheckIsNotElementOf("sub"), Is.True);

    [Test]
    public void CheckIsNotElementOf_String_WithContainedSubstring_ReturnsFalse() =>
        Assert.That("hello world".CheckIsNotElementOf("world"), Is.False);

    [Test]
    public void CheckIsNotElementOf_String_WithMissingSubstring_ReturnsTrue() =>
        Assert.That("hello".CheckIsNotElementOf("xyz"), Is.True);

    [Test]
    public void ValidateIsNotElementOf_Collection_WithMissingItem_ReturnsValid()
    {
        var list = new[] { "a", "b", "c" };
        Assert.That("x".ValidateIsNotElementOf(list).IsValid, Is.True);
    }

    [Test]
    public void ValidateIsNotElementOf_Collection_WithContainedItem_ReturnsInvalid()
    {
        var result = 2.ValidateIsNotElementOf(new List<int> { 1, 2 });
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsNotElementOf.ValidatorName));
    }

    [Test]
    public void ValidateIsNotElementOf_String_WithMissingSubstring_ReturnsValid() =>
        Assert.That("abc".ValidateIsNotElementOf("x").IsValid, Is.True);

    [Test]
    public void EnsureIsNotElementOf_Collection_WithMissingItem_DoesNotThrow() =>
        Assert.DoesNotThrow(() => 5.EnsureIsNotElementOf(new List<int> { 1, 2, 3 }));

    [Test]
    public void EnsureIsNotElementOf_Collection_WithContainedItem_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => 2.EnsureIsNotElementOf(new List<int> { 1, 2 }));
        Assert.That(ex!.Validator, Is.EqualTo(IsNotElementOf.ValidatorName));
    }

    [Test]
    public void EnsureIsNotElementOf_String_WithMissingSubstring_DoesNotThrow() =>
        Assert.DoesNotThrow(() => "hello".EnsureIsNotElementOf("xyz"));
}
