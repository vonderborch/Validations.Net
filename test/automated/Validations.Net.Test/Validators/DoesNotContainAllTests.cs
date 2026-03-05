using Validations.Net.OLD;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class DoesNotContainAllTests
{
    [Test]
    public void CheckDoesNotContainAll_Collection_WithNull_ReturnsTrue() =>
        Assert.That(((List<int>?)null).CheckDoesNotContainAll(new[] { 1, 2 }), Is.True);

    [Test]
    public void CheckDoesNotContainAll_Collection_WithAllPresent_ReturnsFalse()
    {
        var list = new List<int> { 1, 2, 3, 4 };
        Assert.That(list.CheckDoesNotContainAll(new[] { 2, 4 }), Is.False);
    }

    [Test]
    public void CheckDoesNotContainAll_Collection_WithOneMissing_ReturnsTrue()
    {
        var list = new List<int> { 1, 2, 3 };
        Assert.That(list.CheckDoesNotContainAll(new[] { 2, 5 }), Is.True);
    }

    [Test]
    public void CheckDoesNotContainAll_String_WithNull_ReturnsTrue() =>
        Assert.That(((string?)null).CheckDoesNotContainAll(new[] { "a", "b" }), Is.True);

    [Test]
    public void CheckDoesNotContainAll_String_WithAllPresent_ReturnsFalse() =>
        Assert.That("hello world".CheckDoesNotContainAll(new[] { "ell", "wor" }), Is.False);

    [Test]
    public void CheckDoesNotContainAll_String_WithOneMissing_ReturnsTrue() =>
        Assert.That("hello".CheckDoesNotContainAll(new[] { "ell", "xyz" }), Is.True);

    [Test]
    public void ValidateDoesNotContainAll_Collection_WithOneMissing_ReturnsValid()
    {
        var arr = new[] { "a", "b", "c" };
        Assert.That(arr.ValidateDoesNotContainAll(new[] { "a", "x" }).IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAll_Collection_WithAllPresent_ReturnsInvalid()
    {
        var result = new List<int> { 1, 2 }.ValidateDoesNotContainAll(new[] { 1, 2 });
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(DoesNotContainAll.ValidatorName));
    }

    [Test]
    public void ValidateDoesNotContainAll_String_WithOneMissing_ReturnsValid() =>
        Assert.That("abc".ValidateDoesNotContainAll(new[] { "a", "x" }).IsValid, Is.True);

    [Test]
    public void EnsureDoesNotContainAll_Collection_WithOneMissing_DoesNotThrow() =>
        Assert.DoesNotThrow(() => new List<int> { 1, 2, 3 }.EnsureDoesNotContainAll(new[] { 1, 5 }));

    [Test]
    public void EnsureDoesNotContainAll_Collection_WithAllPresent_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => new List<int> { 1, 2 }.EnsureDoesNotContainAll(new[] { 1, 2 }));
        Assert.That(ex!.Validator, Is.EqualTo(DoesNotContainAll.ValidatorName));
    }

    [Test]
    public void EnsureDoesNotContainAll_String_WithOneMissing_DoesNotThrow() =>
        Assert.DoesNotThrow(() => "hello".EnsureDoesNotContainAll(new[] { "ell", "xyz" }));
}
