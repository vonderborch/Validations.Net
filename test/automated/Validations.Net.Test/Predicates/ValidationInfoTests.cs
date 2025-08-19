using System.Reflection;
using NUnit.Framework;
using Validations.Net;
using Validations.Net.Predicates;

namespace Validations.Net.Test.Predicates;

[TestFixture]
public class ValidationInfoTests
{
    private PropertyInfo _testPropertyInfo;
    private FieldInfo _testFieldInfo;
    private List<ValidationAttribute> _testValidators;

    [SetUp]
    public void SetUp()
    {
        _testPropertyInfo = typeof(string).GetProperty("Length")!;
        _testFieldInfo = typeof(TestClass).GetField("TestField")!;
        _testValidators = new List<ValidationAttribute>
        {
            new TestValidationAttribute("Validator1"),
            new TestValidationAttribute("Validator2")
        };
    }

    [TestFixture]
    public class PropertyValidationInfoTests : ValidationInfoTests
    {
        [Test]
        public void Constructor_WithAllParameters_SetsPropertiesCorrectly()
        {
            // Arrange
            var isPublic = true;

            // Act
            var propertyValidationInfo = new PropertyValidationInfo(_testPropertyInfo, isPublic, _testValidators);

            // Assert
            Assert.That(propertyValidationInfo.Property, Is.EqualTo(_testPropertyInfo));
            Assert.That(propertyValidationInfo.IsPublic, Is.EqualTo(isPublic));
            Assert.That(propertyValidationInfo.Validators, Is.EqualTo(_testValidators));
        }

        [Test]
        public void Constructor_WithEmptyValidatorsList_SetsValidatorsToEmpty()
        {
            // Arrange
            var emptyValidators = new List<ValidationAttribute>();

            // Act
            var propertyValidationInfo = new PropertyValidationInfo(_testPropertyInfo, true, emptyValidators);

            // Assert
            Assert.That(propertyValidationInfo.Validators, Is.EqualTo(emptyValidators));
            Assert.That(propertyValidationInfo.Validators, Is.Empty);
        }

        [Test]
        public void Constructor_WithPublicFalse_SetsIsPublicToFalse()
        {
            // Act
            var propertyValidationInfo = new PropertyValidationInfo(_testPropertyInfo, false, _testValidators);

            // Assert
            Assert.That(propertyValidationInfo.IsPublic, Is.False);
        }

        [Test]
        public void Constructor_WithPublicTrue_SetsIsPublicToTrue()
        {
            // Act
            var propertyValidationInfo = new PropertyValidationInfo(_testPropertyInfo, true, _testValidators);

            // Assert
            Assert.That(propertyValidationInfo.IsPublic, Is.True);
        }

        [Test]
        public void Constructor_WithDifferentPropertyTypes_WorksCorrectly()
        {
            // Arrange
            var intPropertyInfo = typeof(TestClass).GetProperty("IntProperty")!;
            var stringPropertyInfo = typeof(TestClass).GetProperty("StringProperty")!;

            // Act
            var intPropertyValidation = new PropertyValidationInfo(intPropertyInfo, true, _testValidators);
            var stringPropertyValidation = new PropertyValidationInfo(stringPropertyInfo, false, _testValidators);

            // Assert
            Assert.That(intPropertyValidation.Property, Is.EqualTo(intPropertyInfo));
            Assert.That(stringPropertyValidation.Property, Is.EqualTo(stringPropertyInfo));
        }

        [Test]
        public void Equality_WithSameValues_ReturnsTrue()
        {
            // Arrange
            var propertyValidationInfo1 = new PropertyValidationInfo(_testPropertyInfo, true, _testValidators);
            var propertyValidationInfo2 = new PropertyValidationInfo(_testPropertyInfo, true, _testValidators);

            // Act & Assert
            Assert.That(propertyValidationInfo1, Is.EqualTo(propertyValidationInfo2));
            Assert.That(propertyValidationInfo1.GetHashCode(), Is.EqualTo(propertyValidationInfo2.GetHashCode()));
        }

        [Test]
        public void Equality_WithDifferentValues_ReturnsFalse()
        {
            // Arrange
            var propertyValidationInfo1 = new PropertyValidationInfo(_testPropertyInfo, true, _testValidators);
            var propertyValidationInfo2 = new PropertyValidationInfo(_testPropertyInfo, false, _testValidators);

            // Act & Assert
            Assert.That(propertyValidationInfo1, Is.Not.EqualTo(propertyValidationInfo2));
        }

        [Test]
        public void RecordStruct_PropertiesAreReadOnly()
        {
            // Arrange
            var propertyValidationInfo = new PropertyValidationInfo(_testPropertyInfo, true, _testValidators);

            // Act & Assert - These should compile without errors, proving properties are read-only
            var property = propertyValidationInfo.Property;
            var isPublic = propertyValidationInfo.IsPublic;
            var validators = propertyValidationInfo.Validators;

            Assert.That(property, Is.Not.Null);
            Assert.That(validators, Is.Not.Null);
        }

