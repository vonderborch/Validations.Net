using Validations.Net.OLD.Validators.DateTime;

namespace Validations.Net.Test.ValidationAttributes.DateTime;

[TestFixture]
public class ValidateIsLocalAttributeTests
{
    [Test]
    public void Validate_WithLocalDateTime_ReturnsSuccess()
    {
        var attr = new ValidateIsLocalAttribute();
        var local = new System.DateTime(2025, 2, 21, 12, 0, 0, DateTimeKind.Local);
        var result = attr.Validate(local);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithUtcDateTime_ReturnsFailure()
    {
        var attr = new ValidateIsLocalAttribute();
        var utc = new System.DateTime(2025, 2, 21, 12, 0, 0, DateTimeKind.Utc);
        var result = attr.Validate(utc);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithUnspecifiedDateTime_ReturnsFailure()
    {
        var attr = new ValidateIsLocalAttribute();
        var unspecified = new System.DateTime(2025, 2, 21, 12, 0, 0, DateTimeKind.Unspecified);
        var result = attr.Validate(unspecified);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsLocalAttribute { Message = "Must be local time" };
        var utc = new System.DateTime(2025, 2, 21, 12, 0, 0, DateTimeKind.Utc);
        var result = attr.Validate(utc);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must be local time"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsLocalAttribute();
        var utc = new System.DateTime(2025, 2, 21, 12, 0, 0, DateTimeKind.Utc);
        var result = attr.Validate(utc, "Timestamp");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Timestamp"));
    }
}
