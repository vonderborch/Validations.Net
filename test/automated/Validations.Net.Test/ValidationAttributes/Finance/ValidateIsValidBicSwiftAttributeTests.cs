using Validations.Net.OLD.Validators.Finance;

namespace Validations.Net.Test.ValidationAttributes.Finance;

[TestFixture]
public class ValidateIsValidBicSwiftAttributeTests
{
    [Test]
    public void Validate_WithValidBic_ReturnsSuccess()
    {
        var attr = new ValidateIsValidBicSwiftAttribute();
        var result = attr.Validate("DEUTDEFF");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithInvalidBic_ReturnsFailure()
    {
        var attr = new ValidateIsValidBicSwiftAttribute();
        var result = attr.Validate("invalid");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_With11CharBic_ReturnsSuccess()
    {
        var attr = new ValidateIsValidBicSwiftAttribute();
        var result = attr.Validate("DEUTDEFF500");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsValidBicSwiftAttribute { Message = "Must be valid BIC" };
        var result = attr.Validate("invalid");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must be valid BIC"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsValidBicSwiftAttribute();
        var result = attr.Validate("invalid", "Bic");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Bic"));
    }
}
