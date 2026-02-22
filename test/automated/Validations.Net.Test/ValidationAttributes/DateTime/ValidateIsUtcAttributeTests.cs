namespace Validations.Net.Test.ValidationAttributes.DateTime;

[TestFixture]
public class ValidateIsUtcAttributeTests
{
    [Test]
    public void Validate_WithUtcDateTime_ReturnsSuccess()
    {
        var attr = new ValidateIsUtcAttribute();
        var utc = new System.DateTime(2025, 2, 21, 12, 0, 0, DateTimeKind.Utc);
        var result = attr.Validate(utc);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithLocalDateTime_ReturnsFailure()
    {
        var attr = new ValidateIsUtcAttribute();
        var local = new System.DateTime(2025, 2, 21, 12, 0, 0, DateTimeKind.Local);
        var result = attr.Validate(local);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithUtcDateTimeOffset_ReturnsSuccess()
    {
        var attr = new ValidateIsUtcAttribute();
        var utc = DateTimeOffset.UtcNow;
        var result = attr.Validate(utc);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsUtcAttribute { Message = "Must be UTC" };
        var local = new System.DateTime(2025, 2, 21, 12, 0, 0, DateTimeKind.Local);
        var result = attr.Validate(local);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must be UTC"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsUtcAttribute();
        var local = new System.DateTime(2025, 2, 21, 12, 0, 0, DateTimeKind.Local);
        var result = attr.Validate(local, "Timestamp");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Timestamp"));
    }
}
