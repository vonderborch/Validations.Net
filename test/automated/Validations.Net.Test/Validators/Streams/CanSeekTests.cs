using System.IO;
using Validations.Net.OLD;
using Validations.Net.OLD.Validators.Streams;

namespace Validations.Net.Test.Validators.Streams;

[TestFixture]
public class CanSeekTests
{
    [Test]
    public void CheckCanSeek_WithSeekableMemoryStream_ReturnsTrue()
    {
        using var ms = new MemoryStream(new byte[] { 1, 2, 3 });
        Assert.That(ms.CheckCanSeek(), Is.True);
    }

    [Test]
    public void CheckCanSeek_WithNull_ReturnsFalse()
    {
        Assert.That(((Stream?)null).CheckCanSeek(), Is.False);
    }

    [Test]
    public void ValidateCanSeek_WithSeekableStream_ReturnsValid()
    {
        using var ms = new MemoryStream(new byte[] { 1 });
        var result = ms.ValidateCanSeek();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateCanSeek_WithNull_ReturnsInvalid()
    {
        var result = ((Stream?)null).ValidateCanSeek();
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureCanSeek_WithSeekableStream_DoesNotThrow()
    {
        using var ms = new MemoryStream(new byte[] { 1 });
        Assert.DoesNotThrow(() => ms.EnsureCanSeek());
    }

    [Test]
    public void EnsureCanSeek_WithNull_Throws()
    {
        Assert.Throws<ValidationException>(() => ((Stream?)null).EnsureCanSeek());
    }
}
