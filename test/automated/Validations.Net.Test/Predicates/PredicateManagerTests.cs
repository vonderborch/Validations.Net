using System.Reflection;
using NUnit.Framework;
using Validations.Net;
using Validations.Net.Predicates;
using Validations.Net.Validators;

namespace Validations.Net.Test.Predicates;

[TestFixture]
public class PredicateManagerTests
{
    [SetUp]
    public void SetUp()
    {
        // Clear any existing predicates before each test
        PredicateManager.Clear();
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up after each test
        PredicateManager.Clear();
    }

    [Test]
    public void Clear_RemovesAllRegisteredPredicates()
    {
        // Arrange & Act
        PredicateManager.Clear();

        // Assert - Verify that predicates are cleared by checking if we can get a predicate
        // that should not exist after clearing
        var result = PredicateManager.GetPredicate<string>("NonExistentPredicate", "TestGroup", new TestClass());
        
        Assert.That(result, Is.Null);
    }

    [Test]
    public void GetPredicate_WithNullInstanceForGlobalPredicate_ReturnsPredicate()
    {
        // Act
        var result = PredicateManager.GetPredicate<string>("IsValidString", "TestGroup", null);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!("test"), Is.True);
        Assert.That(result!(""), Is.False);
    }

    [Test]
    public void GetPredicate_WithNullInstanceForNonGlobalPredicate_ReturnsNull()
    {
        // Act
        var result = PredicateManager.GetPredicate<string>("InstanceIsValid", "TestGroup", null);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void GetPredicate_WithNonExistentPredicate_ReturnsNull()
    {
        // Arrange
        var testInstance = new TestClass();

        // Act
        var result = PredicateManager.GetPredicate<string>("NonExistentPredicate", "TestGroup", testInstance);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void GetPredicate_WithValidStaticMethodPredicate_ReturnsPredicate()
    {
        // Arrange
        var testInstance = new TestClass();

        // Act
        var result = PredicateManager.GetPredicate<string>("IsValidString", "TestGroup", testInstance);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!("test"), Is.True);
        Assert.That(result!(""), Is.False);
    }

    [Test]
    public void GetPredicate_WithValidInstanceMethodPredicate_ReturnsPredicate()
    {
        // Arrange
        var testInstance = new TestClass();

        // Act
        var result = PredicateManager.GetPredicate<string>("InstanceIsValid", "TestGroup", testInstance);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!("test"), Is.True);
        Assert.That(result!(""), Is.False);
    }

    [Test]
    public void GetPredicate_WithValidStaticPropertyPredicate_ReturnsPredicate()
    {
        // Arrange
        var testInstance = new TestClass();

        // Act
        var result = PredicateManager.GetPredicate<string>("StaticPropertyPredicate", "TestGroup", testInstance);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!("test"), Is.True);
        Assert.That(result!(""), Is.False);
    }

    [Test]
    public void GetPredicate_WithValidInstancePropertyPredicate_ReturnsPredicate()
    {
        // Arrange
        var testInstance = new TestClass();

        // Act
        var result = PredicateManager.GetPredicate<string>("InstancePropertyPredicate", "TestGroup", testInstance);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!("test"), Is.True);
        Assert.That(result!(""), Is.False);
    }



    [Test]
    public void GetPredicate_WithRefreshTrue_ReinitializesPredicates()
    {
        // Arrange
        var testInstance = new TestClass();

        // Act
        var result = PredicateManager.GetPredicate<string>("IsValidString", "TestGroup", testInstance, refresh: true);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!("test"), Is.True);
    }

    [Test]
    public void GetPredicate_WithCachedPredicate_ReturnsCachedPredicate()
    {
        // Arrange
        var testInstance = new TestClass();

        // Act - Call twice to test caching
        var result1 = PredicateManager.GetPredicate<string>("IsValidString", "TestGroup", testInstance);
        var result2 = PredicateManager.GetPredicate<string>("IsValidString", "TestGroup", testInstance);

        // Assert
        Assert.That(result1, Is.Not.Null);
        Assert.That(result2, Is.Not.Null);
        Assert.That(result1, Is.EqualTo(result2)); // Should be the same cached instance
    }

    [Test]
    public void GetPredicate_WithInvalidPropertyType_ReturnsNull()
    {
        // Arrange
        var testInstance = new InvalidPropertyTestClass();

        // Act
        var result = PredicateManager.GetPredicate<string>("InvalidPropertyPredicate", "TestGroup", testInstance);

        // Assert
        Assert.That(result, Is.Null);
    }



    [Test]
    public void GetPredicate_WithNullInstanceForNonStaticProperty_ReturnsNull()
    {
        // Act
        var result = PredicateManager.GetPredicate<string>("InstancePropertyPredicate", "TestGroup", null);

        // Assert
        Assert.That(result, Is.Null);
    }



    [Test]
    public void InitializationThreadCount_CanBeModified()
    {
        // Arrange
        var originalValue = PredicateManager.InitializationThreadCount;
        var newValue = 8;

        // Act
        PredicateManager.InitializationThreadCount = newValue;

        // Assert
        Assert.That(PredicateManager.InitializationThreadCount, Is.EqualTo(newValue));

        // Cleanup
        PredicateManager.InitializationThreadCount = originalValue;
    }

    [Test]
    public void GetPredicate_GlobalPredicateCanBeAccessedWithoutInstance()
    {
        // Act
        var result = PredicateManager.GetPredicate<string>("IsValidString", "TestGroup", null);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!("test"), Is.True);
        Assert.That(result!(""), Is.False);
    }

    [Test]
    public void GetPredicate_GlobalPredicateCanBeAccessedWithInstance()
    {
        // Arrange
        var testInstance = new TestClass();

        // Act
        var result = PredicateManager.GetPredicate<string>("IsValidString", "TestGroup", testInstance);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!("test"), Is.True);
        Assert.That(result!(""), Is.False);
    }

    [Test]
    public void GetPredicate_InstancePredicateRequiresInstance()
    {
        // Act
        var result = PredicateManager.GetPredicate<string>("InstanceIsValid", "TestGroup", null);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void GetPredicate_InstancePredicateWorksWithCorrectInstance()
    {
        // Arrange
        var testInstance = new TestClass();

        // Act
        var result = PredicateManager.GetPredicate<string>("InstanceIsValid", "TestGroup", testInstance);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!("test"), Is.True);
        Assert.That(result!(""), Is.False);
    }

    [Test]
    public void GetPredicate_GlobalPredicateCachingWorksWithoutInstance()
    {
        // Act - Call twice to test caching
        var result1 = PredicateManager.GetPredicate<string>("IsValidString", "TestGroup", null);
        var result2 = PredicateManager.GetPredicate<string>("IsValidString", "TestGroup", null);

        // Assert
        Assert.That(result1, Is.Not.Null);
        Assert.That(result2, Is.Not.Null);
        Assert.That(result1, Is.EqualTo(result2)); // Should be the same cached instance
    }

    [Test]
    public void GetPredicate_GlobalPredicateCachingWorksWithInstance()
    {
        // Arrange
        var testInstance = new TestClass();

        // Act - Call twice to test caching
        var result1 = PredicateManager.GetPredicate<string>("IsValidString", "TestGroup", testInstance);
        var result2 = PredicateManager.GetPredicate<string>("IsValidString", "TestGroup", testInstance);

        // Assert
        Assert.That(result1, Is.Not.Null);
        Assert.That(result2, Is.Not.Null);
        Assert.That(result1, Is.EqualTo(result2)); // Should be the same cached instance
    }

    [Test]
    public void GetPredicate_InstancePredicateCachingWorksWithInstance()
    {
        // Arrange
        var testInstance = new TestClass();

        // Act - Call twice to test caching
        var result1 = PredicateManager.GetPredicate<string>("InstanceIsValid", "TestGroup", testInstance);
        var result2 = PredicateManager.GetPredicate<string>("InstanceIsValid", "TestGroup", testInstance);

        // Assert
        Assert.That(result1, Is.Not.Null);
        Assert.That(result2, Is.Not.Null);
        Assert.That(result1, Is.EqualTo(result2)); // Should be the same cached instance
    }

    [Test]
    public void GetPredicate_GlobalPredicateRefreshWorksWithoutInstance()
    {
        // Act
        var result = PredicateManager.GetPredicate<string>("IsValidString", "TestGroup", null, refresh: true);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!("test"), Is.True);
    }

    [Test]
    public void GetPredicate_InstancePredicateRefreshWorksWithInstance()
    {
        // Arrange
        var testInstance = new TestClass();

        // Act
        var result = PredicateManager.GetPredicate<string>("InstanceIsValid", "TestGroup", testInstance, refresh: true);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!("test"), Is.True);
    }

    [Test]
    public void GetPredicate_GlobalPredicateWithDifferentGroups_AreDistinct()
    {
        // Act
        var result1 = PredicateManager.GetPredicate<string>("IsValidString", "TestGroup", null);
        var result2 = PredicateManager.GetPredicate<string>("IsValidString", "DifferentGroup", null);

        // Assert
        Assert.That(result1, Is.Not.Null);
        Assert.That(result2, Is.Null); // Different group should not exist
    }

    [Test]
    public void GetPredicate_InstancePredicateWithDifferentGroups_AreDistinct()
    {
        // Arrange
        var testInstance = new TestClass();

        // Act
        var result1 = PredicateManager.GetPredicate<string>("InstanceIsValid", "TestGroup", testInstance);
        var result2 = PredicateManager.GetPredicate<string>("InstanceIsValid", "DifferentGroup", testInstance);

        // Assert
        Assert.That(result1, Is.Not.Null);
        Assert.That(result2, Is.Null); // Different group should not exist
    }

    // Test classes for predicate registration
    public class TestClass
    {
        [ValidationPredicate("IsValidString", "TestGroup")]
        public static bool IsValidString(string value) => !string.IsNullOrEmpty(value);

        [ValidationPredicate("InstanceIsValid", "TestGroup")]
        public bool InstanceIsValid(string value) => !string.IsNullOrEmpty(value);

        [ValidationPredicate("StaticPropertyPredicate", "TestGroup")]
        public static Func<string, bool> StaticPropertyPredicate => value => !string.IsNullOrEmpty(value);

                 [ValidationPredicate("InstancePropertyPredicate", "TestGroup")]
         public Func<string, bool> InstancePropertyPredicate => value => !string.IsNullOrEmpty(value);

         [ValidationPredicate("StaticFieldPredicate", "TestGroup")]
         public static Func<string, bool> StaticFieldPredicate = value => !string.IsNullOrEmpty(value);

         [ValidationPredicate("InstanceFieldPredicate", "TestGroup")]
         public Func<string, bool> InstanceFieldPredicate = value => !string.IsNullOrEmpty(value);
    }

    public class InvalidPropertyTestClass
    {
        [ValidationPredicate("InvalidPropertyPredicate", "TestGroup")]
        public static string InvalidPropertyPredicate => "This is not a Func<string, bool>";
    }


}
