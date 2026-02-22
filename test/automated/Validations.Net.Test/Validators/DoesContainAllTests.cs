using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class DoesContainAllTests
{
    [Test]
    public void CheckDoesContainAll_Collection_WithNull_ReturnsFalse() =>
        Assert.That(((List<int>?)null).CheckDoesContainAll(new[] { 1, 2 }), Is.False);

    [Test]
    public void CheckDoesContainAll_Collection_WithAllPresent_ReturnsTrue()
    {
        var list = new List<int> { 1, 2, 3, 4 };
        Assert.That(list.CheckDoesContainAll(new[] { 2, 4 }), Is.True);
    }

    [Test]
    public void CheckDoesContainAll_Collection_WithOneMissing_ReturnsFalse()
    {
        var list = new List<int> { 1, 2, 3 };
        Assert.That(list.CheckDoesContainAll(new[] { 2, 5 }), Is.False);
    }

    [Test]
    public void CheckDoesContainAll_String_WithNull_ReturnsFalse() =>
        Assert.That(((string?)null).CheckDoesContainAll(new[] { "a", "b" }), Is.False);

    [Test]
    public void CheckDoesContainAll_String_WithAllPresent_ReturnsTrue() =>
        Assert.That("hello world".CheckDoesContainAll(new[] { "ell", "wor" }), Is.True);

    [Test]
    public void CheckDoesContainAll_String_WithOneMissing_ReturnsFalse() =>
        Assert.That("hello".CheckDoesContainAll(new[] { "ell", "xyz" }), Is.False);

    [Test]
    public void ValidateDoesContainAll_Collection_WithAllPresent_ReturnsValid()
    {
        var arr = new[] { "a", "b", "c" };
        Assert.That(arr.ValidateDoesContainAll(new[] { "a", "c" }).IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesContainAll_Collection_WithOneMissing_ReturnsInvalid()
    {
        var result = new List<int> { 1, 2 }.ValidateDoesContainAll(new[] { 1, 3 });
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(DoesContainAll.ValidatorName));
    }

    [Test]
    public void ValidateDoesContainAll_String_WithAllPresent_ReturnsValid() =>
        Assert.That("abc".ValidateDoesContainAll(new[] { "a", "b" }).IsValid, Is.True);

    [Test]
    public void EnsureDoesContainAll_Collection_WithAllPresent_DoesNotThrow() =>
        Assert.DoesNotThrow(() => new List<int> { 1, 2, 3 }.EnsureDoesContainAll(new[] { 1, 3 }));

    [Test]
    public void EnsureDoesContainAll_Collection_WithOneMissing_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => new List<int> { 1, 2 }.EnsureDoesContainAll(new[] { 1, 3 }));
        Assert.That(ex!.Validator, Is.EqualTo(DoesContainAll.ValidatorName));
    }

    [Test]
    public void EnsureDoesContainAll_String_WithAllPresent_DoesNotThrow() =>
        Assert.DoesNotThrow(() => "hello world".EnsureDoesContainAll(new[] { "ell", "wor" }));
}
