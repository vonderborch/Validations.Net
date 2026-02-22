using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class MinLengthTests
{
    [Test]
    public void CheckMinLength_WithNull_ReturnsFalse() => Assert.That(((string?)null).CheckMinLength(3), Is.False);

    [Test]
    public void CheckMinLength_WithSufficientLength_ReturnsTrue() => Assert.That("hello".CheckMinLength(3), Is.True);

    [Test]
    public void CheckMinLength_WithExactLength_ReturnsTrue() => Assert.That("abc".CheckMinLength(3), Is.True);

    [Test]
    public void CheckMinLength_WithInsufficientLength_ReturnsFalse() => Assert.That("ab".CheckMinLength(3), Is.False);

    [Test]
    public void ValidateMinLength_WithSufficientLength_ReturnsValid() => Assert.That("hello".ValidateMinLength(3).IsValid, Is.True);

    [Test]
    public void ValidateMinLength_WithInsufficientLength_ReturnsInvalid()
    {
        var result = "ab".ValidateMinLength(3);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(MinLength.ValidatorName));
    }

    [Test]
    public void EnsureMinLength_WithSufficientLength_DoesNotThrow() => Assert.DoesNotThrow(() => "hello".EnsureMinLength(3));

    [Test]
    public void EnsureMinLength_WithInsufficientLength_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => "ab".EnsureMinLength(3));
        Assert.That(ex!.Validator, Is.EqualTo(MinLength.ValidatorName));
    }
}
