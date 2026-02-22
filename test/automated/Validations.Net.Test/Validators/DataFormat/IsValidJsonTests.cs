namespace Validations.Net.Test.Validators.DataFormat;

[TestFixture]
public class IsValidJsonTests
{
    [Test]
    public void CheckIsValidJson_WithValidJson_ReturnsTrue()
    {
        Assert.That("{}".CheckIsValidJson(), Is.True);
        Assert.That("{\"key\":\"value\"}".CheckIsValidJson(), Is.True);
        Assert.That("[1,2,3]".CheckIsValidJson(), Is.True);
    }

    [Test]
    public void CheckIsValidJson_WithInvalidJson_ReturnsFalse()
    {
        Assert.That("{".CheckIsValidJson(), Is.False);
        Assert.That("not json".CheckIsValidJson(), Is.False);
    }

    [Test]
    public void CheckIsValidJson_WithNullOrEmpty_ReturnsFalse()
    {
        Assert.That(((string?)null).CheckIsValidJson(), Is.False);
        Assert.That(string.Empty.CheckIsValidJson(), Is.False);
    }

    [Test]
    public void ValidateIsValidJson_WithValidJson_ReturnsValid()
    {
        var result = "{}".ValidateIsValidJson();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsValidJson_WithInvalidJson_ReturnsInvalid()
    {
        var result = "{".ValidateIsValidJson();
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureIsValidJson_WithInvalidJson_Throws()
    {
        Assert.Throws<ValidationException>(() => "invalid".EnsureIsValidJson());
    }
}
