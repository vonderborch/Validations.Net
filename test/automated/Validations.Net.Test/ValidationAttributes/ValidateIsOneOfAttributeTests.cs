namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsOneOfAttributeTests
{
    [Test]
    public void Validate_WhenValueInList_ReturnsSuccess()
    {
        var attr = new ValidateIsOneOfAttribute(1, 2, 3);
        var result = attr.Validate(2);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WhenValueNotInList_ReturnsFailure()
    {
        var attr = new ValidateIsOneOfAttribute(1, 2, 3);
        var result = attr.Validate(4);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsOneOf.ValidatorName));
    }

    [Test]
    public void Validate_WithStringValues_Works()
    {
        var attr = new ValidateIsOneOfAttribute("red", "green", "blue");
        Assert.That(attr.Validate("green").IsValid, Is.True);
        Assert.That(attr.Validate("yellow").IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessageOnFailure()
    {
        var attr = new ValidateIsOneOfAttribute(1, 2, 3) { Message = "Must be 1, 2, or 3" };
        var result = attr.Validate(4);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Must be 1, 2, or 3"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsOneOfAttribute(1, 2, 3);
        var result = attr.Validate(4, "Status");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Status"));
    }
}
