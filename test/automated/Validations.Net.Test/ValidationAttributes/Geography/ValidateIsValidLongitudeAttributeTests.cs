using Validations.Net.OLD.Validators.Geography;

namespace Validations.Net.Test.ValidationAttributes.Geography;

[TestFixture]
public class ValidateIsValidLongitudeAttributeTests
{
    [Test]
    public void Validate_WithValidLongitude_ReturnsSuccess()
    {
        var attr = new ValidateIsValidLongitudeAttribute();
        var result = attr.Validate(90.0);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithLongitudeAboveMax_ReturnsFailure()
    {
        var attr = new ValidateIsValidLongitudeAttribute();
        var result = attr.Validate(181.0);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithLongitudeBelowMin_ReturnsFailure()
    {
        var attr = new ValidateIsValidLongitudeAttribute();
        var result = attr.Validate(-181.0);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsValidLongitudeAttribute { Message = "Must be -180 to 180" };
        var result = attr.Validate(181.0);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must be -180 to 180"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsValidLongitudeAttribute();
        var result = attr.Validate(181.0, "Lon");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Lon"));
    }
}