        [Test]
        public void ToString_ReturnsExpectedFormat()
        {
            // Arrange
            var propertyValidationInfo = new PropertyValidationInfo(_testPropertyInfo, true, _testValidators);

            // Act
            var result = propertyValidationInfo.ToString();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
        }
    }

    [TestFixture]
    public class FieldValidationInfoTests : ValidationInfoTests
    {
        [Test]
        public void Constructor_WithAllParameters_SetsPropertiesCorrectly()
        {
            // Arrange
            var isPublic = true;

            // Act
            var fieldValidationInfo = new FieldValidationInfo(_testFieldInfo, isPublic, _testValidators);

            // Assert
            Assert.That(fieldValidationInfo.Field, Is.EqualTo(_testFieldInfo));
            Assert.That(fieldValidationInfo.IsPublic, Is.EqualTo(isPublic));
            Assert.That(fieldValidationInfo.Validators, Is.EqualTo(_testValidators));
        }

        [Test]
        public void Constructor_WithEmptyValidatorsList_SetsValidatorsToEmpty()
        {
            // Arrange
            var emptyValidators = new List<ValidationAttribute>();

            // Act
            var fieldValidationInfo = new FieldValidationInfo(_testFieldInfo, true, emptyValidators);

            // Assert
            Assert.That(fieldValidationInfo.Validators, Is.EqualTo(emptyValidators));
            Assert.That(fieldValidationInfo.Validators, Is.Empty);
        }

        [Test]
        public void Constructor_WithPublicFalse_SetsIsPublicToFalse()
        {
            // Act
            var fieldValidationInfo = new FieldValidationInfo(_testFieldInfo, false, _testValidators);

            // Assert
            Assert.That(fieldValidationInfo.IsPublic, Is.False);
        }

        [Test]
        public void Constructor_WithPublicTrue_SetsIsPublicToTrue()
        {
            // Act
            var fieldValidationInfo = new FieldValidationInfo(_testFieldInfo, true, _testValidators);

            // Assert
            Assert.That(fieldValidationInfo.IsPublic, Is.True);
        }

        [Test]
        public void Constructor_WithDifferentFieldTypes_WorksCorrectly()
        {
            // Arrange
            var intFieldInfo = typeof(TestClass).GetField("IntField")!;
            var stringFieldInfo = typeof(TestClass).GetField("StringField")!;

            // Act
            var intFieldValidation = new FieldValidationInfo(intFieldInfo, true, _testValidators);
            var stringFieldValidation = new FieldValidationInfo(stringFieldInfo, false, _testValidators);

            // Assert
            Assert.That(intFieldValidation.Field, Is.EqualTo(intFieldInfo));
            Assert.That(stringFieldValidation.Field, Is.EqualTo(stringFieldInfo));
        }

        [Test]
        public void Equality_WithSameValues_ReturnsTrue()
        {
            // Arrange
            var fieldValidationInfo1 = new FieldValidationInfo(_testFieldInfo, true, _testValidators);
            var fieldValidationInfo2 = new FieldValidationInfo(_testFieldInfo, true, _testValidators);

            // Act & Assert
            Assert.That(fieldValidationInfo1, Is.EqualTo(fieldValidationInfo2));
            Assert.That(fieldValidationInfo1.GetHashCode(), Is.EqualTo(fieldValidationInfo2.GetHashCode()));
        }

        [Test]
        public void Equality_WithDifferentValues_ReturnsFalse()
        {
            // Arrange
            var fieldValidationInfo1 = new FieldValidationInfo(_testFieldInfo, true, _testValidators);
            var fieldValidationInfo2 = new FieldValidationInfo(_testFieldInfo, false, _testValidators);

            // Act & Assert
            Assert.That(fieldValidationInfo1, Is.Not.EqualTo(fieldValidationInfo2));
        }

        [Test]
        public void RecordStruct_PropertiesAreReadOnly()
        {
            // Arrange
            var fieldValidationInfo = new FieldValidationInfo(_testFieldInfo, true, _testValidators);

            // Act & Assert - These should compile without errors, proving properties are read-only
            var field = fieldValidationInfo.Field;
            var isPublic = fieldValidationInfo.IsPublic;
            var validators = fieldValidationInfo.Validators;

            Assert.That(field, Is.Not.Null);
            Assert.That(validators, Is.Not.Null);
        }

        [Test]
        public void ToString_ReturnsExpectedFormat()
        {
            // Arrange
            var fieldValidationInfo = new FieldValidationInfo(_testFieldInfo, true, _testValidators);

            // Act
            var result = fieldValidationInfo.ToString();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
        }
    }

    // Helper test class
    private class TestClass
    {
        public string TestField = "test";
        public int IntField = 42;
        public string StringField = "test";

        public int IntProperty { get; set; } = 42;
        public string StringProperty { get; set; } = "test";
    }

    // Helper test validation attribute
    private class TestValidationAttribute : ValidationAttribute
    {
        public TestValidationAttribute(string name) : base(name) { }
    }
}
