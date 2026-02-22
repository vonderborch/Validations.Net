namespace Validations.Net.Test.Validators.DataFormat;

[TestFixture]
public class IsValidBase64Tests
{
    [Test]
    public void CheckIsValidBase64_WithValidBase64_ReturnsTrue()
    {
        Assert.That(Convert.ToBase64String([72, 101, 108, 108, 111]).CheckIsValidBase64(), Is.True);
        Assert.That("SGVsbG8=".CheckIsValidBase64(), Is.True);
    }

    [Test]
    public void CheckIsValidBase64_WithInvalidBase64_ReturnsFalse()
    {
        Assert.That("!!!".CheckIsValidBase64(), Is.False);
        Assert.That("not-base64!!!".CheckIsValidBase64(), Is.False);
    }

    [Test]
    public void CheckIsValidBase64_WithNullOrEmpty_ReturnsFalse()
    {
        Assert.That(((string?)null).CheckIsValidBase64(), Is.False);
        Assert.That(string.Empty.CheckIsValidBase64(), Is.False);
    }

    [Test]
    public void ValidateIsValidBase64_WithValidBase64_ReturnsValid()
    {
        var result = "SGVsbG8=".ValidateIsValidBase64();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsValidBase64_WithInvalidBase64_ReturnsInvalid()
    {
        var result = "!!!".ValidateIsValidBase64();
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureIsValidBase64_WithInvalidBase64_Throws()
    {
        Assert.Throws<ValidationException>(() => "!!!".EnsureIsValidBase64());
    }
}
