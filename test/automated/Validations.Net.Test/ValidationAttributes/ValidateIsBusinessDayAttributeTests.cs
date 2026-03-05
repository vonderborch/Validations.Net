using Validations.Net.OLD.Validators.DateTime;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsBusinessDayAttributeTests
{
    [Test]
    public void Validate_WithWeekdayDateTime_ReturnsValid()
    {
        var attr = new ValidateIsBusinessDayAttribute();
        var monday = new System.DateTime(2025, 2, 17, 12, 0, 0, DateTimeKind.Utc); // Monday
        var result = attr.Validate(monday);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithFridayDateTime_ReturnsValid()
    {
        var attr = new ValidateIsBusinessDayAttribute();
        var friday = new System.DateTime(2025, 2, 21, 12, 0, 0, DateTimeKind.Utc); // Friday
        var result = attr.Validate(friday);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithSaturdayDateTime_ReturnsInvalid()
    {
        var attr = new ValidateIsBusinessDayAttribute();
        var saturday = new System.DateTime(2025, 2, 22, 12, 0, 0, DateTimeKind.Utc); // Saturday
        var result = attr.Validate(saturday);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsBusinessDay.ValidatorName));
    }

    [Test]
    public void Validate_WithSundayDateTime_ReturnsInvalid()
    {
        var attr = new ValidateIsBusinessDayAttribute();
        var sunday = new System.DateTime(2025, 2, 23, 12, 0, 0, DateTimeKind.Utc); // Sunday
        var result = attr.Validate(sunday);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithWeekdayDateTimeOffset_ReturnsValid()
    {
        var attr = new ValidateIsBusinessDayAttribute();
        var monday = new DateTimeOffset(2025, 2, 17, 12, 0, 0, TimeSpan.Zero); // Monday
        var result = attr.Validate(monday);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithSaturdayDateTimeOffset_ReturnsInvalid()
    {
        var attr = new ValidateIsBusinessDayAttribute();
        var saturday = new DateTimeOffset(2025, 2, 22, 12, 0, 0, TimeSpan.Zero); // Saturday
        var result = attr.Validate(saturday);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithUnsupportedType_ReturnsInvalid()
    {
        var attr = new ValidateIsBusinessDayAttribute();
        var result = attr.Validate("not a date");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessageOnFailure()
    {
        var attr = new ValidateIsBusinessDayAttribute() { Message = "Must be a weekday" };
        var saturday = new System.DateTime(2025, 2, 22, 12, 0, 0, DateTimeKind.Utc);
        var result = attr.Validate(saturday);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Must be a weekday"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsBusinessDayAttribute();
        var saturday = new System.DateTime(2025, 2, 22, 12, 0, 0, DateTimeKind.Utc);
        var result = attr.Validate(saturday, "AppointmentDate");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("AppointmentDate"));
    }
}
