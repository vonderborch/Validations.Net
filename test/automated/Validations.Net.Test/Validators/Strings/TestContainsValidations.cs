using System;
using NUnit.Framework;
using Validations.Net.Validators.Strings;
using Validations.Net;

namespace Validations.Net.Test.Validators.Strings
{
    [TestFixture]
    public class TestContainsValidations
    {
        [Test]
        public void CheckContains_String_ContainsSubstring_ReturnsTrue()
        {
            Assert.That("hello world".CheckContains("world"), Is.True);
        }

        [Test]
        public void CheckContains_String_DoesNotContainSubstring_ReturnsFalse()
        {
            Assert.That("hello world".CheckContains("foo"), Is.False);
        }

        [Test]
        public void CheckContains_String_ContainsChar_ReturnsTrue()
        {
            Assert.That("hello world".CheckContains('w'), Is.True);
        }

        [Test]
        public void CheckContains_String_DoesNotContainChar_ReturnsFalse()
        {
            Assert.That("hello world".CheckContains('z'), Is.False);
        }

        [Test]
        public void ValidateContains_String_ContainsSubstring_ReturnsString()
        {
            var result = "hello world".ValidateContains("world", StringComparison.Ordinal, "test");
            Assert.That(result, Is.EqualTo("hello world"));
        }

        [Test]
        public void ValidateContains_StringDoesNotContainSubstring_Throws()
        {
            var ex = Assert.Throws<ValidationException>(() => "hello world".ValidateContains("foo", StringComparison.Ordinal, "test"));
            Assert.That(ex.Message, Does.Contain("must contain 'foo'"));
        }

        [Test]
        public void ValidateContains_String_ContainsChar_ReturnsString()
        {
            var result = "hello world".ValidateContains('w', StringComparison.Ordinal, "test");
            Assert.That(result, Is.EqualTo("hello world"));
        }

        [Test]
        public void ValidateContains_StringDoesNotContainChar_Throws()
        {
            var ex = Assert.Throws<ValidationException>(() => "hello world".ValidateContains('z', StringComparison.Ordinal, "test"));
            Assert.That(ex.Message, Does.Contain("must contain 'z'"));
        }
    }
} 