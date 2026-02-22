using System.IO;
using Validations.Net.Validators.FileSystem;

namespace Validations.Net.Test.Validators.FileSystem;

[TestFixture]
public class DoesDirectoryExistTests
{
    private string _tempDir = null!;

    [SetUp]
    public void SetUp()
    {
        _tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(_tempDir);
    }

    [TearDown]
    public void TearDown()
    {
        try
        {
            if (Directory.Exists(_tempDir)) Directory.Delete(_tempDir);
        }
        catch { /* ignore */ }
    }

    [Test]
    public void CheckDoesDirectoryExist_WithExistingDirectory_ReturnsTrue()
    {
        Assert.That(_tempDir.CheckDoesDirectoryExist(), Is.True);
    }

    [Test]
    public void CheckDoesDirectoryExist_WithNonExistingPath_ReturnsFalse()
    {
        Assert.That(Path.Combine(_tempDir, "nonexistent").CheckDoesDirectoryExist(), Is.False);
    }

    [Test]
    public void CheckDoesDirectoryExist_WithNull_ReturnsFalse()
    {
        Assert.That(((string?)null).CheckDoesDirectoryExist(), Is.False);
    }

    [Test]
    public void ValidateDoesDirectoryExist_WithExistingDirectory_ReturnsValid()
    {
        var result = _tempDir.ValidateDoesDirectoryExist();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesDirectoryExist_WithNonExistingPath_ReturnsInvalid()
    {
        var result = "C:\\nonexistent\\dir".ValidateDoesDirectoryExist();
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureDoesDirectoryExist_WithExistingDirectory_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => _tempDir.EnsureDoesDirectoryExist());
    }

    [Test]
    public void EnsureDoesDirectoryExist_WithNonExistingPath_Throws()
    {
        Assert.Throws<ValidationException>(() => "C:\\nonexistent\\dir".EnsureDoesDirectoryExist());
    }
}
