using System.IO;
using Validations.Net.OLD;
using Validations.Net.OLD.Validators.Streams;

namespace Validations.Net.Test.Validators.Streams;

[TestFixture]
public class CanWriteTests
{
    [Test]
    public void CheckCanWrite_WithWritableMemoryStream_ReturnsTrue()
    {
        using var ms = new MemoryStream();
        Assert.That(ms.CheckCanWrite(), Is.True);
    }

    [Test]
    public void CheckCanWrite_WithReadOnlyMemoryStream_ReturnsFalse()
    {
        var ms = new MemoryStream(new byte[] { 1 }, false);
        Assert.That(ms.CheckCanWrite(), Is.False);
    }

    [Test]
    public void CheckCanWrite_WithNull_ReturnsFalse()
    {
        Assert.That(((Stream?)null).CheckCanWrite(), Is.False);
    }

    [Test]
    public void ValidateCanWrite_WithWritableStream_ReturnsValid()
    {
        using var ms = new MemoryStream();
        var result = ms.ValidateCanWrite();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateCanWrite_WithNull_ReturnsInvalid()
    {
        var result = ((Stream?)null).ValidateCanWrite();
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureCanWrite_WithWritableStream_DoesNotThrow()
    {
        using var ms = new MemoryStream();
        Assert.DoesNotThrow(() => ms.EnsureCanWrite());
    }

    [Test]
    public void EnsureCanWrite_WithNull_Throws()
    {
        Assert.Throws<ValidationException>(() => ((Stream?)null).EnsureCanWrite());
    }
}
