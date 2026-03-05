using Validations.Net.OLD.Validators.DateTime;

namespace Validations.Net.Test.ValidationAttributes.DateTime;

[TestFixture]
public class ValidateIsInFutureAttributeTests
{
    [Test]
    public void Validate_WithFutureDateTime_ReturnsSuccess()
    {
        var attr = new ValidateIsInFutureAttribute();
        var future = System.DateTime.UtcNow.AddDays(1);
        var result = attr.Validate(future);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithPastDateTime_ReturnsFailure()
    {
        var attr = new ValidateIsInFutureAttribute();
        var past = System.DateTime.UtcNow.AddDays(-1);
        var result = attr.Validate(past);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsInFuture.ValidatorName));
    }

    [Test]
    public void Validate_WithFutureDateTimeOffset_ReturnsSuccess()
    {
        var attr = new ValidateIsInFutureAttribute();
        var future = DateTimeOffset.UtcNow.AddDays(1);
        var result = attr.Validate(future);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithNonDateTime_ReturnsFailure()
    {
        var attr = new ValidateIsInFutureAttribute();
        var result = attr.Validate("not a date");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessageOnFailure()
    {
        var attr = new ValidateIsInFutureAttribute { Message = "Must be future" };
        var past = System.DateTime.UtcNow.AddDays(-1);
        var result = attr.Validate(past);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Must be future"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsInFutureAttribute();
        var past = System.DateTime.UtcNow.AddDays(-1);
        var result = attr.Validate(past, "ExpiryDate");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("ExpiryDate"));
    }
}
