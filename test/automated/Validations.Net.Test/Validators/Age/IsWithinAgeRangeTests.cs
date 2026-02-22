namespace Validations.Net.Test.Validators.Age;

[TestFixture]
public class IsWithinAgeRangeTests
{
    [Test]
    public void CheckIsWithinAgeRange_Int_WithAgeInRange_ReturnsTrue()
    {
        Assert.That(25.CheckIsWithinAgeRange(18, 65), Is.True);
        Assert.That(18.CheckIsWithinAgeRange(18, 65), Is.True);
        Assert.That(65.CheckIsWithinAgeRange(18, 65), Is.True);
    }

    [Test]
    public void CheckIsWithinAgeRange_Int_WithAgeBelowMin_ReturnsFalse()
    {
        Assert.That(17.CheckIsWithinAgeRange(18, 65), Is.False);
    }

    [Test]
    public void CheckIsWithinAgeRange_Int_WithAgeAboveMax_ReturnsFalse()
    {
        Assert.That(66.CheckIsWithinAgeRange(18, 65), Is.False);
    }

    [Test]
    public void CheckIsWithinAgeRange_DateTime_WithAgeInRange_ReturnsTrue()
    {
        var dob = System.DateTime.UtcNow.AddYears(-25);
        Assert.That(dob.CheckIsWithinAgeRange(18, 65), Is.True);
    }

    [Test]
    public void CheckIsWithinAgeRange_DateTime_WithAgeBelowMin_ReturnsFalse()
    {
        var minorDob = System.DateTime.UtcNow.AddYears(-10);
        Assert.That(minorDob.CheckIsWithinAgeRange(18, 65), Is.False);
    }

    [Test]
    public void CheckIsWithinAgeRange_DateTime_WithAgeAboveMax_ReturnsFalse()
    {
        var seniorDob = System.DateTime.UtcNow.AddYears(-70);
        Assert.That(seniorDob.CheckIsWithinAgeRange(18, 65), Is.False);
    }

    [Test]
    public void CheckIsWithinAgeRange_DateTimeOffset_WithAgeInRange_ReturnsTrue()
    {
        var dob = new DateTimeOffset(System.DateTime.UtcNow.AddYears(-30));
        Assert.That(dob.CheckIsWithinAgeRange(18, 65), Is.True);
    }

    [Test]
    public void ValidateIsWithinAgeRange_Int_WithAgeInRange_ReturnsValid()
    {
        var result = 25.ValidateIsWithinAgeRange(18, 65);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsWithinAgeRange_Int_WithAgeOutOfRange_ReturnsInvalid()
    {
        var result = 17.ValidateIsWithinAgeRange(18, 65);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsWithinAgeRange.ValidatorName));
    }

    [Test]
    public void EnsureIsWithinAgeRange_Int_WithAgeInRange_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => 25.EnsureIsWithinAgeRange(18, 65));
    }

    [Test]
    public void EnsureIsWithinAgeRange_Int_WithAgeOutOfRange_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => 17.EnsureIsWithinAgeRange(18, 65));
        Assert.That(ex!.Validator, Is.EqualTo(IsWithinAgeRange.ValidatorName));
    }

    [Test]
    public void EnsureIsWithinAgeRange_DateTime_WithAgeInRange_DoesNotThrow()
    {
        var dob = System.DateTime.UtcNow.AddYears(-25);
        Assert.DoesNotThrow(() => dob.EnsureIsWithinAgeRange(18, 65));
    }

    [Test]
    public void EnsureIsWithinAgeRange_DateTime_WithAgeOutOfRange_Throws()
    {
        var minorDob = System.DateTime.UtcNow.AddYears(-10);
        var ex = Assert.Throws<ValidationException>(() => minorDob.EnsureIsWithinAgeRange(18, 65));
        Assert.That(ex!.Validator, Is.EqualTo(IsWithinAgeRange.ValidatorName));
    }
}
