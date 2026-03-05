using Validations.Net.OLD;
using Validations.Net.OLD.Validators.Geography;

namespace Validations.Net.Test.Validators.Geography;

[TestFixture]
public class IsValidLatitudeTests
{
    [Test]
    public void CheckIsValidLatitude_WithValidLatitude_ReturnsTrue()
    {
        Assert.That(0.0.CheckIsValidLatitude(), Is.True);
        Assert.That(45.5.CheckIsValidLatitude(), Is.True);
        Assert.That(90.0.CheckIsValidLatitude(), Is.True);
        Assert.That((-90.0).CheckIsValidLatitude(), Is.True);
    }

    [Test]
    public void CheckIsValidLatitude_WithAboveMax_ReturnsFalse()
    {
        Assert.That(90.1.CheckIsValidLatitude(), Is.False);
        Assert.That(100.0.CheckIsValidLatitude(), Is.False);
    }

    [Test]
    public void CheckIsValidLatitude_WithBelowMin_ReturnsFalse()
    {
        Assert.That((-90.1).CheckIsValidLatitude(), Is.False);
        Assert.That((-100.0).CheckIsValidLatitude(), Is.False);
    }

    [Test]
    public void ValidateIsValidLatitude_WithValidLatitude_ReturnsValid()
    {
        var result = 45.0.ValidateIsValidLatitude();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsValidLatitude_WithInvalidLatitude_ReturnsInvalid()
    {
        var result = 95.0.ValidateIsValidLatitude();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsValidLatitude.ValidatorName));
    }

    [Test]
    public void EnsureIsValidLatitude_WithValidLatitude_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => 45.0.EnsureIsValidLatitude());
    }

    [Test]
    public void EnsureIsValidLatitude_WithInvalidLatitude_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => 95.0.EnsureIsValidLatitude());
        Assert.That(ex!.Validator, Is.EqualTo(IsValidLatitude.ValidatorName));
    }
}
