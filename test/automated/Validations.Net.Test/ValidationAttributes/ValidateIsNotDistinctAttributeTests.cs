using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsNotDistinctAttributeTests
{
    [Test]
    public void Validate_WithDuplicates_ReturnsSuccess()
    {
        var attr = new ValidateIsNotDistinctAttribute();
        var result = attr.Validate(new List<int> { 1, 2, 2, 3 });
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithDistinctList_ReturnsFailure()
    {
        var attr = new ValidateIsNotDistinctAttribute();
        var result = attr.Validate(new List<int> { 1, 2, 3 });
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNull_ReturnsFailure()
    {
        var attr = new ValidateIsNotDistinctAttribute();
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsNotDistinctAttribute { Message = "Must have duplicates" };
        var result = attr.Validate(new List<int> { 1, 2, 3 });
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must have duplicates"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateIsNotDistinctAttribute();
        var result = attr.Validate(new List<int> { 1, 2, 3 }, "Items");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Items"));
    }
}
