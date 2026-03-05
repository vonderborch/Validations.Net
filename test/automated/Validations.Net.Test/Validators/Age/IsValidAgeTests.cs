using Validations.Net.OLD;
using Validations.Net.OLD.Validators.Age;

namespace Validations.Net.Test.Validators.Age;

[TestFixture]
public class IsValidAgeTests
{
    [Test]
    public void CheckIsValidAge_WithValidAge_ReturnsTrue()
    {
        Assert.That(0.CheckIsValidAge(), Is.True);
        Assert.That(25.CheckIsValidAge(), Is.True);
        Assert.That(150.CheckIsValidAge(), Is.True);
    }

    [Test]
    public void CheckIsValidAge_WithNegativeAge_ReturnsFalse()
    {
        Assert.That((-1).CheckIsValidAge(), Is.False);
    }

    [Test]
    public void CheckIsValidAge_WithAgeOver150_ReturnsFalse()
    {
        Assert.That(151.CheckIsValidAge(), Is.False);
    }

    [Test]
    public void ValidateIsValidAge_WithValidAge_ReturnsValid()
    {
        var result = 25.ValidateIsValidAge();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsValidAge_WithInvalidAge_ReturnsInvalid()
    {
        var result = (-1).ValidateIsValidAge();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsValidAge.ValidatorName));
    }

    [Test]
    public void EnsureIsValidAge_WithValidAge_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => 25.EnsureIsValidAge());
    }

    [Test]
    public void EnsureIsValidAge_WithInvalidAge_Throws()
    {
        Assert.Throws<ValidationException>(() => (-1).EnsureIsValidAge());
    }
}
