using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNotNullOrWhiteSpaceTests
{
    [Test]
    public void CheckIsNotNullOrWhiteSpace_WithNull_ReturnsFalse() => Assert.That(((string?)null).CheckIsNotNullOrWhiteSpace(), Is.False);

    [Test]
    public void CheckIsNotNullOrWhiteSpace_WithEmpty_ReturnsFalse() => Assert.That("".CheckIsNotNullOrWhiteSpace(), Is.False);

    [Test]
    public void CheckIsNotNullOrWhiteSpace_WithWhitespace_ReturnsFalse() => Assert.That("   ".CheckIsNotNullOrWhiteSpace(), Is.False);

    [Test]
    public void CheckIsNotNullOrWhiteSpace_WithContent_ReturnsTrue() => Assert.That("hello".CheckIsNotNullOrWhiteSpace(), Is.True);

    [Test]
    public void ValidateIsNotNullOrWhiteSpace_WithContent_ReturnsValid() => Assert.That("hello".ValidateIsNotNullOrWhiteSpace().IsValid, Is.True);

    [Test]
    public void ValidateIsNotNullOrWhiteSpace_WithNull_ReturnsInvalid()
    {
        var result = ((string?)null).ValidateIsNotNullOrWhiteSpace();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsNotNullOrWhiteSpace.ValidatorName));
    }

    [Test]
    public void EnsureIsNotNullOrWhiteSpace_WithContent_DoesNotThrow() => Assert.DoesNotThrow(() => "abc".EnsureIsNotNullOrWhiteSpace());

    [Test]
    public void EnsureIsNotNullOrWhiteSpace_WithNull_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => ((string?)null).EnsureIsNotNullOrWhiteSpace());
        Assert.That(ex!.Validator, Is.EqualTo(IsNotNullOrWhiteSpace.ValidatorName));
    }
}
