using Validations.Net.OLD.Validators.FileSystem;

namespace Validations.Net.Test.ValidationAttributes.FileSystem;

[TestFixture]
public class ValidateDoesDirectoryNotExistAttributeTests
{
    [Test]
    public void Validate_WithNonExistentDirectory_ReturnsSuccess()
    {
        var attr = new ValidateDoesDirectoryNotExistAttribute();
        var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        var result = attr.Validate(path);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithExistingDirectory_ReturnsFailure()
    {
        var attr = new ValidateDoesDirectoryNotExistAttribute();
        var path = Path.GetTempPath();
        var result = attr.Validate(path);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNonStringValue_ReturnsSuccess()
    {
        var attr = new ValidateDoesDirectoryNotExistAttribute();
        var result = attr.Validate(123);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateDoesDirectoryNotExistAttribute { Message = "Directory must not exist" };
        var result = attr.Validate(Path.GetTempPath());
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Directory must not exist"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateDoesDirectoryNotExistAttribute();
        var result = attr.Validate(Path.GetTempPath(), "DirectoryPath");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("DirectoryPath"));
    }
}
