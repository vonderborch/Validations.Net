using System;
using System.Collections.Generic;
using NUnit.Framework;
using Validations.Net.Validators;
using Validations.Net;

namespace Validations.Net.Test.Validators
{
    [TestFixture]
    public class TestStringDoesNotContainValidation
    {
        [Test]
        public void DoesNotContain_SubstringDoesNotExist_ReturnsTrue()
        {
            Assert.That(StringDoesNotContainValidation.DoesNotContain("hello world", "planet"), Is.True);
        }

        [Test]
        public void DoesNotContain_SubstringExists_ReturnsFalse()
        {
            Assert.That(StringDoesNotContainValidation.DoesNotContain("hello world", "world"), Is.False);
        }

        [Test]
        public void DoesNotContain_SubstringDoesNotExist_CaseInsensitive_ReturnsTrue()
        {
            Assert.That(StringDoesNotContainValidation.DoesNotContain("hello world", "PLANET", StringComparison.OrdinalIgnoreCase), Is.True);
        }

        [Test]
        public void DoesNotContain_SubstringExists_CaseInsensitive_ReturnsFalse()
        {
            Assert.That(StringDoesNotContainValidation.DoesNotContain("hello world", "WORLD", StringComparison.OrdinalIgnoreCase), Is.False);
        }

        [Test]
        public void DoesNotContain_NoneOfSubstringsExist_Array_ReturnsTrue()
        {
            Assert.That(StringDoesNotContainValidation.DoesNotContain("abc def ghi", new[] { "xyz", "uvw" }), Is.True);
        }

        [Test]
        public void DoesNotContain_OneSubstringExists_Array_ReturnsFalse()
        {
            Assert.That(StringDoesNotContainValidation.DoesNotContain("abc def ghi", new[] { "abc", "xyz" }), Is.False);
        }

        [Test]
        public void DoesNotContain_NoneOfSubstringsExist_Array_CaseInsensitive_ReturnsTrue()
        {
            Assert.That(StringDoesNotContainValidation.DoesNotContain("abc DEF ghi", new[] { "xyz", "uvw" }, StringComparison.OrdinalIgnoreCase), Is.True);
        }

        [Test]
        public void DoesNotContain_OneSubstringExists_Array_CaseInsensitive_ReturnsFalse()
        {
            Assert.That(StringDoesNotContainValidation.DoesNotContain("abc DEF ghi", new[] { "def", "xyz" }, StringComparison.OrdinalIgnoreCase), Is.False);
        }

        [Test]
        public void DoesNotContain_NoneOfSubstringsExist_Enumerable_ReturnsTrue()
        {
            var substrings = new List<string> { "xyz", "uvw" };
            Assert.That(StringDoesNotContainValidation.DoesNotContain("abc def ghi", substrings), Is.True);
        }

        [Test]
        public void DoesNotContain_OneSubstringExists_Enumerable_ReturnsFalse()
        {
            var substrings = new List<string> { "abc", "xyz" };
            Assert.That(StringDoesNotContainValidation.DoesNotContain("abc def ghi", substrings), Is.False);
        }

        [Test]
        public void DoesNotContain_NoneOfSubstringsExist_Enumerable_CaseInsensitive_ReturnsTrue()
        {
            var substrings = new List<string> { "xyz", "uvw" };
            Assert.That(StringDoesNotContainValidation.DoesNotContain("abc DEF ghi", substrings, StringComparison.OrdinalIgnoreCase), Is.True);
        }

        [Test]
        public void DoesNotContain_OneSubstringExists_Enumerable_CaseInsensitive_ReturnsFalse()
        {
            var substrings = new List<string> { "def", "xyz" };
            Assert.That(StringDoesNotContainValidation.DoesNotContain("abc DEF ghi", substrings, StringComparison.OrdinalIgnoreCase), Is.False);
        }

        [Test]
        public void CheckDoesNotContain_SubstringDoesNotExist_ReturnsTrue()
        {
            Assert.That("hello world".CheckDoesNotContain("planet"), Is.True);
        }

