using Validations.Net.OLD;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNotWhiteSpaceTests
{
    [Test]
    public void CheckIsNotWhiteSpace_WithNull_ReturnsTrue() => Assert.That(((string?)null).CheckIsNotWhiteSpace(), Is.True);

    [Test]
    public void CheckIsNotWhiteSpace_WithEmpty_ReturnsTrue() => Assert.That("".CheckIsNotWhiteSpace(), Is.True);

    [Test]
    public void CheckIsNotWhiteSpace_WithWhitespace_ReturnsFalse() => Assert.That("   ".CheckIsNotWhiteSpace(), Is.False);

    [Test]
    public void CheckIsNotWhiteSpace_WithContent_ReturnsTrue() => Assert.That("x".CheckIsNotWhiteSpace(), Is.True);

    [Test]
    public void ValidateIsNotWhiteSpace_WithNull_ReturnsValid() => Assert.That(((string?)null).ValidateIsNotWhiteSpace().IsValid, Is.True);

    [Test]
    public void ValidateIsNotWhiteSpace_WithContent_ReturnsValid() => Assert.That("hello".ValidateIsNotWhiteSpace().IsValid, Is.True);

    [Test]
    public void ValidateIsNotWhiteSpace_WithWhitespace_ReturnsInvalid()
    {
        var result = "   ".ValidateIsNotWhiteSpace();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsNotWhiteSpace.ValidatorName));
    }

    [Test]
    public void EnsureIsNotWhiteSpace_WithContent_DoesNotThrow() => Assert.DoesNotThrow(() => "abc".EnsureIsNotWhiteSpace());

    [Test]
    public void EnsureIsNotWhiteSpace_WithWhitespace_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => "   ".EnsureIsNotWhiteSpace());
        Assert.That(ex!.Validator, Is.EqualTo(IsNotWhiteSpace.ValidatorName));
    }
}
