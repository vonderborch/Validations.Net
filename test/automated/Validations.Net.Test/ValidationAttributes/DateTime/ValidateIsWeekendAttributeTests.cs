namespace Validations.Net.Test.ValidationAttributes.DateTime;

[TestFixture]
public class ValidateIsWeekendAttributeTests
{
    [Test]
    public void Validate_WithSaturday_ReturnsSuccess()
    {
        var attr = new ValidateIsWeekendAttribute();
        var saturday = new System.DateTime(2025, 2, 22, 12, 0, 0, DateTimeKind.Utc);
        var result = attr.Validate(saturday);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithSunday_ReturnsSuccess()
    {
        var attr = new ValidateIsWeekendAttribute();
        var sunday = new System.DateTime(2025, 2, 23, 12, 0, 0, DateTimeKind.Utc);
        var result = attr.Validate(sunday);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithMonday_ReturnsFailure()
    {
        var attr = new ValidateIsWeekendAttribute();
        var monday = new System.DateTime(2025, 2, 24, 12, 0, 0, DateTimeKind.Utc);
        var result = attr.Validate(monday);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsWeekendAttribute { Message = "Must be weekend" };
        var monday = new System.DateTime(2025, 2, 24, 12, 0, 0, DateTimeKind.Utc);
        var result = attr.Validate(monday);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must be weekend"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsWeekendAttribute();
        var monday = new System.DateTime(2025, 2, 24, 12, 0, 0, DateTimeKind.Utc);
        var result = attr.Validate(monday, "Date");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Date"));
    }
}
