namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateEachIsValidAttributeTests
{
    [Test]
    public void Validate_AlwaysReturnsSuccess()
    {
        var attr = new ValidateEachIsValidAttribute();
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCollection_ReturnsSuccess()
    {
        var attr = new ValidateEachIsValidAttribute();
        var result = attr.Validate(new[] { 1, 2, 3 });
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithEmptyCollection_ReturnsSuccess()
    {
        var attr = new ValidateEachIsValidAttribute();
        var result = attr.Validate(Array.Empty<int>());
        Assert.That(result.IsValid, Is.True);
    }
}
