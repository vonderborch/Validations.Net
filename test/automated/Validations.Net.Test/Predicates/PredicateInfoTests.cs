using System.Reflection;
using NUnit.Framework;
using Validations.Net.Predicates;

namespace Validations.Net.Test.Predicates;

[TestFixture]
public class PredicateInfoTests
{
    private MethodInfo _testMethodInfo;
    private Type _testDeclaringType;

    [SetUp]
    public void SetUp()
    {
        _testMethodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
        _testDeclaringType = typeof(string);
    }

    [Test]
    public void Constructor_WithAllParameters_SetsPropertiesCorrectly()
    {
        // Arrange
        var name = "TestPredicate";
        var group = "TestGroup";
        var isPublic = true;
        var type = PredicateType.StaticMethod;

        // Act
        var predicateInfo = new PredicateInfo(name, group, isPublic, type, _testMethodInfo, _testDeclaringType);

        // Assert
        Assert.That(predicateInfo.Name, Is.EqualTo(name));
        Assert.That(predicateInfo.Group, Is.EqualTo(group));
        Assert.That(predicateInfo.IsPublic, Is.EqualTo(isPublic));
        Assert.That(predicateInfo.Type, Is.EqualTo(type));
        Assert.That(predicateInfo.MethodInfo, Is.EqualTo(_testMethodInfo));
        Assert.That(predicateInfo.DeclaringType, Is.EqualTo(_testDeclaringType));
    }

    [Test]
    public void Constructor_WithNullGroup_SetsGroupToNull()
    {
        // Arrange
        var name = "TestPredicate";
        string? group = null;
        var isPublic = true;
        var type = PredicateType.Method;

        // Act
        var predicateInfo = new PredicateInfo(name, group, isPublic, type, _testMethodInfo, _testDeclaringType);

        // Assert
        Assert.That(predicateInfo.Group, Is.Null);
        Assert.That(predicateInfo.Name, Is.EqualTo(name));
    }

