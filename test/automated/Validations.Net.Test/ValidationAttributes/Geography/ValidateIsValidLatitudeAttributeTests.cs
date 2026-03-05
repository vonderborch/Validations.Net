using Validations.Net.OLD.Validators.Geography;

namespace Validations.Net.Test.ValidationAttributes.Geography;

[TestFixture]
public class ValidateIsValidLatitudeAttributeTests
{
    [Test]
    public void Validate_WithValidLatitude_ReturnsSuccess()
    {
        var attr = new ValidateIsValidLatitudeAttribute();
        var result = attr.Validate(45.0);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithLatitudeAboveMax_ReturnsFailure()
    {
        var attr = new ValidateIsValidLatitudeAttribute();
        var result = attr.Validate(91.0);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithLatitudeBelowMin_ReturnsFailure()
    {
        var attr = new ValidateIsValidLatitudeAttribute();
        var result = attr.Validate(-91.0);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsValidLatitudeAttribute { Message = "Must be -90 to 90" };
        var result = attr.Validate(91.0);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must be -90 to 90"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsValidLatitudeAttribute();
        var result = attr.Validate(91.0, "Lat");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Lat"));
    }
}
