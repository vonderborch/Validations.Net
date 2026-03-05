using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsNotOneOfAttributeTests
{
    [Test]
    public void Validate_WhenValueNotInList_ReturnsSuccess()
    {
        var attr = new ValidateIsNotOneOfAttribute(1, 2, 3);
        var result = attr.Validate(4);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WhenValueInList_ReturnsFailure()
    {
        var attr = new ValidateIsNotOneOfAttribute(1, 2, 3);
        var result = attr.Validate(2);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsNotOneOf.ValidatorName));
    }

    [Test]
    public void Validate_WithStringValues_Works()
    {
        var attr = new ValidateIsNotOneOfAttribute("forbidden", "blocked");
        Assert.That(attr.Validate("allowed").IsValid, Is.True);
        Assert.That(attr.Validate("forbidden").IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessageOnFailure()
    {
        var attr = new ValidateIsNotOneOfAttribute(1, 2, 3) { Message = "Must not be 1, 2, or 3" };
        var result = attr.Validate(2);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Must not be 1, 2, or 3"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsNotOneOfAttribute(1, 2, 3);
        var result = attr.Validate(2, "ExcludedStatus");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("ExcludedStatus"));
    }
}
