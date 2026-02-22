namespace Validations.Net.Test.ValidationAttributes.DateTime;

[TestFixture]
public class ValidateIsDateOnlyAttributeTests
{
    [Test]
    public void Validate_WithMidnightDateTime_ReturnsSuccess()
    {
        var attr = new ValidateIsDateOnlyAttribute();
        var dateOnly = new System.DateTime(2025, 2, 21, 0, 0, 0, DateTimeKind.Utc);
        var result = attr.Validate(dateOnly);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithNonMidnightDateTime_ReturnsFailure()
    {
        var attr = new ValidateIsDateOnlyAttribute();
        var withTime = new System.DateTime(2025, 2, 21, 12, 0, 0, DateTimeKind.Utc);
        var result = attr.Validate(withTime);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsDateOnly.ValidatorName));
    }

    [Test]
    public void Validate_WithMidnightDateTimeOffset_ReturnsSuccess()
    {
        var attr = new ValidateIsDateOnlyAttribute();
        var dateOnly = new DateTimeOffset(2025, 2, 21, 0, 0, 0, TimeSpan.Zero);
        var result = attr.Validate(dateOnly);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithNonDateTime_ReturnsFailure()
    {
        var attr = new ValidateIsDateOnlyAttribute();
        var result = attr.Validate("not a date");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessageOnFailure()
    {
        var attr = new ValidateIsDateOnlyAttribute { Message = "Must be date only" };
        var withTime = new System.DateTime(2025, 2, 21, 12, 0, 0, DateTimeKind.Utc);
        var result = attr.Validate(withTime);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Must be date only"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsDateOnlyAttribute();
        var withTime = new System.DateTime(2025, 2, 21, 12, 0, 0, DateTimeKind.Utc);
        var result = attr.Validate(withTime, "EventDate");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("EventDate"));
    }
}
