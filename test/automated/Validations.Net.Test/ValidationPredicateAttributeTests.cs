using NUnit.Framework;
using Validations.Net;

namespace Validations.Net.Test.Predicates;

[TestFixture]
public class ValidationPredicateAttributeTests
{
    [Test]
    public void Constructor_WithNameOnly_SetsNameAndGroupToNull()
    {
        // Arrange
        var name = "TestPredicate";

        // Act
        var attribute = new ValidationPredicateAttribute(name);

        // Assert
        Assert.That(attribute.Name, Is.EqualTo(name));
        Assert.That(attribute.Group, Is.Null);
    }

    [Test]
    public void Constructor_WithNameAndGroup_SetsNameAndGroup()
    {
        // Arrange
        var name = "TestPredicate";
        var group = "TestGroup";

        // Act
        var attribute = new ValidationPredicateAttribute(name, group);

        // Assert
        Assert.That(attribute.Name, Is.EqualTo(name));
        Assert.That(attribute.Group, Is.EqualTo(group));
    }

    [Test]
    public void Constructor_WithNameAndNullGroup_SetsNameAndGroupToNull()
    {
        // Arrange
        var name = "TestPredicate";
        string? group = null;

        // Act
        var attribute = new ValidationPredicateAttribute(name, group);

        // Assert
        Assert.That(attribute.Name, Is.EqualTo(name));
        Assert.That(attribute.Group, Is.Null);
    }

    [Test]
    public void Constructor_WithEmptyName_SetsEmptyName()
    {
        // Arrange
        var name = "";

        // Act
        var attribute = new ValidationPredicateAttribute(name);

        // Assert
        Assert.That(attribute.Name, Is.EqualTo(name));
        Assert.That(attribute.Group, Is.Null);
    }

    [Test]
    public void Constructor_WithWhitespaceName_SetsWhitespaceName()
    {
        // Arrange
        var name = "   ";

        // Act
        var attribute = new ValidationPredicateAttribute(name);

        // Assert
        Assert.That(attribute.Name, Is.EqualTo(name));
        Assert.That(attribute.Group, Is.Null);
    }

    [Test]
    public void Constructor_WithEmptyGroup_SetsEmptyGroup()
    {
        // Arrange
        var name = "TestPredicate";
        var group = "";

        // Act
        var attribute = new ValidationPredicateAttribute(name, group);

        // Assert
        Assert.That(attribute.Name, Is.EqualTo(name));
        Assert.That(attribute.Group, Is.EqualTo(group));
    }

    [Test]
    public void Constructor_WithWhitespaceGroup_SetsWhitespaceGroup()
    {
        // Arrange
        var name = "TestPredicate";
        var group = "   ";

        // Act
        var attribute = new ValidationPredicateAttribute(name, group);

        // Assert
        Assert.That(attribute.Name, Is.EqualTo(name));
        Assert.That(attribute.Group, Is.EqualTo(group));
    }

    [Test]
    public void Constructor_WithSpecialCharactersInName_SetsNameWithSpecialCharacters()
    {
        // Arrange
        var name = "Test-Predicate_123";

        // Act
        var attribute = new ValidationPredicateAttribute(name);

        // Assert
        Assert.That(attribute.Name, Is.EqualTo(name));
        Assert.That(attribute.Group, Is.Null);
    }

    [Test]
    public void Constructor_WithSpecialCharactersInGroup_SetsGroupWithSpecialCharacters()
    {
        // Arrange
        var name = "TestPredicate";
        var group = "Test-Group_123";

        // Act
        var attribute = new ValidationPredicateAttribute(name, group);

        // Assert
        Assert.That(attribute.Name, Is.EqualTo(name));
        Assert.That(attribute.Group, Is.EqualTo(group));
    }

    [Test]
    public void Constructor_WithUnicodeCharacters_SetsUnicodeCharacters()
    {
        // Arrange
        var name = "TestPredicate\u00E9";
        var group = "TestGroup\u00E9";

        // Act
        var attribute = new ValidationPredicateAttribute(name, group);

        // Assert
        Assert.That(attribute.Name, Is.EqualTo(name));
        Assert.That(attribute.Group, Is.EqualTo(group));
    }

