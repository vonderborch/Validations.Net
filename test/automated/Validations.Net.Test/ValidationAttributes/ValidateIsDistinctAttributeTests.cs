using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsDistinctAttributeTests
{
    [Test]
    public void Validate_WithDistinctList_ReturnsSuccess()
    {
        var attr = new ValidateIsDistinctAttribute();
        var result = attr.Validate(new List<int> { 1, 2, 3 });
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithDuplicates_ReturnsFailure()
    {
        var attr = new ValidateIsDistinctAttribute();
        var result = attr.Validate(new List<int> { 1, 2, 2, 3 });
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNull_ReturnsFailure()
    {
        var attr = new ValidateIsDistinctAttribute();
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsDistinctAttribute { Message = "All items must be unique" };
        var result = attr.Validate(new List<int> { 1, 1 });
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("All items must be unique"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateIsDistinctAttribute();
        var result = attr.Validate(new List<int> { 1, 1 }, "Items");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Items"));
    }
}
