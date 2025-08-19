using System.Reflection;
using NUnit.Framework;
using Validations.Net;
using Validations.Net.Predicates;

namespace Validations.Net.Test.Predicates;

[TestFixture]
public class TypePredicatesTests
{
    [Test]
    public void Constructor_WithValidType_CreatesTypePredicatesInfo()
    {
        // Arrange
        var type = typeof(TestClassWithPredicates);

        // Act
        var typePredicates = new TypePredicatesInfo(type);

        // Assert
        Assert.That(typePredicates.Type, Is.EqualTo(type));
        Assert.That(typePredicates.Predicates, Is.Not.Null);
        Assert.That(typePredicates.Predicates.Count, Is.GreaterThan(0));
    }

    [Test]
    public void Constructor_WithTypeWithoutPredicates_ReturnsEmptyPredicatesList()
    {
        // Arrange
        var type = typeof(TestClassWithoutPredicates);

        // Act
        var typePredicates = new TypePredicatesInfo(type);

        // Assert
        Assert.That(typePredicates.Type, Is.EqualTo(type));
        Assert.That(typePredicates.Predicates, Is.Not.Null);
        Assert.That(typePredicates.Predicates.Count, Is.EqualTo(0));
    }

    [Test]
    public void GetPredicates_WithValidStaticMethod_IncludesStaticMethodPredicate()
    {
        // Arrange
        var type = typeof(TestClassWithPredicates);

        // Act
        var typePredicates = new TypePredicatesInfo(type);

        // Assert
        var staticMethodPredicate = typePredicates.Predicates.FirstOrDefault(p => p.Name == "StaticMethodPredicate");
        Assert.That(staticMethodPredicate.Name, Is.EqualTo("StaticMethodPredicate"));
        Assert.That(staticMethodPredicate.Type, Is.EqualTo(PredicateType.StaticMethod));
        Assert.That(staticMethodPredicate.IsPublic, Is.True);
        Assert.That(staticMethodPredicate.Group, Is.EqualTo("TestGroup"));
    }

    [Test]
    public void GetPredicates_WithValidInstanceMethod_IncludesInstanceMethodPredicate()
    {
        // Arrange
        var type = typeof(TestClassWithPredicates);

        // Act
        var typePredicates = new TypePredicatesInfo(type);

        // Assert
        var instanceMethodPredicate = typePredicates.Predicates.FirstOrDefault(p => p.Name == "InstanceMethodPredicate");
        Assert.That(instanceMethodPredicate.Name, Is.EqualTo("InstanceMethodPredicate"));
        Assert.That(instanceMethodPredicate.Type, Is.EqualTo(PredicateType.Method));
        Assert.That(instanceMethodPredicate.IsPublic, Is.True);
        Assert.That(instanceMethodPredicate.Group, Is.EqualTo("TestGroup"));
    }

    [Test]
    public void GetPredicates_WithValidStaticProperty_IncludesStaticPropertyPredicate()
    {
        // Arrange
        var type = typeof(TestClassWithPredicates);

        // Act
        var typePredicates = new TypePredicatesInfo(type);

        // Assert
        var staticPropertyPredicate = typePredicates.Predicates.FirstOrDefault(p => p.Name == "StaticPropertyPredicate");
        Assert.That(staticPropertyPredicate.Name, Is.EqualTo("StaticPropertyPredicate"));
        Assert.That(staticPropertyPredicate.Type, Is.EqualTo(PredicateType.StaticProperty));
        Assert.That(staticPropertyPredicate.IsPublic, Is.True);
        Assert.That(staticPropertyPredicate.Group, Is.EqualTo("TestGroup"));
    }

    [Test]
    public void GetPredicates_WithValidInstanceProperty_IncludesInstancePropertyPredicate()
    {
        // Arrange
        var type = typeof(TestClassWithPredicates);

        // Act
        var typePredicates = new TypePredicatesInfo(type);

        // Assert
        var instancePropertyPredicate = typePredicates.Predicates.FirstOrDefault(p => p.Name == "InstancePropertyPredicate");
        Assert.That(instancePropertyPredicate.Name, Is.EqualTo("InstancePropertyPredicate"));
        Assert.That(instancePropertyPredicate.Type, Is.EqualTo(PredicateType.Property));
        Assert.That(instancePropertyPredicate.IsPublic, Is.True);
        Assert.That(instancePropertyPredicate.Group, Is.EqualTo("TestGroup"));
    }

