namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsNotSortedAttributeTests
{
    [Test]
    public void Validate_WithUnsortedList_ReturnsSuccess()
    {
        var attr = new ValidateIsNotSortedAttribute();
        var result = attr.Validate(new List<int> { 3, 1, 2 });
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithSortedList_ReturnsFailure()
    {
        var attr = new ValidateIsNotSortedAttribute();
        var result = attr.Validate(new List<int> { 1, 2, 3 });
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithDescendingList_WhenDescendingTrue_ReturnsFailure()
    {
        var attr = new ValidateIsNotSortedAttribute { Descending = true };
        var result = attr.Validate(new List<int> { 3, 2, 1 });
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsNotSortedAttribute { Message = "Must not be sorted" };
        var result = attr.Validate(new List<int> { 1, 2, 3 });
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must not be sorted"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateIsNotSortedAttribute();
        var result = attr.Validate(new List<int> { 1, 2, 3 }, "Items");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Items"));
    }
}
