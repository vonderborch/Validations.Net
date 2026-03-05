using Validations.Net.OLD;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class DoesNotContainTests
{
    [Test]
    public void CheckDoesNotContain_Collection_WithNull_ReturnsTrue() =>
        Assert.That(((List<int>?)null).CheckDoesNotContain(1), Is.True);

    [Test]
    public void CheckDoesNotContain_Collection_WithContainedItem_ReturnsFalse()
    {
        var list = new List<int> { 1, 2, 3 };
        Assert.That(list.CheckDoesNotContain(2), Is.False);
    }

    [Test]
    public void CheckDoesNotContain_Collection_WithMissingItem_ReturnsTrue()
    {
        var list = new List<int> { 1, 2, 3 };
        Assert.That(list.CheckDoesNotContain(4), Is.True);
    }

    [Test]
    public void CheckDoesNotContain_String_WithNull_ReturnsTrue() =>
        Assert.That(((string?)null).CheckDoesNotContain("sub"), Is.True);

    [Test]
    public void CheckDoesNotContain_String_WithContainedSubstring_ReturnsFalse() =>
        Assert.That("hello world".CheckDoesNotContain("world"), Is.False);

    [Test]
    public void CheckDoesNotContain_String_WithMissingSubstring_ReturnsTrue() =>
        Assert.That("hello".CheckDoesNotContain("xyz"), Is.True);

    [Test]
    public void ValidateDoesNotContain_Collection_WithMissingItem_ReturnsValid()
    {
        var arr = new[] { "a", "b", "c" };
        Assert.That(arr.ValidateDoesNotContain("x").IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesNotContain_Collection_WithContainedItem_ReturnsInvalid()
    {
        var result = new List<int> { 1, 2 }.ValidateDoesNotContain(2);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(DoesNotContain.ValidatorName));
    }

    [Test]
    public void ValidateDoesNotContain_String_WithMissingSubstring_ReturnsValid() =>
        Assert.That("abc".ValidateDoesNotContain("x").IsValid, Is.True);

    [Test]
    public void EnsureDoesNotContain_Collection_WithMissingItem_DoesNotThrow() =>
        Assert.DoesNotThrow(() => new List<int> { 1, 2, 3 }.EnsureDoesNotContain(5));

    [Test]
    public void EnsureDoesNotContain_Collection_WithContainedItem_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => new List<int> { 1, 2 }.EnsureDoesNotContain(2));
        Assert.That(ex!.Validator, Is.EqualTo(DoesNotContain.ValidatorName));
    }

    [Test]
    public void EnsureDoesNotContain_String_WithMissingSubstring_DoesNotThrow() =>
        Assert.DoesNotThrow(() => "hello".EnsureDoesNotContain("xyz"));
}