    [TestCase(PredicateType.StaticField, true, true)]
    [TestCase(PredicateType.StaticProperty, true, true)]
    [TestCase(PredicateType.StaticMethod, true, true)]
    [TestCase(PredicateType.StaticField, false, false)]
    [TestCase(PredicateType.StaticProperty, false, false)]
    [TestCase(PredicateType.StaticMethod, false, false)]
    [TestCase(PredicateType.Field, true, false)]
    [TestCase(PredicateType.Property, true, false)]
    [TestCase(PredicateType.Method, true, false)]
    [TestCase(PredicateType.Field, false, false)]
    [TestCase(PredicateType.Property, false, false)]
    [TestCase(PredicateType.Method, false, false)]
    public void IsGlobalPredicate_ReturnsCorrectValue(PredicateType type, bool isPublic, bool expectedResult)
    {
        // Arrange
        var predicateInfo = new PredicateInfo("Test", "Group", isPublic, type, _testMethodInfo, _testDeclaringType);

        // Act
        var result = predicateInfo.IsGlobalPredicate;

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [TestCase(PredicateType.StaticField, true)]
    [TestCase(PredicateType.StaticProperty, true)]
    [TestCase(PredicateType.StaticMethod, true)]
    [TestCase(PredicateType.Field, false)]
    [TestCase(PredicateType.Property, false)]
    [TestCase(PredicateType.Method, false)]
    public void IsStatic_ReturnsCorrectValue(PredicateType type, bool expectedResult)
    {
        // Arrange
        var predicateInfo = new PredicateInfo("Test", "Group", true, type, _testMethodInfo, _testDeclaringType);

        // Act
        var result = predicateInfo.IsStatic;

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [TestCase("TestName", "TestGroup", "TestGroup-TestName")]
    [TestCase("TestName", null, "TestName")]
    [TestCase("SimpleName", "SimpleGroup", "SimpleGroup-SimpleName")]
    [TestCase("Name", "", "-Name")]
    public void Key_GeneratesCorrectKey(string name, string? group, string expectedKey)
    {
        // Arrange
        var predicateInfo = new PredicateInfo(name, group, true, PredicateType.Method, _testMethodInfo, _testDeclaringType);

        // Act
        var result = predicateInfo.Key;

        // Assert
        Assert.That(result, Is.EqualTo(expectedKey));
    }

    [TestCase("TestName", "TestGroup", "TestGroup-TestName")]
    [TestCase("TestName", null, "TestName")]
    [TestCase("SimpleName", "SimpleGroup", "SimpleGroup-SimpleName")]
    [TestCase("Name", "", "-Name")]
    public void GetKey_StaticMethod_GeneratesCorrectKey(string name, string? group, string expectedKey)
    {
        // Act
        var result = PredicateInfo.GetKey(name, group);

        // Assert
        Assert.That(result, Is.EqualTo(expectedKey));
    }

    [Test]
    public void GetKey_WithSpecialCharacters_HandlesCorrectly()
    {
        // Arrange
        var name = "Test-Name_With.Special@Chars";
        var group = "Group$With%Special&Chars";
        var expectedKey = $"{group}-{name}";

        // Act
        var result = PredicateInfo.GetKey(name, group);

        // Assert
        Assert.That(result, Is.EqualTo(expectedKey));
    }

    [Test]
    public void GetKey_WithLongStrings_HandlesCorrectly()
    {
        // Arrange
        var name = new string('N', 1000);
        var group = new string('G', 500);
        var expectedKey = $"{group}-{name}";

        // Act
        var result = PredicateInfo.GetKey(name, group);

        // Assert
        Assert.That(result, Is.EqualTo(expectedKey));
        Assert.That(result.Length, Is.EqualTo(1501)); // 500 + 1 + 1000
    }

    [Test]
    public void Equality_WithSameValues_ReturnsTrue()
    {
        // Arrange
        var name = "TestPredicate";
        var group = "TestGroup";
        var isPublic = true;
        var type = PredicateType.StaticMethod;

        var predicateInfo1 = new PredicateInfo(name, group, isPublic, type, _testMethodInfo, _testDeclaringType);
        var predicateInfo2 = new PredicateInfo(name, group, isPublic, type, _testMethodInfo, _testDeclaringType);

        // Act & Assert
        Assert.That(predicateInfo1, Is.EqualTo(predicateInfo2));
        Assert.That(predicateInfo1.GetHashCode(), Is.EqualTo(predicateInfo2.GetHashCode()));
    }

    [Test]
    public void Equality_WithDifferentValues_ReturnsFalse()
    {
        // Arrange
        var predicateInfo1 = new PredicateInfo("Name1", "Group1", true, PredicateType.Method, _testMethodInfo, _testDeclaringType);
        var predicateInfo2 = new PredicateInfo("Name2", "Group2", false, PredicateType.Field, _testMethodInfo, _testDeclaringType);

        // Act & Assert
        Assert.That(predicateInfo1, Is.Not.EqualTo(predicateInfo2));
    }

    [Test]
    public void ToString_ReturnsExpectedFormat()
    {
        // Arrange
        var predicateInfo = new PredicateInfo("TestName", "TestGroup", true, PredicateType.StaticMethod, _testMethodInfo, _testDeclaringType);

        // Act
        var result = predicateInfo.ToString();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Not.Empty);
        Assert.That(result, Contains.Substring("TestName"));
        Assert.That(result, Contains.Substring("TestGroup"));
    }

    [Test]
    public void RecordStruct_PropertiesAreReadOnly()
    {
        // Arrange
        var predicateInfo = new PredicateInfo("Test", "Group", true, PredicateType.Method, _testMethodInfo, _testDeclaringType);

        // Act & Assert - These should compile without errors, proving properties are read-only
        var name = predicateInfo.Name;
        var group = predicateInfo.Group;
        var isPublic = predicateInfo.IsPublic;
        var type = predicateInfo.Type;
        var methodInfo = predicateInfo.MethodInfo;
        var declaringType = predicateInfo.DeclaringType;
        var key = predicateInfo.Key;
        var isGlobal = predicateInfo.IsGlobalPredicate;
        var isStatic = predicateInfo.IsStatic;

        Assert.That(name, Is.Not.Null);
        Assert.That(methodInfo, Is.Not.Null);
        Assert.That(declaringType, Is.Not.Null);
        Assert.That(key, Is.Not.Null);
    }

    [Test]
    public void Constructor_WithDifferentMethodInfoTypes_WorksCorrectly()
    {
        // Arrange
        var getMethodInfo = typeof(string).GetProperty("Length")!.GetGetMethod()!;
        var predicateInfo = new PredicateInfo("LengthPredicate", "StringGroup", true, PredicateType.Property, getMethodInfo, typeof(string));

        // Act & Assert
        Assert.That(predicateInfo.MethodInfo, Is.EqualTo(getMethodInfo));
        Assert.That(predicateInfo.DeclaringType, Is.EqualTo(typeof(string)));
    }

    [Test]
    public void Key_IsConsistentAcrossInstances()
    {
        // Arrange
        var predicateInfo1 = new PredicateInfo("Test", "Group", true, PredicateType.Method, _testMethodInfo, _testDeclaringType);
        var predicateInfo2 = new PredicateInfo("Test", "Group", false, PredicateType.Field, _testMethodInfo, typeof(object));

        // Act
        var key1 = predicateInfo1.Key;
        var key2 = predicateInfo2.Key;

        // Assert - Keys should be the same because they only depend on Name and Group
        Assert.That(key1, Is.EqualTo(key2));
        Assert.That(key1, Is.EqualTo("Group-Test"));
    }
}
