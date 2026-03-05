using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsCountAttributeTests
{
    [Test]
    public void Validate_WithCollectionOfExactCount_ReturnsSuccess()
    {
        var attr = new ValidateIsCountAttribute(3);
        var result = attr.Validate(new[] { 1, 2, 3 });
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCollectionOfDifferentCount_ReturnsFailure()
    {
        var attr = new ValidateIsCountAttribute(3);
        var result = attr.Validate(new[] { 1, 2 });
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithEnumerableOfExactCount_ReturnsSuccess()
    {
        var attr = new ValidateIsCountAttribute(2);
        var result = attr.Validate(new List<int> { 10, 20 });
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsCountAttribute(3) { Message = "Must have exactly 3 items" };
        var result = attr.Validate(new[] { 1, 2 });
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must have exactly 3 items"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateIsCountAttribute(3);
        var result = attr.Validate(new[] { 1, 2 }, "Items");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Items"));
    }
}
