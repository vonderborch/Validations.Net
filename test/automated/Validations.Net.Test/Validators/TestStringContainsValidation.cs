using System;
using System.Collections.Generic;
using NUnit.Framework;
using Validations.Net.Validators;
using Validations.Net;

namespace Validations.Net.Test.Validators
{
    [TestFixture]
    public class TestStringContainsValidation
    {
        [Test]
        public void DoesContain_SubstringExists_ReturnsTrue()
        {
            Assert.That(StringContainsValidation.DoesContain("hello world", "world"), Is.True);
        }

        [Test]
        public void DoesContain_SubstringDoesNotExist_ReturnsFalse()
        {
            Assert.That(StringContainsValidation.DoesContain("hello world", "planet"), Is.False);
        }

        [Test]
        public void DoesContain_SubstringExists_CaseInsensitive_ReturnsTrue()
        {
            Assert.That(StringContainsValidation.DoesContain("hello world", "WORLD", StringComparison.OrdinalIgnoreCase), Is.True);
        }

        [Test]
        public void DoesContain_SubstringDoesNotExist_CaseInsensitive_ReturnsFalse()
        {
            Assert.That(StringContainsValidation.DoesContain("hello world", "PLANET", StringComparison.OrdinalIgnoreCase), Is.False);
        }

        [Test]
        public void DoesContain_AllSubstringsExist_Array_ReturnsTrue()
        {
            Assert.That(StringContainsValidation.DoesContain("abc def ghi", new[] { "abc", "def" }), Is.True);
        }

        [Test]
        public void DoesContain_NotAllSubstringsExist_Array_ReturnsFalse()
        {
            Assert.That(StringContainsValidation.DoesContain("abc def ghi", new[] { "abc", "xyz" }), Is.False);
        }

        [Test]
        public void DoesContain_AllSubstringsExist_Array_CaseInsensitive_ReturnsTrue()
        {
            Assert.That(StringContainsValidation.DoesContain("abc DEF ghi", new[] { "abc", "def" }, StringComparison.OrdinalIgnoreCase), Is.True);
        }

        [Test]
        public void DoesContain_NotAllSubstringsExist_Array_CaseInsensitive_ReturnsFalse()
        {
            Assert.That(StringContainsValidation.DoesContain("abc DEF ghi", new[] { "abc", "xyz" }, StringComparison.OrdinalIgnoreCase), Is.False);
        }

        [Test]
        public void DoesContain_AllSubstringsExist_Enumerable_ReturnsTrue()
        {
            var substrings = new List<string> { "abc", "def" };
            Assert.That(StringContainsValidation.DoesContain("abc def ghi", substrings), Is.True);
        }

        [Test]
        public void DoesContain_NotAllSubstringsExist_Enumerable_ReturnsFalse()
        {
            var substrings = new List<string> { "abc", "xyz" };
            Assert.That(StringContainsValidation.DoesContain("abc def ghi", substrings), Is.False);
        }

        [Test]
        public void DoesContain_AllSubstringsExist_Enumerable_CaseInsensitive_ReturnsTrue()
        {
            var substrings = new List<string> { "abc", "def" };
            Assert.That(StringContainsValidation.DoesContain("abc DEF ghi", substrings, StringComparison.OrdinalIgnoreCase), Is.True);
        }

        [Test]
        public void DoesContain_NotAllSubstringsExist_Enumerable_CaseInsensitive_ReturnsFalse()
        {
            var substrings = new List<string> { "abc", "xyz" };
            Assert.That(StringContainsValidation.DoesContain("abc DEF ghi", substrings, StringComparison.OrdinalIgnoreCase), Is.False);
        }

        [Test]
        public void CheckDoesContain_SubstringExists_ReturnsTrue()
        {
            Assert.That("hello world".CheckDoesContain("world"), Is.True);
        }

        [Test]
        public void CheckDoesContain_SubstringDoesNotExist_ReturnsFalse()
        {
            Assert.That("hello world".CheckDoesContain("planet"), Is.False);
        }

        [Test]
        public void CheckDoesContain_SubstringExists_CaseInsensitive_ReturnsTrue()
        {
            Assert.That("hello world".CheckDoesContain("WORLD", StringComparison.OrdinalIgnoreCase), Is.True);
        }

        [Test]
        public void CheckDoesContain_SubstringDoesNotExist_CaseInsensitive_ReturnsFalse()
        {
            Assert.That("hello world".CheckDoesContain("PLANET", StringComparison.OrdinalIgnoreCase), Is.False);
        }

        [Test]
        public void CheckDoesContain_AllSubstringsExist_Array_ReturnsTrue()
        {
            Assert.That("abc def ghi".CheckDoesContain(new[] { "abc", "def" }), Is.True);
        }

        [Test]
        public void CheckDoesContain_NotAllSubstringsExist_Array_ReturnsFalse()
        {
            Assert.That("abc def ghi".CheckDoesContain(new[] { "abc", "xyz" }), Is.False);
        }

        [Test]
        public void CheckDoesContain_AllSubstringsExist_Array_CaseInsensitive_ReturnsTrue()
        {
            Assert.That("abc DEF ghi".CheckDoesContain(new[] { "abc", "def" }, StringComparison.OrdinalIgnoreCase), Is.True);
        }

        [Test]
        public void CheckDoesContain_NotAllSubstringsExist_Array_CaseInsensitive_ReturnsFalse()
        {
            Assert.That("abc DEF ghi".CheckDoesContain(new[] { "abc", "xyz" }, StringComparison.OrdinalIgnoreCase), Is.False);
        }

        [Test]
        public void CheckDoesContain_AllSubstringsExist_Enumerable_ReturnsTrue()
        {
            var substrings = new List<string> { "abc", "def" };
            Assert.That("abc def ghi".CheckDoesContain(substrings), Is.True);
        }

        [Test]
        public void CheckDoesContain_NotAllSubstringsExist_Enumerable_ReturnsFalse()
        {
            var substrings = new List<string> { "abc", "xyz" };
            Assert.That("abc def ghi".CheckDoesContain(substrings), Is.False);
        }

        [Test]
        public void CheckDoesContain_AllSubstringsExist_Enumerable_CaseInsensitive_ReturnsTrue()
        {
            var substrings = new List<string> { "abc", "def" };
            Assert.That("abc DEF ghi".CheckDoesContain(substrings, StringComparison.OrdinalIgnoreCase), Is.True);
        }

        [Test]
        public void CheckDoesContain_NotAllSubstringsExist_Enumerable_CaseInsensitive_ReturnsFalse()
        {
            var substrings = new List<string> { "abc", "xyz" };
            Assert.That("abc DEF ghi".CheckDoesContain(substrings, StringComparison.OrdinalIgnoreCase), Is.False);
        }

        [Test]
        public void DoesContainValidation_SubstringExists_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => StringContainsValidation.DoesContainValidation("hello world", "world", "value"));
        }

        [Test]
        public void DoesContainValidation_SubstringDoesNotExist_Throws()
        {
            Assert.Throws<ValidationException>(() => StringContainsValidation.DoesContainValidation("hello world", "planet", "value"));
        }

        [Test]
        public void ValidateDoesContain_SubstringExists_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => "hello world".ValidateDoesContain("world", "value"));
        }

        [Test]
        public void ValidateDoesContain_SubstringDoesNotExist_Throws()
        {
            Assert.Throws<ValidationException>(() => "hello world".ValidateDoesContain("planet", "value"));
        }
    }
} 