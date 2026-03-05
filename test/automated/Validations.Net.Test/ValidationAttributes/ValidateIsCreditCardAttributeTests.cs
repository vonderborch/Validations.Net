using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsCreditCardAttributeTests
{
    [Test]
    public void Validate_WithLuhnValidCard_ReturnsSuccess()
    {
        var attr = new ValidateIsCreditCardAttribute();
        var result = attr.Validate("4242424242424242");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithInvalidCard_ReturnsFailure()
    {
        var attr = new ValidateIsCreditCardAttribute();
        var result = attr.Validate("1234567890");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNonString_ReturnsSuccess()
    {
        var attr = new ValidateIsCreditCardAttribute();
        var result = attr.Validate(12345);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsCreditCardAttribute { Message = "Invalid card number" };
        var result = attr.Validate("1234567890");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Invalid card number"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateIsCreditCardAttribute();
        var result = attr.Validate("1234567890", "CardNumber");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("CardNumber"));
    }
}
