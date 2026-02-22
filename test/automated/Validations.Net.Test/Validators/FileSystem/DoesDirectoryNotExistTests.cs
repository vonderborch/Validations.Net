using System.IO;
using Validations.Net.Validators.FileSystem;

namespace Validations.Net.Test.Validators.FileSystem;

[TestFixture]
public class DoesDirectoryNotExistTests
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
    public void CheckDoesDirectoryNotExist_WithNonExistingPath_ReturnsTrue()
    {
        Assert.That(Path.Combine(_tempDir, "nonexistent").CheckDoesDirectoryNotExist(), Is.True);
    }

    [Test]
    public void CheckDoesDirectoryNotExist_WithExistingDirectory_ReturnsFalse()
    {
        Assert.That(_tempDir.CheckDoesDirectoryNotExist(), Is.False);
    }

    [Test]
    public void CheckDoesDirectoryNotExist_WithNull_ReturnsTrue()
    {
        Assert.That(((string?)null).CheckDoesDirectoryNotExist(), Is.True);
    }

    [Test]
    public void ValidateDoesDirectoryNotExist_WithNonExistingPath_ReturnsValid()
    {
        var result = Path.Combine(_tempDir, "nonexistent").ValidateDoesDirectoryNotExist();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesDirectoryNotExist_WithExistingDirectory_ReturnsInvalid()
    {
        var result = _tempDir.ValidateDoesDirectoryNotExist();
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureDoesDirectoryNotExist_WithNonExistingPath_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => Path.Combine(_tempDir, "nonexistent").EnsureDoesDirectoryNotExist());
    }

    [Test]
    public void EnsureDoesDirectoryNotExist_WithExistingDirectory_Throws()
    {
        Assert.Throws<ValidationException>(() => _tempDir.EnsureDoesDirectoryNotExist());
    }
}
