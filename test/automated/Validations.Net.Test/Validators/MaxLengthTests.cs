using Validations.Net.OLD;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class MaxLengthTests
{
    [Test]
    public void CheckMaxLength_WithNull_ReturnsFalse() => Assert.That(((string?)null).CheckMaxLength(5), Is.False);

    [Test]
    public void CheckMaxLength_WithWithinLimit_ReturnsTrue() => Assert.That("hi".CheckMaxLength(5), Is.True);

    [Test]
    public void CheckMaxLength_WithExactLength_ReturnsTrue() => Assert.That("hello".CheckMaxLength(5), Is.True);

    [Test]
    public void CheckMaxLength_WithExceedsLimit_ReturnsFalse() => Assert.That("hello!".CheckMaxLength(5), Is.False);

    [Test]
    public void ValidateMaxLength_WithWithinLimit_ReturnsValid() => Assert.That("hi".ValidateMaxLength(5).IsValid, Is.True);

    [Test]
    public void ValidateMaxLength_WithExceedsLimit_ReturnsInvalid()
    {
        var result = "hello!".ValidateMaxLength(5);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(MaxLength.ValidatorName));
    }

    [Test]
    public void EnsureMaxLength_WithWithinLimit_DoesNotThrow() => Assert.DoesNotThrow(() => "hi".EnsureMaxLength(5));

    [Test]
    public void EnsureMaxLength_WithExceedsLimit_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => "hello!".EnsureMaxLength(5));
        Assert.That(ex!.Validator, Is.EqualTo(MaxLength.ValidatorName));
    }
}
