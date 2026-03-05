using Validations.Net.OLD;
using Validations.Net.OLD.Validators.Geography;

namespace Validations.Net.Test.Validators.Geography;

[TestFixture]
public class IsValidLongitudeTests
{
    [Test]
    public void CheckIsValidLongitude_WithValidLongitude_ReturnsTrue()
    {
        Assert.That(0.0.CheckIsValidLongitude(), Is.True);
        Assert.That(120.5.CheckIsValidLongitude(), Is.True);
        Assert.That(180.0.CheckIsValidLongitude(), Is.True);
        Assert.That((-180.0).CheckIsValidLongitude(), Is.True);
    }

    [Test]
    public void CheckIsValidLongitude_WithAboveMax_ReturnsFalse()
    {
        Assert.That(180.1.CheckIsValidLongitude(), Is.False);
        Assert.That(200.0.CheckIsValidLongitude(), Is.False);
    }

    [Test]
    public void CheckIsValidLongitude_WithBelowMin_ReturnsFalse()
    {
        Assert.That((-180.1).CheckIsValidLongitude(), Is.False);
        Assert.That((-200.0).CheckIsValidLongitude(), Is.False);
    }

    [Test]
    public void ValidateIsValidLongitude_WithValidLongitude_ReturnsValid()
    {
        var result = 120.0.ValidateIsValidLongitude();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsValidLongitude_WithInvalidLongitude_ReturnsInvalid()
    {
        var result = 200.0.ValidateIsValidLongitude();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsValidLongitude.ValidatorName));
    }

    [Test]
    public void EnsureIsValidLongitude_WithValidLongitude_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => 120.0.EnsureIsValidLongitude());
    }

    [Test]
    public void EnsureIsValidLongitude_WithInvalidLongitude_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => 200.0.EnsureIsValidLongitude());
        Assert.That(ex!.Validator, Is.EqualTo(IsValidLongitude.ValidatorName));
    }
}
