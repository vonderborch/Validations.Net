using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Predicates;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class AgainstPredicateTests
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
    public void ValidatorName_IsCorrect()
    {
        // Assert
        Assert.That(AgainstPredicate.ValidatorName, Is.EqualTo("AgainstPredicate"));
    }

    [Test]
    public void ValidationFailureMessage_IsCorrect()
    {
        // Assert
        Assert.That(AgainstPredicate.ValidationFailureMessage, Is.EqualTo("Parameter must meet the predicate"));
    }

    [Test]
    public void CheckAgainstPredicate_WithValidPredicate_ReturnsTrue()
    {
        // Arrange
        string value = "test";
        Func<string?, bool> predicate = v => !string.IsNullOrEmpty(v);

        // Act
        var result = value.CheckAgainstPredicate(predicate);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckAgainstPredicate_WithInvalidPredicate_ReturnsFalse()
    {
        // Arrange
        string value = "";
        Func<string?, bool> predicate = v => !string.IsNullOrEmpty(v);

        // Act
        var result = value.CheckAgainstPredicate(predicate);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckAgainstPredicate_WithNullValue_ReturnsFalse()
    {
        // Arrange
        string? value = null;
        Func<string?, bool> predicate = v => !string.IsNullOrEmpty(v);

        // Act
        var result = value.CheckAgainstPredicate(predicate);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckAgainstPredicate_WithComplexPredicate_ReturnsTrue()
    {
        // Arrange
        int? value = 42;
        Func<int?, bool> predicate = v => v.HasValue && v.Value > 0 && v.Value < 100;

        // Act
        var result = value.CheckAgainstPredicate(predicate);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckAgainstPredicate_WithComplexPredicate_ReturnsFalse()
    {
        // Arrange
        int? value = 150;
        Func<int?, bool> predicate = v => v.HasValue && v.Value > 0 && v.Value < 100;

        // Act
        var result = value.CheckAgainstPredicate(predicate);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckAgainstPredicate_WithRegisteredPredicate_ReturnsTrue()
    {
        // Arrange
        string value = "test";
        var testInstance = new TestClass();

        // Act
        var result = value.CheckAgainstPredicate("IsValidString", "TestGroup", testInstance);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckAgainstPredicate_WithRegisteredPredicate_ReturnsFalse()
    {
        // Arrange
        string value = "";
        var testInstance = new TestClass();

        // Act
        var result = value.CheckAgainstPredicate("IsValidString", "TestGroup", testInstance);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckAgainstPredicate_WithNonExistentPredicate_ThrowsException()
    {
        // Arrange
        string value = "test";
        var testInstance = new TestClass();

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => value.CheckAgainstPredicate("NonExistentPredicate", "TestGroup", testInstance));
    }

    [Test]
    public void CheckAgainstPredicate_WithGlobalPredicate_ReturnsTrue()
    {
        // Arrange
        string value = "test";

        // Act
        var result = value.CheckAgainstPredicate("IsValidString", "TestGroup", null);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckAgainstPredicate_WithGlobalPredicate_ReturnsFalse()
    {
        // Arrange
        string value = "";

        // Act
        var result = value.CheckAgainstPredicate("IsValidString", "TestGroup", null);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void EnsureAgainstPredicate_WithValidPredicate_ReturnsValue()
    {
        // Arrange
        string value = "test";
        Func<string?, bool> predicate = v => !string.IsNullOrEmpty(v);

        // Act
        var result = value.EnsureAgainstPredicate(predicate);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void EnsureAgainstPredicate_WithInvalidPredicate_ThrowsValidationException()
    {
        // Arrange
        string value = "";
        Func<string?, bool> predicate = v => !string.IsNullOrEmpty(v);

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureAgainstPredicate(predicate));
        Assert.That(exception.Message, Does.Contain("Parameter must meet the predicate"));
    }

    [Test]
    public void EnsureAgainstPredicate_WithRegisteredPredicate_ReturnsValue()
    {
        // Arrange
        string value = "test";
        var testInstance = new TestClass();

        // Act
        var result = value.EnsureAgainstPredicate("IsValidString", "TestGroup", testInstance);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void EnsureAgainstPredicate_WithRegisteredPredicate_ThrowsValidationException()
    {
        // Arrange
        string value = "";
        var testInstance = new TestClass();

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureAgainstPredicate("IsValidString", "TestGroup", testInstance));
        Assert.That(exception.Message, Does.Contain("Parameter must meet the predicate"));
    }

    [Test]
    public void EnsureAgainstPredicate_WithGlobalPredicate_ReturnsValue()
    {
        // Arrange
        string value = "test";

        // Act
        var result = value.EnsureAgainstPredicate("IsValidString", "TestGroup", null);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void EnsureAgainstPredicate_WithGlobalPredicate_ThrowsValidationException()
    {
        // Arrange
        string value = "";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureAgainstPredicate("IsValidString", "TestGroup", null));
        Assert.That(exception.Message, Does.Contain("Parameter must meet the predicate"));
    }

    [Test]
    public void EnsureAgainstPredicate_WithParameterName_ThrowsValidationExceptionWithParameterName()
    {
        // Arrange
        string value = "";
        string parameterName = "testParameter";
        Func<string?, bool> predicate = v => !string.IsNullOrEmpty(v);

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureAgainstPredicate(predicate, parameterName));
        Assert.That(exception.Message, Does.Contain("Parameter must meet the predicate"));
    }

    [Test]
    public void EnsureAgainstPredicate_WithBlackboard_ThrowsValidationException()
    {
        // Arrange
        string value = "";
        var blackboard = new Blackboard();
        Func<string?, bool> predicate = v => !string.IsNullOrEmpty(v);

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureAgainstPredicate(predicate, null, blackboard));
        Assert.That(exception.Message, Does.Contain("Parameter must meet the predicate"));
    }

    [Test]
    public void ValidateAgainstPredicate_WithValidPredicate_ReturnsValidResult()
    {
        // Arrange
        string value = "test";
        Func<string?, bool> predicate = v => !string.IsNullOrEmpty(v);

        // Act
        var result = value.ValidateAgainstPredicate(predicate);

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateAgainstPredicate_WithInvalidPredicate_ReturnsInvalidResult()
    {
        // Arrange
        string value = "";
        Func<string?, bool> predicate = v => !string.IsNullOrEmpty(v);

        // Act
        var result = value.ValidateAgainstPredicate(predicate);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must meet the predicate"));
    }

    [Test]
    public void ValidateAgainstPredicate_WithRegisteredPredicate_ReturnsValidResult()
    {
        // Arrange
        string value = "test";
        var testInstance = new TestClass();

        // Act
        var result = value.ValidateAgainstPredicate("IsValidString", "TestGroup", testInstance);

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateAgainstPredicate_WithRegisteredPredicate_ReturnsInvalidResult()
    {
        // Arrange
        string value = "";
        var testInstance = new TestClass();

        // Act
        var result = value.ValidateAgainstPredicate("IsValidString", "TestGroup", testInstance);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must meet the predicate"));
    }

    [Test]
    public void ValidateAgainstPredicate_WithGlobalPredicate_ReturnsValidResult()
    {
        // Arrange
        string value = "test";

        // Act
        var result = value.ValidateAgainstPredicate("IsValidString", "TestGroup", null);

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateAgainstPredicate_WithGlobalPredicate_ReturnsInvalidResult()
    {
        // Arrange
        string value = "";

        // Act
        var result = value.ValidateAgainstPredicate("IsValidString", "TestGroup", null);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must meet the predicate"));
    }

    [Test]
    public void ValidateAgainstPredicate_WithParameterName_ReturnsInvalidResultWithParameterName()
    {
        // Arrange
        string value = "";
        string parameterName = "testParameter";
        Func<string?, bool> predicate = v => !string.IsNullOrEmpty(v);

        // Act
        var result = value.ValidateAgainstPredicate(predicate, parameterName);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must meet the predicate"));
    }

    [Test]
    public void ValidateAgainstPredicate_WithBlackboard_ReturnsInvalidResult()
    {
        // Arrange
        string value = "";
        var blackboard = new Blackboard();
        Func<string?, bool> predicate = v => !string.IsNullOrEmpty(v);

        // Act
        var result = value.ValidateAgainstPredicate(predicate, null, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must meet the predicate"));
    }

    [Test]
    public void ValidateAgainstPredicate_WithNullValue_ReturnsInvalidResult()
    {
        // Arrange
        string? value = null;
        Func<string?, bool> predicate = v => !string.IsNullOrEmpty(v);

        // Act
        var result = value.ValidateAgainstPredicate(predicate);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must meet the predicate"));
    }

    [Test]
    public void ValidateAgainstPredicate_WithComplexObject_ReturnsValidResult()
    {
        // Arrange
        var value = new { Name = "Test", Value = 42 };
        Func<object?, bool> predicate = v => v != null && v.ToString()!.Contains("Test");

        // Act
        var result = value.ValidateAgainstPredicate(predicate);

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateAgainstPredicate_WithComplexObject_ReturnsInvalidResult()
    {
        // Arrange
        var value = new { Name = "Other", Value = 42 };
        Func<object?, bool> predicate = v => v != null && v.ToString()!.Contains("Test");

        // Act
        var result = value.ValidateAgainstPredicate(predicate);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must meet the predicate"));
    }

    [Test]
    public void ValidateAgainstPredicate_WithValueType_ReturnsValidResult()
    {
        // Arrange
        int? value = 42;
        Func<int?, bool> predicate = v => v.HasValue && v.Value > 0 && v.Value < 100;

        // Act
        var result = value.ValidateAgainstPredicate(predicate);

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateAgainstPredicate_WithValueType_ReturnsInvalidResult()
    {
        // Arrange
        int? value = 150;
        Func<int?, bool> predicate = v => v.HasValue && v.Value > 0 && v.Value < 100;

        // Act
        var result = value.ValidateAgainstPredicate(predicate);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must meet the predicate"));
    }

    // Test class for predicate registration
    public class TestClass
    {
        [ValidationPredicate("IsValidString", "TestGroup")]
        public static bool IsValidString(string value) => !string.IsNullOrEmpty(value);

        [ValidationPredicate("IsValidNumber", "TestGroup")]
        public static bool IsValidNumber(int value) => value > 0 && value < 100;
    }
}
