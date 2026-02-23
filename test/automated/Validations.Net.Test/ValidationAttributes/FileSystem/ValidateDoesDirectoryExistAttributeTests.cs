using Validations.Net.Validators.FileSystem;

namespace Validations.Net.Test.ValidationAttributes.FileSystem;

[TestFixture]
public class ValidateDoesDirectoryExistAttributeTests
{
    [Test]
    public void Validate_WithExistingDirectory_ReturnsSuccess()
    {
        var attr = new ValidateDoesDirectoryExistAttribute();
        var result = attr.Validate(Path.GetTempPath());
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithNonExistentDirectory_ReturnsFailure()
    {
        var attr = new ValidateDoesDirectoryExistAttribute();
        var result = attr.Validate("/nonexistent/path/that/does/not/exist");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNull_ReturnsFailure()
    {
        var attr = new ValidateDoesDirectoryExistAttribute();
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateDoesDirectoryExistAttribute { Message = "Dir required" };
        var result = attr.Validate("/nonexistent");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Dir required"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateDoesDirectoryExistAttribute();
        var result = attr.Validate("/nonexistent", "DirPath");
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("DirPath"));
    }
}
