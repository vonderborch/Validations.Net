namespace Validations.Net.Test.ValidationAttributes.Finance;

[TestFixture]
public class ValidateIsValidIbanAttributeTests
{
    [Test]
    public void Validate_WithValidIban_ReturnsSuccess()
    {
        var attr = new ValidateIsValidIbanAttribute();
        var result = attr.Validate("GB82WEST12345698765432");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithInvalidIban_ReturnsFailure()
    {
        var attr = new ValidateIsValidIbanAttribute();
        var result = attr.Validate("invalid");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithIbanWithSpaces_ReturnsSuccess()
    {
        var attr = new ValidateIsValidIbanAttribute();
        var result = attr.Validate("GB82 WEST 1234 5698 7654 32");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsValidIbanAttribute { Message = "Must be valid IBAN" };
        var result = attr.Validate("invalid");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must be valid IBAN"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsValidIbanAttribute();
        var result = attr.Validate("invalid", "Iban");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Iban"));
    }
}
