using System;
using NUnit.Framework;
using Validations.Net.Validators.Strings;
using Validations.Net;

namespace Validations.Net.Test.Validators.Strings
{
    [TestFixture]
    public class TestDoesNotContainValidations
    {
        [Test]
        public void CheckDoesNotContain_String_DoesNotContainSubstring_ReturnsTrue()
        {
            Assert.That("hello world".CheckDoesNotContain("foo"), Is.True);
        }

        [Test]
        public void CheckDoesNotContain_String_ContainsSubstring_ReturnsFalse()
        {
            Assert.That("hello world".CheckDoesNotContain("world"), Is.False);
        }

        [Test]
        public void CheckDoesNotContain_String_DoesNotContainChar_ReturnsTrue()
        {
            Assert.That("hello world".CheckDoesNotContain('z'), Is.True);
        }

        [Test]
        public void CheckDoesNotContain_String_ContainsChar_ReturnsFalse()
        {
            Assert.That("hello world".CheckDoesNotContain('w'), Is.False);
        }

        [Test]
        public void ValidateDoesNotContain_String_DoesNotContainSubstring_ReturnsString()
        {
            var result = "hello world".ValidateDoesNotContain("foo", StringComparison.Ordinal, "test");
            Assert.That(result, Is.EqualTo("hello world"));
        }

        [Test]
        public void ValidateDoesNotContain_String_ContainsSubstring_Throws()
        {
            var ex = Assert.Throws<ValidationException>(() => "hello world".ValidateDoesNotContain("world", StringComparison.Ordinal, "test"));
            Assert.That(ex.Message, Does.Contain("must not contain 'world'"));
        }

        [Test]
        public void ValidateDoesNotContain_String_DoesNotContainChar_ReturnsString()
        {
            var result = "hello world".ValidateDoesNotContain('z', StringComparison.Ordinal, "test");
            Assert.That(result, Is.EqualTo("hello world"));
        }

        [Test]
        public void ValidateDoesNotContain_String_ContainsChar_Throws()
        {
            var ex = Assert.Throws<ValidationException>(() => "hello world".ValidateDoesNotContain('w', StringComparison.Ordinal, "test"));
            Assert.That(ex.Message, Does.Contain("must not contain 'w'"));
        }
    }
} 