        [Test]
        public void CheckDoesNotContain_SubstringExists_ReturnsFalse()
        {
            Assert.That("hello world".CheckDoesNotContain("world"), Is.False);
        }

        [Test]
        public void CheckDoesNotContain_SubstringDoesNotExist_CaseInsensitive_ReturnsTrue()
        {
            Assert.That("hello world".CheckDoesNotContain("PLANET", StringComparison.OrdinalIgnoreCase), Is.True);
        }

        [Test]
        public void CheckDoesNotContain_SubstringExists_CaseInsensitive_ReturnsFalse()
        {
            Assert.That("hello world".CheckDoesNotContain("WORLD", StringComparison.OrdinalIgnoreCase), Is.False);
        }

        [Test]
        public void CheckDoesNotContain_NoneOfSubstringsExist_Array_ReturnsTrue()
        {
            Assert.That("abc def ghi".CheckDoesNotContain(new[] { "xyz", "uvw" }), Is.True);
        }

        [Test]
        public void CheckDoesNotContain_OneSubstringExists_Array_ReturnsFalse()
        {
            Assert.That("abc def ghi".CheckDoesNotContain(new[] { "abc", "xyz" }), Is.False);
        }

        [Test]
        public void CheckDoesNotContain_NoneOfSubstringsExist_Array_CaseInsensitive_ReturnsTrue()
        {
            Assert.That("abc DEF ghi".CheckDoesNotContain(new[] { "xyz", "uvw" }, StringComparison.OrdinalIgnoreCase), Is.True);
        }

        [Test]
        public void CheckDoesNotContain_OneSubstringExists_Array_CaseInsensitive_ReturnsFalse()
        {
            Assert.That("abc DEF ghi".CheckDoesNotContain(new[] { "def", "xyz" }, StringComparison.OrdinalIgnoreCase), Is.False);
        }

        [Test]
        public void CheckDoesNotContain_NoneOfSubstringsExist_Enumerable_ReturnsTrue()
        {
            var substrings = new List<string> { "xyz", "uvw" };
            Assert.That("abc def ghi".CheckDoesNotContain(substrings), Is.True);
        }

        [Test]
        public void CheckDoesNotContain_OneSubstringExists_Enumerable_ReturnsFalse()
        {
            var substrings = new List<string> { "abc", "xyz" };
            Assert.That("abc def ghi".CheckDoesNotContain(substrings), Is.False);
        }

        [Test]
        public void CheckDoesNotContain_NoneOfSubstringsExist_Enumerable_CaseInsensitive_ReturnsTrue()
        {
            var substrings = new List<string> { "xyz", "uvw" };
            Assert.That("abc DEF ghi".CheckDoesNotContain(substrings, StringComparison.OrdinalIgnoreCase), Is.True);
        }

        [Test]
        public void CheckDoesNotContain_OneSubstringExists_Enumerable_CaseInsensitive_ReturnsFalse()
        {
            var substrings = new List<string> { "def", "xyz" };
            Assert.That("abc DEF ghi".CheckDoesNotContain(substrings, StringComparison.OrdinalIgnoreCase), Is.False);
        }

        [Test]
        public void DoesNotContainValidation_SubstringDoesNotExist_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => StringDoesNotContainValidation.DoesNotContainValidation("hello world", "planet", "value"));
        }

        [Test]
        public void DoesNotContainValidation_SubstringExists_Throws()
        {
            Assert.Throws<ValidationException>(() => StringDoesNotContainValidation.DoesNotContainValidation("hello world", "world", "value"));
        }

        [Test]
        public void ValidateDoesNotContain_SubstringDoesNotExist_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => "hello world".ValidateDoesNotContain("planet", "value"));
        }

        [Test]
        public void ValidateDoesNotContain_SubstringExists_Throws()
        {
            Assert.Throws<ValidationException>(() => "hello world".ValidateDoesNotContain("world", "value"));
        }
    }
} 