using System.IO;
using Validations.Net.OLD;
using Validations.Net.OLD.Validators.FileSystem;

namespace Validations.Net.Test.Validators.FileSystem;

[TestFixture]
public class DoesFileExistTests
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
    public void CheckDoesFileExist_WithExistingFile_ReturnsTrue()
    {
        Assert.That(_tempFile.CheckDoesFileExist(), Is.True);
    }

    [Test]
    public void CheckDoesFileExist_WithNonExistingPath_ReturnsFalse()
    {
        Assert.That(Path.Combine(_tempDir, "nonexistent.txt").CheckDoesFileExist(), Is.False);
    }

    [Test]
    public void CheckDoesFileExist_WithNull_ReturnsFalse()
    {
        Assert.That(((string?)null).CheckDoesFileExist(), Is.False);
    }

    [Test]
    public void CheckDoesFileExist_WithEmptyString_ReturnsFalse()
    {
        Assert.That("".CheckDoesFileExist(), Is.False);
    }

    [Test]
    public void CheckDoesFileExist_WithWhitespace_ReturnsFalse()
    {
        Assert.That("   ".CheckDoesFileExist(), Is.False);
    }

    [Test]
    public void ValidateDoesFileExist_WithExistingFile_ReturnsValid()
    {
        var result = _tempFile.ValidateDoesFileExist();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesFileExist_WithNonExistingPath_ReturnsInvalid()
    {
        var result = "C:\\nonexistent\\file.txt".ValidateDoesFileExist();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(DoesFileExist.ValidatorName));
    }

    [Test]
    public void EnsureDoesFileExist_WithExistingFile_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => _tempFile.EnsureDoesFileExist());
    }

    [Test]
    public void EnsureDoesFileExist_WithNonExistingPath_Throws()
    {
        Assert.Throws<ValidationException>(() => "C:\\nonexistent\\file.txt".EnsureDoesFileExist());
    }
}
