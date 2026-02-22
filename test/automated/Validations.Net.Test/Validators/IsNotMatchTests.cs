using System.Text.RegularExpressions;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNotMatchTests
{
    [Test]
    public void CheckIsNotMatch_WithNull_ReturnsTrue() => Assert.That(((string?)null).CheckIsNotMatch(@"\d+"), Is.True);

    [Test]
    public void CheckIsNotMatch_WithMatchingString_ReturnsFalse() => Assert.That("123".CheckIsNotMatch(@"\d+"), Is.False);

    [Test]
    public void CheckIsNotMatch_WithNonMatchingString_ReturnsTrue() => Assert.That("abc".CheckIsNotMatch(@"\d+"), Is.True);

    [Test]
    public void CheckIsNotMatch_WithRegex_Works()
    {
        var regex = new Regex(@"^[a-z]+$");
        Assert.That("Hello".CheckIsNotMatch(regex), Is.True);
        Assert.That("hello".CheckIsNotMatch(regex), Is.False);
    }

    [Test]
    public void ValidateIsNotMatch_WithNonMatchingString_ReturnsValid() => Assert.That("xyz".ValidateIsNotMatch(@"\d+").IsValid, Is.True);

    [Test]
    public void ValidateIsNotMatch_WithMatchingString_ReturnsInvalid()
    {
        var result = "123".ValidateIsNotMatch(@"\d+");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsNotMatch.ValidatorName));
    }

    [Test]
    public void EnsureIsNotMatch_WithNonMatchingString_DoesNotThrow() => Assert.DoesNotThrow(() => "abc".EnsureIsNotMatch(@"\d+"));

    [Test]
    public void EnsureIsNotMatch_WithMatchingString_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => "123".EnsureIsNotMatch(@"\d+"));
        Assert.That(ex!.Validator, Is.EqualTo(IsNotMatch.ValidatorName));
    }
}
