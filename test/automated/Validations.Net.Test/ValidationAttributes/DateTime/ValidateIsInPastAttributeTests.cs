namespace Validations.Net.Test.ValidationAttributes.DateTime;

[TestFixture]
public class ValidateIsInPastAttributeTests
{
    [Test]
    public void Validate_WithPastDateTime_ReturnsSuccess()
    {
        var attr = new ValidateIsInPastAttribute();
        var past = System.DateTime.UtcNow.AddDays(-1);
        var result = attr.Validate(past);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithFutureDateTime_ReturnsFailure()
    {
        var attr = new ValidateIsInPastAttribute();
        var future = System.DateTime.UtcNow.AddDays(1);
        var result = attr.Validate(future);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsInPast.ValidatorName));
    }

    [Test]
    public void Validate_WithPastDateTimeOffset_ReturnsSuccess()
    {
        var attr = new ValidateIsInPastAttribute();
        var past = DateTimeOffset.UtcNow.AddDays(-1);
        var result = attr.Validate(past);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithNonDateTime_ReturnsFailure()
    {
        var attr = new ValidateIsInPastAttribute();
        var result = attr.Validate("not a date");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessageOnFailure()
    {
        var attr = new ValidateIsInPastAttribute { Message = "Must be past" };
        var future = System.DateTime.UtcNow.AddDays(1);
        var result = attr.Validate(future);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Must be past"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsInPastAttribute();
        var future = System.DateTime.UtcNow.AddDays(1);
        var result = attr.Validate(future, "BirthDate");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("BirthDate"));
    }
}