    [Test]
    public void GetPredicates_WithValidStaticField_IncludesStaticFieldPredicate()
    {
        // Arrange
        var type = typeof(TestClassWithPredicates);

        // Act
        var typePredicates = new TypePredicatesInfo(type);

        // Assert
        var staticFieldPredicate = typePredicates.Predicates.FirstOrDefault(p => p.Name == "StaticFieldPredicate");
        Assert.That(staticFieldPredicate.Name, Is.EqualTo("StaticFieldPredicate"));
        Assert.That(staticFieldPredicate.Type, Is.EqualTo(PredicateType.StaticField));
        Assert.That(staticFieldPredicate.IsPublic, Is.True);
        Assert.That(staticFieldPredicate.Group, Is.EqualTo("TestGroup"));
    }

    [Test]
    public void GetPredicates_WithValidInstanceField_IncludesInstanceFieldPredicate()
    {
        // Arrange
        var type = typeof(TestClassWithPredicates);

        // Act
        var typePredicates = new TypePredicatesInfo(type);

        // Assert
        var instanceFieldPredicate = typePredicates.Predicates.FirstOrDefault(p => p.Name == "InstanceFieldPredicate");
        Assert.That(instanceFieldPredicate.Name, Is.EqualTo("InstanceFieldPredicate"));
        Assert.That(instanceFieldPredicate.Type, Is.EqualTo(PredicateType.Field));
        Assert.That(instanceFieldPredicate.IsPublic, Is.True);
        Assert.That(instanceFieldPredicate.Group, Is.EqualTo("TestGroup"));
    }



    [Test]
    public void GetPredicates_WithPrivatePredicate_IncludesPrivatePredicate()
    {
        // Arrange
        var type = typeof(TestClassWithPrivatePredicates);

        // Act
        var typePredicates = new TypePredicatesInfo(type);

        // Assert
        var privatePredicate = typePredicates.Predicates.FirstOrDefault(p => p.Name == "PrivateMethodPredicate");
        Assert.That(privatePredicate.Name, Is.EqualTo("PrivateMethodPredicate"));
        Assert.That(privatePredicate.IsPublic, Is.False);
    }

    [Test]
    public void GetPredicates_WithNullGroup_IncludesPredicateWithNullGroup()
    {
        // Arrange
        var type = typeof(TestClassWithNullGroup);

        // Act
        var typePredicates = new TypePredicatesInfo(type);

        // Assert
        var nullGroupPredicate = typePredicates.Predicates.FirstOrDefault(p => p.Name == "NullGroupPredicate");
        Assert.That(nullGroupPredicate.Name, Is.EqualTo("NullGroupPredicate"));
        Assert.That(nullGroupPredicate.Group, Is.Null);
    }

    [Test]
    public void GetPredicates_WithInvalidMethodSignature_ExcludesInvalidMethod()
    {
        // Arrange
        var type = typeof(TestClassWithInvalidPredicates);

        // Act
        var typePredicates = new TypePredicatesInfo(type);

        // Assert
        var invalidMethodPredicate = typePredicates.Predicates.FirstOrDefault(p => p.Name == "InvalidMethodPredicate");
        Assert.That(invalidMethodPredicate.Name, Is.EqualTo(default(string)));
    }

    [Test]
    public void GetPredicates_WithInvalidPropertyType_ExcludesInvalidProperty()
    {
        // Arrange
        var type = typeof(TestClassWithInvalidPredicates);

        // Act
        var typePredicates = new TypePredicatesInfo(type);

        // Assert
        var invalidPropertyPredicate = typePredicates.Predicates.FirstOrDefault(p => p.Name == "InvalidPropertyPredicate");
        Assert.That(invalidPropertyPredicate.Name, Is.EqualTo(default(string)));
    }



    [Test]
    public void GetPredicates_WithPropertyWithoutPublicGetter_ExcludesProperty()
    {
        // Arrange
        var type = typeof(TestClassWithInvalidPredicates);

        // Act
        var typePredicates = new TypePredicatesInfo(type);

        // Assert
        var privateGetterProperty = typePredicates.Predicates.FirstOrDefault(p => p.Name == "PrivateGetterProperty");
        Assert.That(privateGetterProperty.Name, Is.EqualTo(default(string)));
    }

    [Test]
    public void GetPredicates_StaticMethodPredicate_IsGlobalPredicate()
    {
        // Arrange
        var type = typeof(TestClassWithPredicates);

        // Act
        var typePredicates = new TypePredicatesInfo(type);

        // Assert
        var staticMethodPredicate = typePredicates.Predicates.FirstOrDefault(p => p.Name == "StaticMethodPredicate");
        Assert.That(staticMethodPredicate.IsGlobalPredicate, Is.True);
    }

