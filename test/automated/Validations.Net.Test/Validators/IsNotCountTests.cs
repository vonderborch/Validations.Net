using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNotCountTests
{
    [Test]
    public void CheckIsNotCount_WithWrongCountList_ReturnsTrue()
    {
        System.Collections.ICollection list = new List<int> { 1, 2 };
        Assert.That(list.CheckIsNotCount(3), Is.True);
    }

    [Test]
    public void CheckIsNotCount_WithExactCountList_ReturnsFalse()
    {
        System.Collections.ICollection list = new List<int> { 1, 2, 3 };
        Assert.That(list.CheckIsNotCount(3), Is.False);
    }

    [Test]
    public void CheckIsNotCount_WithNullList_ReturnsTrue()
    {
        System.Collections.ICollection? nullList = null;
        Assert.That(nullList.CheckIsNotCount(0), Is.True);
    }

    [Test]
    public void ValidateIsNotCount_WithWrongCount_ReturnsValid()
    {
        var list = new List<int> { 1 };
        var result = list.ValidateIsNotCount(2);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsNotCount_WithExactCount_ReturnsInvalid()
    {
        var list = new List<int> { 1, 2 };
        var result = list.ValidateIsNotCount(2);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureIsNotCount_WithWrongCount_DoesNotThrow()
    {
        var list = new List<int> { 1, 2 };
        Assert.DoesNotThrow(() => list.EnsureIsNotCount(1));
    }

    [Test]
    public void EnsureIsNotCount_WithExactCount_Throws()
    {
        var list = new List<int> { 1 };
        Assert.Throws<ValidationException>(() => list.EnsureIsNotCount(1));
    }
}
