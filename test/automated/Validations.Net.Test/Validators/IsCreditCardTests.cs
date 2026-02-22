using Validations.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsCreditCardTests
{
    // Valid test card numbers (Luhn algorithm)
    private const string ValidCard1 = "4532015112830366"; // Visa
    private const string ValidCard2 = "4242424242424242";
    private const string ValidCardWithSpaces = "4532 0151 1283 0366";
    private const string ValidCardWithDashes = "4532-0151-1283-0366";

    #region CheckIsCreditCard Tests

    [Test]
    public void CheckIsCreditCard_WithValidCard_ReturnsTrue()
    {
        Assert.That(ValidCard1.CheckIsCreditCard(), Is.True);
    }

    [Test]
    public void CheckIsCreditCard_WithValidCardWithSpaces_ReturnsTrue()
    {
        Assert.That(ValidCardWithSpaces.CheckIsCreditCard(), Is.True);
    }

    [Test]
    public void CheckIsCreditCard_WithValidCardWithDashes_ReturnsTrue()
    {
        Assert.That(ValidCardWithDashes.CheckIsCreditCard(), Is.True);
    }

    [Test]
    public void CheckIsCreditCard_WithInvalidCard_ReturnsFalse()
    {
        Assert.That("4532015112830367".CheckIsCreditCard(), Is.False);
    }

    [Test]
    public void CheckIsCreditCard_WithNull_ReturnsFalse()
    {
        Assert.That(((string?)null).CheckIsCreditCard(), Is.False);
    }

    [Test]
    public void CheckIsCreditCard_WithEmptyString_ReturnsFalse()
    {
        Assert.That("".CheckIsCreditCard(), Is.False);
    }

    [Test]
    public void CheckIsCreditCard_WithTooShort_ReturnsFalse()
    {
        Assert.That("123456789012".CheckIsCreditCard(), Is.False); // 12 digits
    }

    [Test]
    public void CheckIsCreditCard_WithTooLong_ReturnsFalse()
    {
        Assert.That("12345678901234567890".CheckIsCreditCard(), Is.False); // 20 digits
    }

    [Test]
    public void CheckIsCreditCard_WithInvalidLuhn_ReturnsFalse()
    {
        Assert.That("1234567890123456".CheckIsCreditCard(), Is.False);
    }

    #endregion

    #region ValidateIsCreditCard Tests

    [Test]
    public void ValidateIsCreditCard_WithValidCard_ReturnsValidResult()
    {
        var result = ValidCard1.ValidateIsCreditCard();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsCreditCard_WithInvalidCard_ReturnsInvalidResult()
    {
        var result = "1234567890123456".ValidateIsCreditCard();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsCreditCard.ValidatorName));
    }

    [Test]
    public void ValidateIsCreditCard_WithNull_ReturnsInvalidResult()
    {
        var result = ((string?)null).ValidateIsCreditCard();
        Assert.That(result.IsValid, Is.False);
    }

    #endregion

    #region EnsureIsCreditCard Tests

    [Test]
    public void EnsureIsCreditCard_WithValidCard_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => ValidCard1.EnsureIsCreditCard());
    }

    [Test]
    public void EnsureIsCreditCard_WithInvalidCard_ThrowsValidationException()
    {
        Assert.Throws<ValidationException>(() => "1234567890123456".EnsureIsCreditCard());
    }

    [Test]
    public void EnsureIsCreditCard_ReturnsValue()
    {
        var result = ValidCard1.EnsureIsCreditCard();
        Assert.That(result, Is.EqualTo(ValidCard1));
    }

    [Test]
    public void EnsureIsCreditCard_WithSpaces_Works()
    {
        Assert.DoesNotThrow(() => ValidCardWithSpaces.EnsureIsCreditCard());
    }

    #endregion
}
