using Validations.Net.OLD;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class DoesContainTests
{
    [Test]
    public void CheckDoesContain_Collection_WithNull_ReturnsFalse() =>
        Assert.That(((List<int>?)null).CheckDoesContain(1), Is.False);

    [Test]
    public void CheckDoesContain_Collection_WithContainedItem_ReturnsTrue()
    {
        var list = new List<int> { 1, 2, 3 };
        Assert.That(list.CheckDoesContain(2), Is.True);
    }

    [Test]
    public void CheckDoesContain_Collection_WithMissingItem_ReturnsFalse()
    {
        var list = new List<int> { 1, 2, 3 };
        Assert.That(list.CheckDoesContain(4), Is.False);
    }

    [Test]
    public void CheckDoesContain_String_WithNull_ReturnsFalse() =>
        Assert.That(((string?)null).CheckDoesContain("sub"), Is.False);

    [Test]
    public void CheckDoesContain_String_WithContainedSubstring_ReturnsTrue() =>
        Assert.That("hello world".CheckDoesContain("world"), Is.True);

    [Test]
    public void CheckDoesContain_String_WithMissingSubstring_ReturnsFalse() =>
        Assert.That("hello".CheckDoesContain("xyz"), Is.False);

    [Test]
    public void CheckDoesContain_String_WithComparison_RespectsCase()
    {
        Assert.That("Hello".CheckDoesContain("ell", StringComparison.Ordinal), Is.True);
        Assert.That("Hello".CheckDoesContain("ELL", StringComparison.Ordinal), Is.False);
        Assert.That("Hello".CheckDoesContain("ELL", StringComparison.OrdinalIgnoreCase), Is.True);
    }

    [Test]
    public void ValidateDoesContain_Collection_WithContainedItem_ReturnsValid()
    {
        var arr = new[] { "a", "b", "c" };
        Assert.That(arr.ValidateDoesContain("b").IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesContain_Collection_WithMissingItem_ReturnsInvalid()
    {
        var result = new List<int> { 1, 2 }.ValidateDoesContain(5);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(DoesContain.ValidatorName));
    }

    [Test]
    public void ValidateDoesContain_String_WithContainedSubstring_ReturnsValid() =>
        Assert.That("abc".ValidateDoesContain("b").IsValid, Is.True);

    [Test]
    public void EnsureDoesContain_Collection_WithContainedItem_DoesNotThrow() =>
        Assert.DoesNotThrow(() => new List<int> { 1, 2, 3 }.EnsureDoesContain(2));

    [Test]
    public void EnsureDoesContain_Collection_WithMissingItem_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => new List<int> { 1, 2 }.EnsureDoesContain(5));
        Assert.That(ex!.Validator, Is.EqualTo(DoesContain.ValidatorName));
    }

    [Test]
    public void EnsureDoesContain_String_WithContainedSubstring_DoesNotThrow() =>
        Assert.DoesNotThrow(() => "hello".EnsureDoesContain("ell"));
}
