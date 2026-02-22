using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsValidUriTests
{
    [Test]
    public void CheckIsValidUri_WithValidAbsolute_ReturnsTrue() => Assert.That("https://example.com".CheckIsValidUri(), Is.True);

    [Test]
    public void CheckIsValidUri_WithInvalid_ReturnsFalse() => Assert.That("not a uri".CheckIsValidUri(), Is.False);

    [Test]
    public void CheckIsValidUri_WithRelativeOrAbsolute_Works()
    {
        Assert.That("relative-segment".CheckIsValidUri(UriKind.Relative), Is.True);
        Assert.That("https://example.com".CheckIsValidUri(UriKind.RelativeOrAbsolute), Is.True);
    }

    [Test]
    public void ValidateIsValidUri_WithValidUri_ReturnsValid() => Assert.That("https://test.com".ValidateIsValidUri().IsValid, Is.True);

    [Test]
    public void ValidateIsValidUri_WithInvalidUri_ReturnsInvalid()
    {
        var result = ":::".ValidateIsValidUri();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsValidUri.ValidatorName));
    }

    [Test]
    public void EnsureIsValidUri_WithValidUri_DoesNotThrow() => Assert.DoesNotThrow(() => "https://x.com".EnsureIsValidUri());

    [Test]
    public void EnsureIsValidUri_WithInvalidUri_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => "not valid".EnsureIsValidUri());
        Assert.That(ex!.Validator, Is.EqualTo(IsValidUri.ValidatorName));
    }
}
