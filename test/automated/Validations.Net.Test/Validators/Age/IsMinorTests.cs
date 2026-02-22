namespace Validations.Net.Test.Validators.Age;

[TestFixture]
public class IsMinorTests
{
    [Test]
    public void CheckIsMinor_Int_WithValidAge_ReturnsTrue()
    {
        Assert.That(0.CheckIsMinor(), Is.True);
        Assert.That(10.CheckIsMinor(), Is.True);
        Assert.That(17.CheckIsMinor(), Is.True);
    }

    [Test]
    public void CheckIsMinor_Int_WithInvalidAge_ReturnsFalse()
    {
        Assert.That(18.CheckIsMinor(), Is.False);
        Assert.That(25.CheckIsMinor(), Is.False);
    }

    [Test]
    public void CheckIsMinor_Int_WithCustomAdultAge_RespectsThreshold()
    {
        Assert.That(20.CheckIsMinor(21), Is.True);
        Assert.That(21.CheckIsMinor(21), Is.False);
    }

    [Test]
    public void CheckIsMinor_DateTime_WithMinorDob_ReturnsTrue()
    {
        var minorDob = System.DateTime.UtcNow.AddYears(-10);
        Assert.That(minorDob.CheckIsMinor(), Is.True);
    }

    [Test]
    public void CheckIsMinor_DateTime_WithAdultDob_ReturnsFalse()
    {
        var adultDob = System.DateTime.UtcNow.AddYears(-20);
        Assert.That(adultDob.CheckIsMinor(), Is.False);
    }

    [Test]
    public void CheckIsMinor_DateTimeOffset_WithMinorDob_ReturnsTrue()
    {
        var minorDob = new DateTimeOffset(System.DateTime.UtcNow.AddYears(-5));
        Assert.That(minorDob.CheckIsMinor(), Is.True);
    }

    [Test]
    public void CheckIsMinor_DateTimeOffset_WithAdultDob_ReturnsFalse()
    {
        var adultDob = new DateTimeOffset(System.DateTime.UtcNow.AddYears(-25));
        Assert.That(adultDob.CheckIsMinor(), Is.False);
    }

    [Test]
    public void ValidateIsMinor_Int_WithValidAge_ReturnsValid()
    {
        var result = 10.ValidateIsMinor();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsMinor_Int_WithInvalidAge_ReturnsInvalid()
    {
        var result = 18.ValidateIsMinor();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsMinor.ValidatorName));
    }

    [Test]
    public void EnsureIsMinor_Int_WithValidAge_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => 10.EnsureIsMinor());
    }

    [Test]
    public void EnsureIsMinor_Int_WithInvalidAge_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => 18.EnsureIsMinor());
        Assert.That(ex!.Validator, Is.EqualTo(IsMinor.ValidatorName));
    }

    [Test]
    public void EnsureIsMinor_DateTime_WithMinorDob_DoesNotThrow()
    {
        var minorDob = System.DateTime.UtcNow.AddYears(-10);
        Assert.DoesNotThrow(() => minorDob.EnsureIsMinor());
    }

    [Test]
    public void EnsureIsMinor_DateTime_WithAdultDob_Throws()
    {
        var adultDob = System.DateTime.UtcNow.AddYears(-20);
        var ex = Assert.Throws<ValidationException>(() => adultDob.EnsureIsMinor());
        Assert.That(ex!.Validator, Is.EqualTo(IsMinor.ValidatorName));
    }
}
