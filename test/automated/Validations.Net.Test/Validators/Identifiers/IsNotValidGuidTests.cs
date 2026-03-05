using Validations.Net.OLD;
using Validations.Net.OLD.Validators.Identifiers;

namespace Validations.Net.Test.Validators.Identifiers;

[TestFixture]
public class IsNotValidGuidTests
{
    [Test]
    public void CheckIsNotValidGuid_WithInvalidString_ReturnsTrue()
    {
        Assert.That("not-a-guid".CheckIsNotValidGuid(), Is.True);
    }

    [Test]
    public void CheckIsNotValidGuid_WithValidGuid_ReturnsFalse()
    {
        Assert.That("550e8400-e29b-41d4-a716-446655440000".CheckIsNotValidGuid(), Is.False);
    }

    [Test]
    public void CheckIsNotValidGuid_WithNull_ReturnsTrue()
    {
        Assert.That(((string?)null).CheckIsNotValidGuid(), Is.True);
    }

    [Test]
    public void ValidateIsNotValidGuid_WithInvalidString_ReturnsValid()
    {
        var result = "not-a-guid".ValidateIsNotValidGuid();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsNotValidGuid_WithValidGuid_ReturnsInvalid()
    {
        var result = "550e8400-e29b-41d4-a716-446655440000".ValidateIsNotValidGuid();
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureIsNotValidGuid_WithInvalidString_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => "not-a-guid".EnsureIsNotValidGuid());
    }

    [Test]
    public void EnsureIsNotValidGuid_WithValidGuid_Throws()
    {
        Assert.Throws<ValidationException>(() => "550e8400-e29b-41d4-a716-446655440000".EnsureIsNotValidGuid());
    }
}
