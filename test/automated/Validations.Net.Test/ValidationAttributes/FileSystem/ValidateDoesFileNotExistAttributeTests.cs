using Validations.Net.OLD.Validators.FileSystem;

namespace Validations.Net.Test.ValidationAttributes.FileSystem;

[TestFixture]
public class ValidateDoesFileNotExistAttributeTests
{
    [Test]
    public void Validate_WithNonExistentFile_ReturnsSuccess()
    {
        var attr = new ValidateDoesFileNotExistAttribute();
        var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".tmp");
        var result = attr.Validate(path);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithExistingFile_ReturnsFailure()
    {
        var path = Path.GetTempFileName();
        try
        {
            var attr = new ValidateDoesFileNotExistAttribute();
            var result = attr.Validate(path);
            Assert.That(result.IsValid, Is.False);
        }
        finally
        {
            File.Delete(path);
        }
    }

    [Test]
    public void Validate_WithNonStringValue_ReturnsSuccess()
    {
        var attr = new ValidateDoesFileNotExistAttribute();
        var result = attr.Validate(123);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var path = Path.GetTempFileName();
        try
        {
            var attr = new ValidateDoesFileNotExistAttribute { Message = "File must not exist" };
            var result = attr.Validate(path);
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ExceptionMessage, Does.Contain("File must not exist"));
        }
        finally
        {
            File.Delete(path);
        }
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var path = Path.GetTempFileName();
        try
        {
            var attr = new ValidateDoesFileNotExistAttribute();
            var result = attr.Validate(path, "FilePath");
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("FilePath"));
        }
        finally
        {
            File.Delete(path);
        }
    }
}
