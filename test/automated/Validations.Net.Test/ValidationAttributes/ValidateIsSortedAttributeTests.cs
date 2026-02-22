namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsSortedAttributeTests
{
    [Test]
    public void Validate_WithSortedList_ReturnsSuccess()
    {
        var attr = new ValidateIsSortedAttribute();
        var result = attr.Validate(new List<int> { 1, 2, 3 });
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithUnsortedList_ReturnsFailure()
    {
        var attr = new ValidateIsSortedAttribute();
        var result = attr.Validate(new List<int> { 3, 1, 2 });
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithDescendingSortedList_WhenDescendingTrue_ReturnsSuccess()
    {
        var attr = new ValidateIsSortedAttribute { Descending = true };
        var result = attr.Validate(new List<int> { 3, 2, 1 });
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsSortedAttribute { Message = "Must be sorted" };
        var result = attr.Validate(new List<int> { 3, 1, 2 });
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must be sorted"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateIsSortedAttribute();
        var result = attr.Validate(new List<int> { 3, 1, 2 }, "Items");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Items"));
    }
}
