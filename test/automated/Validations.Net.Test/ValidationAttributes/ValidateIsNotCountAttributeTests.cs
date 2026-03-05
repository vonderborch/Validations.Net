using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsNotCountAttributeTests
{
    [Test]
    public void Validate_WithCollectionOfDifferentCount_ReturnsSuccess()
    {
        var attr = new ValidateIsNotCountAttribute(3);
        var result = attr.Validate(new[] { 1, 2 });
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCollectionOfExactCount_ReturnsFailure()
    {
        var attr = new ValidateIsNotCountAttribute(3);
        var result = attr.Validate(new[] { 1, 2, 3 });
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNull_ReturnsSuccess()
    {
        var attr = new ValidateIsNotCountAttribute(3);
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsNotCountAttribute(3) { Message = "Must not have 3 items" };
        var result = attr.Validate(new[] { 1, 2, 3 });
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must not have 3 items"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateIsNotCountAttribute(3);
        var result = attr.Validate(new[] { 1, 2, 3 }, "Items");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Items"));
    }
}