    [Test]
    public void GetPredicates_InstanceMethodPredicate_IsNotGlobalPredicate()
    {
        // Arrange
        var type = typeof(TestClassWithPredicates);

        // Act
        var typePredicates = new TypePredicatesInfo(type);

        // Assert
        var instanceMethodPredicate = typePredicates.Predicates.FirstOrDefault(p => p.Name == "InstanceMethodPredicate");
        Assert.That(instanceMethodPredicate.IsGlobalPredicate, Is.False);
    }

    [Test]
    public void GetPredicates_StaticPropertyPredicate_IsGlobalPredicate()
    {
        // Arrange
        var type = typeof(TestClassWithPredicates);

        // Act
        var typePredicates = new TypePredicatesInfo(type);

        // Assert
        var staticPropertyPredicate = typePredicates.Predicates.FirstOrDefault(p => p.Name == "StaticPropertyPredicate");
        Assert.That(staticPropertyPredicate.IsGlobalPredicate, Is.True);
    }

    [Test]
    public void GetPredicates_InstancePropertyPredicate_IsNotGlobalPredicate()
    {
        // Arrange
        var type = typeof(TestClassWithPredicates);

        // Act
        var typePredicates = new TypePredicatesInfo(type);

        // Assert
        var instancePropertyPredicate = typePredicates.Predicates.FirstOrDefault(p => p.Name == "InstancePropertyPredicate");
        Assert.That(instancePropertyPredicate.IsGlobalPredicate, Is.False);
    }

    [Test]
    public void GetPredicates_StaticFieldPredicate_IsGlobalPredicate()
    {
        // Arrange
        var type = typeof(TestClassWithPredicates);

        // Act
        var typePredicates = new TypePredicatesInfo(type);

        // Assert
        var staticFieldPredicate = typePredicates.Predicates.FirstOrDefault(p => p.Name == "StaticFieldPredicate");
        Assert.That(staticFieldPredicate.IsGlobalPredicate, Is.True);
    }

    [Test]
    public void GetPredicates_InstanceFieldPredicate_IsNotGlobalPredicate()
    {
        // Arrange
        var type = typeof(TestClassWithPredicates);

        // Act
        var typePredicates = new TypePredicatesInfo(type);

        // Assert
        var instanceFieldPredicate = typePredicates.Predicates.FirstOrDefault(p => p.Name == "InstanceFieldPredicate");
        Assert.That(instanceFieldPredicate.IsGlobalPredicate, Is.False);
    }



    // Test classes for predicate validation
    public class TestClassWithPredicates
    {
        [ValidationPredicate("StaticMethodPredicate", "TestGroup")]
        public static bool StaticMethodPredicate(string value) => !string.IsNullOrEmpty(value);

        [ValidationPredicate("InstanceMethodPredicate", "TestGroup")]
        public bool InstanceMethodPredicate(string value) => !string.IsNullOrEmpty(value);

        [ValidationPredicate("StaticPropertyPredicate", "TestGroup")]
        public static Func<string, bool> StaticPropertyPredicate => value => !string.IsNullOrEmpty(value);

                 [ValidationPredicate("InstancePropertyPredicate", "TestGroup")]
         public Func<string, bool> InstancePropertyPredicate => value => !string.IsNullOrEmpty(value);

         [ValidationPredicate("StaticFieldPredicate", "TestGroup")]
         public static Func<string, bool> StaticFieldPredicate = value => !string.IsNullOrEmpty(value);

         [ValidationPredicate("InstanceFieldPredicate", "TestGroup")]
         public Func<string, bool> InstanceFieldPredicate = value => !string.IsNullOrEmpty(value);
    }

    public class TestClassWithoutPredicates
    {
        public bool RegularMethod(string value) => !string.IsNullOrEmpty(value);
        public static string RegularProperty => "test";
        public static int RegularField = 42;
    }

    public class TestClassWithPrivatePredicates
    {
        [ValidationPredicate("PrivateMethodPredicate", "TestGroup")]
        private static bool PrivateMethodPredicate(string value) => !string.IsNullOrEmpty(value);
    }

    public class TestClassWithNullGroup
    {
        [ValidationPredicate("NullGroupPredicate")]
        public static bool NullGroupPredicate(string value) => !string.IsNullOrEmpty(value);
    }

    public class TestClassWithInvalidPredicates
    {
        [ValidationPredicate("InvalidMethodPredicate", "TestGroup")]
        public static string InvalidMethodPredicate(string value) => value; // Wrong return type

                 [ValidationPredicate("InvalidPropertyPredicate", "TestGroup")]
         public static string InvalidPropertyPredicate => "test"; // Wrong type

         [ValidationPredicate("PrivateGetterProperty", "TestGroup")]
         private static Func<string, bool> PrivateGetterProperty => value => !string.IsNullOrEmpty(value);
    }
}
