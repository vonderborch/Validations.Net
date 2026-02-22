namespace Validations.Net.Test.Validators.DateTime;

[TestFixture]
public class IsLeapYearTests
{
    [Test]
    public void CheckIsLeapYear_WithLeapYear_ReturnsTrue()
    {
        Assert.That(2024.CheckIsLeapYear(), Is.True);
        Assert.That(2020.CheckIsLeapYear(), Is.True);
    }

    [Test]
    public void CheckIsLeapYear_WithNonLeapYear_ReturnsFalse()
    {
        Assert.That(2023.CheckIsLeapYear(), Is.False);
        Assert.That(2025.CheckIsLeapYear(), Is.False);
    }

    [Test]
    public void CheckIsLeapYear_WithDateTime_ChecksYear()
    {
        var leapDate = new System.DateTime(2024, 2, 29, 12, 0, 0, DateTimeKind.Utc);
        Assert.That(leapDate.CheckIsLeapYear(), Is.True);
    }

    [Test]
    public void ValidateIsLeapYear_WithLeapYear_ReturnsValid()
    {
        var result = 2024.ValidateIsLeapYear();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsLeapYear_WithNonLeapYear_ReturnsInvalid()
    {
        var result = 2023.ValidateIsLeapYear();
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureIsLeapYear_WithNonLeapYear_Throws()
    {
        Assert.Throws<ValidationException>(() => 2023.EnsureIsLeapYear());
    }
}
