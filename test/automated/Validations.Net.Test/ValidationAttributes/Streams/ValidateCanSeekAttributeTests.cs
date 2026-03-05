using Validations.Net.OLD.Validators.Streams;

namespace Validations.Net.Test.ValidationAttributes.Streams;

[TestFixture]
public class ValidateCanSeekAttributeTests
{
    [Test]
    public void Validate_WithSeekableMemoryStream_ReturnsSuccess()
    {
        var attr = new ValidateCanSeekAttribute();
        using var stream = new MemoryStream([1, 2, 3]);
        var result = attr.Validate(stream);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithNonSeekableStream_ReturnsFailure()
    {
        var attr = new ValidateCanSeekAttribute();
        var result = attr.Validate(new NonSeekableStream());
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNonStream_ReturnsFailure()
    {
        var attr = new ValidateCanSeekAttribute();
        var result = attr.Validate("not a stream");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateCanSeekAttribute { Message = "Stream must be seekable" };
        var result = attr.Validate("not a stream");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Stream must be seekable"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateCanSeekAttribute();
        var result = attr.Validate("not a stream", "DataStream");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("DataStream"));
    }
}

file sealed class NonSeekableStream : Stream
{
    private readonly MemoryStream _inner = new([1, 2, 3]);
    public override bool CanRead => true;
    public override bool CanSeek => false;
    public override bool CanWrite => true;
    public override long Length => _inner.Length;
    public override long Position { get => _inner.Position; set => _inner.Position = value; }
    public override void Flush() => _inner.Flush();
    public override int Read(byte[] buffer, int offset, int count) => _inner.Read(buffer, offset, count);
    public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
    public override void SetLength(long value) => _inner.SetLength(value);
    public override void Write(byte[] buffer, int offset, int count) => _inner.Write(buffer, offset, count);
    protected override void Dispose(bool disposing) => _inner.Dispose();
}
