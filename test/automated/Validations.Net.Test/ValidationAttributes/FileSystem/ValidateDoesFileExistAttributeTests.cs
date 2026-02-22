namespace Validations.Net.Test.ValidationAttributes.FileSystem;

[TestFixture]
public class ValidateDoesFileExistAttributeTests
{
    [Test]
    public void Validate_WithExistingFile_ReturnsSuccess()
    {
        var path = Path.GetTempFileName();
        try
        {
            var attr = new ValidateDoesFileExistAttribute();
            var result = attr.Validate(path);
            Assert.That(result.IsValid, Is.True);
        }
        finally
        {
            File.Delete(path);
        }
    }

    [Test]
    public void Validate_WithNonExistentFile_ReturnsFailure()
    {
        var attr = new ValidateDoesFileExistAttribute();
        var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".tmp");
        var result = attr.Validate(path);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNonStringValue_ReturnsFailure()
    {
        var attr = new ValidateDoesFileExistAttribute();
        var result = attr.Validate(123);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateDoesFileExistAttribute { Message = "File must exist" };
        var result = attr.Validate("/nonexistent/path/file.txt");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("File must exist"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateDoesFileExistAttribute();
        var result = attr.Validate("/nonexistent/path", "FilePath");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("FilePath"));
    }
}
