using System.IO;
using Validations.Net.OLD;
using Validations.Net.OLD.Validators.FileSystem;

namespace Validations.Net.Test.Validators.FileSystem;

[TestFixture]
public class DoesFileNotExistTests
{
    private string _tempDir = null!;
    private string _tempFile = null!;

    [SetUp]
    public void SetUp()
    {
        _tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(_tempDir);
        _tempFile = Path.Combine(_tempDir, "test.txt");
        File.WriteAllText(_tempFile, "test");
    }

    [TearDown]
    public void TearDown()
    {
        try
        {
            if (File.Exists(_tempFile)) File.Delete(_tempFile);
            if (Directory.Exists(_tempDir)) Directory.Delete(_tempDir);
        }
        catch { /* ignore */ }
    }

    [Test]
    public void CheckDoesFileNotExist_WithNonExistingPath_ReturnsTrue()
    {
        Assert.That(Path.Combine(_tempDir, "nonexistent.txt").CheckDoesFileNotExist(), Is.True);
    }

    [Test]
    public void CheckDoesFileNotExist_WithExistingFile_ReturnsFalse()
    {
        Assert.That(_tempFile.CheckDoesFileNotExist(), Is.False);
    }

    [Test]
    public void CheckDoesFileNotExist_WithNull_ReturnsTrue()
    {
        Assert.That(((string?)null).CheckDoesFileNotExist(), Is.True);
    }

    [Test]
    public void ValidateDoesFileNotExist_WithNonExistingPath_ReturnsValid()
    {
        var result = Path.Combine(_tempDir, "nonexistent.txt").ValidateDoesFileNotExist();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesFileNotExist_WithExistingFile_ReturnsInvalid()
    {
        var result = _tempFile.ValidateDoesFileNotExist();
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureDoesFileNotExist_WithNonExistingPath_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => Path.Combine(_tempDir, "nonexistent.txt").EnsureDoesFileNotExist());
    }

    [Test]
    public void EnsureDoesFileNotExist_WithExistingFile_Throws()
    {
        Assert.Throws<ValidationException>(() => _tempFile.EnsureDoesFileNotExist());
    }
}
