using Validations.Net.OLD.Validators.DateTime;

namespace Validations.Net.Test.ValidationAttributes.DateTime;

[TestFixture]
public class ValidateIsLeapYearAttributeTests
{
    [Test]
    public void Validate_WithLeapYearInt_ReturnsSuccess()
    {
        var attr = new ValidateIsLeapYearAttribute();
        var result = attr.Validate(2024);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithNonLeapYearInt_ReturnsFailure()
    {
        var attr = new ValidateIsLeapYearAttribute();
        var result = attr.Validate(2023);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithDateTimeInLeapYear_ReturnsSuccess()
    {
        var attr = new ValidateIsLeapYearAttribute();
        var dt = new System.DateTime(2024, 2, 29, 0, 0, 0, DateTimeKind.Utc);
        var result = attr.Validate(dt);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsLeapYearAttribute { Message = "Must be leap year" };
        var result = attr.Validate(2023);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must be leap year"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsLeapYearAttribute();
        var result = attr.Validate(2023, "Year");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Year"));
    }
}
