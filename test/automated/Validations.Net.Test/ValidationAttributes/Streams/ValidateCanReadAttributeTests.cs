using Validations.Net.OLD.Validators.Streams;

namespace Validations.Net.Test.ValidationAttributes.Streams;

[TestFixture]
public class ValidateCanReadAttributeTests
{
    [Test]
    public void Validate_WithReadableMemoryStream_ReturnsSuccess()
    {
        var attr = new ValidateCanReadAttribute();
        using var stream = new MemoryStream([1, 2, 3]);
        var result = attr.Validate(stream);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithNonReadableStream_ReturnsFailure()
    {
        var attr = new ValidateCanReadAttribute();
        var result = attr.Validate(new WriteOnlyStream());
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNonStream_ReturnsFailure()
    {
        var attr = new ValidateCanReadAttribute();
        var result = attr.Validate("not a stream");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateCanReadAttribute { Message = "Stream must be readable" };
        var result = attr.Validate("not a stream");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Stream must be readable"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateCanReadAttribute();
        var result = attr.Validate("not a stream", "InputStream");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("InputStream"));
    }
}

file sealed class WriteOnlyStream : Stream
{
    public override bool CanRead => false;
    public override bool CanSeek => false;
    public override bool CanWrite => true;
    public override long Length => 0;
    public override long Position { get; set; }
    public override void Flush() { }
    public override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException();
    public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
    public override void SetLength(long value) => throw new NotSupportedException();
    public override void Write(byte[] buffer, int offset, int count) { }
}
