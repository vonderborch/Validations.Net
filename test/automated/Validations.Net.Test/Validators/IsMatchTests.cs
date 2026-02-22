using System.Text.RegularExpressions;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsMatchTests
{
    [Test]
    public void CheckIsMatch_WithNull_ReturnsFalse() => Assert.That(((string?)null).CheckIsMatch(@"\d+"), Is.False);

    [Test]
    public void CheckIsMatch_WithMatchingString_ReturnsTrue() => Assert.That("123".CheckIsMatch(@"\d+"), Is.True);

    [Test]
    public void CheckIsMatch_WithNonMatchingString_ReturnsFalse() => Assert.That("abc".CheckIsMatch(@"\d+"), Is.False);

    [Test]
    public void CheckIsMatch_WithRegex_Works()
    {
        var regex = new Regex(@"^[a-z]+$");
        Assert.That("hello".CheckIsMatch(regex), Is.True);
        Assert.That("Hello".CheckIsMatch(regex), Is.False);
    }

    [Test]
    public void ValidateIsMatch_WithMatchingString_ReturnsValid() => Assert.That("123".ValidateIsMatch(@"\d+").IsValid, Is.True);

    [Test]
    public void ValidateIsMatch_WithNonMatchingString_ReturnsInvalid()
    {
        var result = "xyz".ValidateIsMatch(@"\d+");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsMatch.ValidatorName));
    }

    [Test]
    public void EnsureIsMatch_WithMatchingString_DoesNotThrow() => Assert.DoesNotThrow(() => "123".EnsureIsMatch(@"\d+"));

    [Test]
    public void EnsureIsMatch_WithNonMatchingString_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => "abc".EnsureIsMatch(@"\d+"));
        Assert.That(ex!.Validator, Is.EqualTo(IsMatch.ValidatorName));
    }
}
