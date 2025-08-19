using NUnit.Framework;
using Validations.Net.Predicates;

namespace Validations.Net.Test.Predicates;

[TestFixture]
public class PredicateTypeTests
{
    [Test]
    public void PredicateType_EnumValues_AreCorrectlyDefined()
    {
        // Arrange & Act
        var enumValues = Enum.GetValues<PredicateType>();

        // Assert
        Assert.That(enumValues, Contains.Item(PredicateType.Property));
        Assert.That(enumValues, Contains.Item(PredicateType.StaticProperty));
        Assert.That(enumValues, Contains.Item(PredicateType.Field));
        Assert.That(enumValues, Contains.Item(PredicateType.StaticField));
        Assert.That(enumValues, Contains.Item(PredicateType.Method));
        Assert.That(enumValues, Contains.Item(PredicateType.StaticMethod));
        Assert.That(enumValues.Length, Is.EqualTo(6));
    }

    [Test]
    public void PredicateType_EnumNames_AreCorrectlyDefined()
    {
        // Arrange & Act
        var enumNames = Enum.GetNames<PredicateType>();

        // Assert
        Assert.That(enumNames, Contains.Item("Property"));
        Assert.That(enumNames, Contains.Item("StaticProperty"));
        Assert.That(enumNames, Contains.Item("Field"));
        Assert.That(enumNames, Contains.Item("StaticField"));
        Assert.That(enumNames, Contains.Item("Method"));
        Assert.That(enumNames, Contains.Item("StaticMethod"));
        Assert.That(enumNames.Length, Is.EqualTo(6));
    }

    [TestCase(PredicateType.Property, "Property")]
    [TestCase(PredicateType.StaticProperty, "StaticProperty")]
    [TestCase(PredicateType.Field, "Field")]
    [TestCase(PredicateType.StaticField, "StaticField")]
    [TestCase(PredicateType.Method, "Method")]
    [TestCase(PredicateType.StaticMethod, "StaticMethod")]
    public void PredicateType_ToString_ReturnsCorrectName(PredicateType predicateType, string expectedName)
    {
        // Act
        var result = predicateType.ToString();

        // Assert
        Assert.That(result, Is.EqualTo(expectedName));
    }

    [TestCase("Property", PredicateType.Property)]
    [TestCase("StaticProperty", PredicateType.StaticProperty)]
    [TestCase("Field", PredicateType.Field)]
    [TestCase("StaticField", PredicateType.StaticField)]
    [TestCase("Method", PredicateType.Method)]
    [TestCase("StaticMethod", PredicateType.StaticMethod)]
    public void PredicateType_Parse_ReturnsCorrectEnum(string name, PredicateType expectedType)
    {
        // Act
        var result = Enum.Parse<PredicateType>(name);

        // Assert
        Assert.That(result, Is.EqualTo(expectedType));
    }

    [Test]
    public void PredicateType_IsDefined_ReturnsTrueForValidValues()
    {
        // Arrange
        var validValues = new[]
        {
            PredicateType.Property,
            PredicateType.StaticProperty,
            PredicateType.Field,
            PredicateType.StaticField,
            PredicateType.Method,
            PredicateType.StaticMethod
        };

        // Act & Assert
        foreach (var value in validValues)
        {
            Assert.That(Enum.IsDefined(typeof(PredicateType), value), Is.True, $"{value} should be defined");
        }
    }

    [Test]
    public void PredicateType_IsDefined_ReturnsFalseForInvalidValues()
    {
        // Arrange
        var invalidValues = new[] { (PredicateType)(-1), (PredicateType)99, (PredicateType)100 };

        // Act & Assert
        foreach (var value in invalidValues)
        {
            Assert.That(Enum.IsDefined(typeof(PredicateType), value), Is.False, $"{value} should not be defined");
        }
    }

    [Test]
    public void PredicateType_EnumValues_HaveSequentialIntegerValues()
    {
        // Act & Assert
        Assert.That((int)PredicateType.Property, Is.EqualTo(0));
        Assert.That((int)PredicateType.StaticProperty, Is.EqualTo(1));
        Assert.That((int)PredicateType.Field, Is.EqualTo(2));
        Assert.That((int)PredicateType.StaticField, Is.EqualTo(3));
        Assert.That((int)PredicateType.Method, Is.EqualTo(4));
        Assert.That((int)PredicateType.StaticMethod, Is.EqualTo(5));
    }

    [Test]
    public void PredicateType_GetValues_ReturnsAllEnumValues()
    {
        // Act
        var values = Enum.GetValues<PredicateType>();

        // Assert
        Assert.That(values, Is.Not.Null);
        Assert.That(values.Length, Is.EqualTo(6));
        Assert.That(values, Is.Unique);
    }

    [Test]
    public void PredicateType_Comparison_WorksCorrectly()
    {
        // Act & Assert
        Assert.That(PredicateType.Property, Is.LessThan(PredicateType.StaticProperty));
        Assert.That(PredicateType.Field, Is.GreaterThan(PredicateType.StaticProperty));
        Assert.That(PredicateType.Method.CompareTo(PredicateType.Method), Is.EqualTo(0));
        Assert.That(PredicateType.StaticMethod, Is.GreaterThan(PredicateType.Method));
    }
}
