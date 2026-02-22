using System.IO;
using Validations.Net.Validators.Streams;

namespace Validations.Net.Test.Validators.Streams;

[TestFixture]
public class CanReadTests
{
    [Test]
    public void CheckCanRead_WithReadableMemoryStream_ReturnsTrue()
    {
        using var ms = new MemoryStream(new byte[] { 1, 2, 3 });
        Assert.That(ms.CheckCanRead(), Is.True);
    }

    [Test]
    public void CheckCanRead_WithReadableStream_ReturnsTrueForMultipleCalls()
    {
        using var ms = new MemoryStream(new byte[] { 1, 2, 3 });
        Assert.That(ms.CheckCanRead(), Is.True);
        ms.ReadByte();
        Assert.That(ms.CheckCanRead(), Is.True);
    }

    [Test]
    public void CheckCanRead_WithNull_ReturnsFalse()
    {
        Assert.That(((Stream?)null).CheckCanRead(), Is.False);
    }

    [Test]
    public void ValidateCanRead_WithReadableStream_ReturnsValid()
    {
        using var ms = new MemoryStream(new byte[] { 1 });
        var result = ms.ValidateCanRead();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateCanRead_WithNull_ReturnsInvalid()
    {
        var result = ((Stream?)null).ValidateCanRead();
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureCanRead_WithReadableStream_DoesNotThrow()
    {
        using var ms = new MemoryStream(new byte[] { 1 });
        Assert.DoesNotThrow(() => ms.EnsureCanRead());
    }

    [Test]
    public void EnsureCanRead_WithNull_Throws()
    {
        Assert.Throws<ValidationException>(() => ((Stream?)null).EnsureCanRead());
    }
}
