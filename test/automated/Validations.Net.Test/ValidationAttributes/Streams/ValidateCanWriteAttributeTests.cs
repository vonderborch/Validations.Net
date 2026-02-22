namespace Validations.Net.Test.ValidationAttributes.Streams;

[TestFixture]
public class ValidateCanWriteAttributeTests
{
    [Test]
    public void Validate_WithWritableMemoryStream_ReturnsSuccess()
    {
        var attr = new ValidateCanWriteAttribute();
        using var stream = new MemoryStream();
        var result = attr.Validate(stream);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithNonWritableStream_ReturnsFailure()
    {
        var attr = new ValidateCanWriteAttribute();
        var result = attr.Validate(new ReadOnlyStream());
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNonStream_ReturnsFailure()
    {
        var attr = new ValidateCanWriteAttribute();
        var result = attr.Validate("not a stream");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateCanWriteAttribute { Message = "Stream must be writable" };
        var result = attr.Validate("not a stream");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Stream must be writable"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateCanWriteAttribute();
        var result = attr.Validate("not a stream", "OutputStream");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("OutputStream"));
    }
}

file sealed class ReadOnlyStream : Stream
{
    private readonly byte[] _data = [1, 2, 3];
    public override bool CanRead => true;
    public override bool CanSeek => false;
    public override bool CanWrite => false;
    public override long Length => _data.Length;
    public override long Position { get; set; }
    public override void Flush() { }
    public override int Read(byte[] buffer, int offset, int count)
    {
        var toRead = Math.Min(count, _data.Length - (int)Position);
        Array.Copy(_data, (int)Position, buffer, offset, toRead);
        Position += toRead;
        return toRead;
    }
    public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
    public override void SetLength(long value) => throw new NotSupportedException();
    public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();
}
