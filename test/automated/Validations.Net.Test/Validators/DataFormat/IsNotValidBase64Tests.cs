namespace Validations.Net.Test.Validators.DataFormat;

[TestFixture]
public class IsNotValidBase64Tests
{
    [Test]
    public void CheckIsNotValidBase64_WithInvalidBase64_ReturnsTrue()
    {
        Assert.That("!!!".CheckIsNotValidBase64(), Is.True);
    }

    [Test]
    public void CheckIsNotValidBase64_WithValidBase64_ReturnsFalse()
    {
        Assert.That("SGVsbG8=".CheckIsNotValidBase64(), Is.False);
    }

    [Test]
    public void ValidateIsNotValidBase64_WithInvalidBase64_ReturnsValid()
    {
        var result = "!!!".ValidateIsNotValidBase64();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsNotValidBase64_WithValidBase64_ReturnsInvalid()
    {
        var result = "SGVsbG8=".ValidateIsNotValidBase64();
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureIsNotValidBase64_WithValidBase64_Throws()
    {
        Assert.Throws<ValidationException>(() => "SGVsbG8=".EnsureIsNotValidBase64());
    }
}
