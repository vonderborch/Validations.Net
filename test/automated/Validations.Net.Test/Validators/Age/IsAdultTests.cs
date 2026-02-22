namespace Validations.Net.Test.Validators.Age;

[TestFixture]
public class IsAdultTests
{
    [Test]
    public void CheckIsAdult_Int_WithValidAge_ReturnsTrue()
    {
        Assert.That(18.CheckIsAdult(), Is.True);
        Assert.That(25.CheckIsAdult(), Is.True);
        Assert.That(100.CheckIsAdult(), Is.True);
    }

    [Test]
    public void CheckIsAdult_Int_WithInvalidAge_ReturnsFalse()
    {
        Assert.That(17.CheckIsAdult(), Is.False);
        Assert.That(0.CheckIsAdult(), Is.False);
    }

    [Test]
    public void CheckIsAdult_Int_WithCustomAdultAge_RespectsThreshold()
    {
        Assert.That(21.CheckIsAdult(21), Is.True);
        Assert.That(20.CheckIsAdult(21), Is.False);
    }

    [Test]
    public void CheckIsAdult_DateTime_WithAdultDob_ReturnsTrue()
    {
        var adultDob = System.DateTime.UtcNow.AddYears(-20);
        Assert.That(adultDob.CheckIsAdult(), Is.True);
    }

    [Test]
    public void CheckIsAdult_DateTime_WithMinorDob_ReturnsFalse()
    {
        var minorDob = System.DateTime.UtcNow.AddYears(-10);
        Assert.That(minorDob.CheckIsAdult(), Is.False);
    }

    [Test]
    public void CheckIsAdult_DateTimeOffset_WithAdultDob_ReturnsTrue()
    {
        var adultDob = new DateTimeOffset(System.DateTime.UtcNow.AddYears(-25));
        Assert.That(adultDob.CheckIsAdult(), Is.True);
    }

    [Test]
    public void CheckIsAdult_DateTimeOffset_WithMinorDob_ReturnsFalse()
    {
        var minorDob = new DateTimeOffset(System.DateTime.UtcNow.AddYears(-5));
        Assert.That(minorDob.CheckIsAdult(), Is.False);
    }

    [Test]
    public void ValidateIsAdult_Int_WithValidAge_ReturnsValid()
    {
        var result = 25.ValidateIsAdult();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsAdult_Int_WithInvalidAge_ReturnsInvalid()
    {
        var result = 17.ValidateIsAdult();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsAdult.ValidatorName));
    }

    [Test]
    public void EnsureIsAdult_Int_WithValidAge_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => 25.EnsureIsAdult());
    }

    [Test]
    public void EnsureIsAdult_Int_WithInvalidAge_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => 17.EnsureIsAdult());
        Assert.That(ex!.Validator, Is.EqualTo(IsAdult.ValidatorName));
    }

    [Test]
    public void EnsureIsAdult_DateTime_WithAdultDob_DoesNotThrow()
    {
        var adultDob = System.DateTime.UtcNow.AddYears(-20);
        Assert.DoesNotThrow(() => adultDob.EnsureIsAdult());
    }

    [Test]
    public void EnsureIsAdult_DateTime_WithMinorDob_Throws()
    {
        var minorDob = System.DateTime.UtcNow.AddYears(-10);
        var ex = Assert.Throws<ValidationException>(() => minorDob.EnsureIsAdult());
        Assert.That(ex!.Validator, Is.EqualTo(IsAdult.ValidatorName));
    }
}