    [Test]
    public void Constructor_WithVeryLongName_SetsVeryLongName()
    {
        // Arrange
        var name = new string('a', 1000);

        // Act
        var attribute = new ValidationPredicateAttribute(name);

        // Assert
        Assert.That(attribute.Name, Is.EqualTo(name));
        Assert.That(attribute.Group, Is.Null);
    }

    [Test]
    public void Constructor_WithVeryLongGroup_SetsVeryLongGroup()
    {
        // Arrange
        var name = "TestPredicate";
        var group = new string('b', 1000);

        // Act
        var attribute = new ValidationPredicateAttribute(name, group);

        // Assert
        Assert.That(attribute.Name, Is.EqualTo(name));
        Assert.That(attribute.Group, Is.EqualTo(group));
    }

    [Test]
    public void Name_Property_IsReadOnly()
    {
        // Arrange
        var attribute = new ValidationPredicateAttribute("TestPredicate");

        // Act & Assert
        Assert.That(attribute.Name, Is.EqualTo("TestPredicate"));
        
        // Verify it's read-only by checking that we can't modify it through reflection
        var propertyInfo = typeof(ValidationPredicateAttribute).GetProperty("Name");
        Assert.That(propertyInfo?.CanWrite, Is.False);
    }

    [Test]
    public void Group_Property_IsReadOnly()
    {
        // Arrange
        var attribute = new ValidationPredicateAttribute("TestPredicate", "TestGroup");

        // Act & Assert
        Assert.That(attribute.Group, Is.EqualTo("TestGroup"));
        
        // Verify it's read-only by checking that we can't modify it through reflection
        var propertyInfo = typeof(ValidationPredicateAttribute).GetProperty("Group");
        Assert.That(propertyInfo?.CanWrite, Is.False);
    }

    [Test]
    public void AttributeUsage_AllowsMethods()
    {
        // Arrange & Act
        var attributeUsage = typeof(ValidationPredicateAttribute).GetCustomAttributes(typeof(AttributeUsageAttribute), false)
            .Cast<AttributeUsageAttribute>()
            .FirstOrDefault();

        // Assert
        Assert.That(attributeUsage, Is.Not.Null);
        Assert.That(attributeUsage.ValidOn & AttributeTargets.Method, Is.EqualTo(AttributeTargets.Method));
    }

    [Test]
    public void AttributeUsage_AllowsDelegates()
    {
        // Arrange & Act
        var attributeUsage = typeof(ValidationPredicateAttribute).GetCustomAttributes(typeof(AttributeUsageAttribute), false)
            .Cast<AttributeUsageAttribute>()
            .FirstOrDefault();

        // Assert
        Assert.That(attributeUsage, Is.Not.Null);
        Assert.That(attributeUsage.ValidOn & AttributeTargets.Delegate, Is.EqualTo(AttributeTargets.Delegate));
    }

    [Test]
    public void AttributeUsage_AllowsProperties()
    {
        // Arrange & Act
        var attributeUsage = typeof(ValidationPredicateAttribute).GetCustomAttributes(typeof(AttributeUsageAttribute), false)
            .Cast<AttributeUsageAttribute>()
            .FirstOrDefault();

        // Assert
        Assert.That(attributeUsage, Is.Not.Null);
        Assert.That(attributeUsage.ValidOn & AttributeTargets.Property, Is.EqualTo(AttributeTargets.Property));
    }

    [Test]
    public void AttributeUsage_DoesNotAllowMultiple()
    {
        // Arrange & Act
        var attributeUsage = typeof(ValidationPredicateAttribute).GetCustomAttributes(typeof(AttributeUsageAttribute), false)
            .Cast<AttributeUsageAttribute>()
            .FirstOrDefault();

        // Assert
        Assert.That(attributeUsage, Is.Not.Null);
        Assert.That(attributeUsage.AllowMultiple, Is.False);
    }

    [Test]
    public void AttributeUsage_IsInherited()
    {
        // Arrange & Act
        var attributeUsage = typeof(ValidationPredicateAttribute).GetCustomAttributes(typeof(AttributeUsageAttribute), false)
            .Cast<AttributeUsageAttribute>()
            .FirstOrDefault();

        // Assert
        Assert.That(attributeUsage, Is.Not.Null);
        Assert.That(attributeUsage.Inherited, Is.True);
    }
}
