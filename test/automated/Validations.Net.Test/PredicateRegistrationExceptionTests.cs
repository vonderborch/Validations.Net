using Validations.Net.Predicates;

namespace Validations.Net.Test;

[TestFixture]
public class PredicateRegistrationExceptionTests
{
    [Test]
    public void Constructor_WithValidParameters_SetsPropertiesCorrectly()
    {
        // Arrange
        var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
        var predicateInfo = new PredicateInfo("TestPredicate", "TestGroup", true, PredicateType.StaticMethod, methodInfo, typeof(string));
        var inputType = typeof(int);
        var innerException = new InvalidOperationException("Test inner exception");

        // Act
        var exception = new PredicateRegistrationException(predicateInfo, inputType, innerException);

        // Assert
        Assert.That(exception.PredicateInfo, Is.EqualTo(predicateInfo));
        Assert.That(exception.InputType, Is.EqualTo(inputType));
        Assert.That(exception.InnerException, Is.EqualTo(innerException));
    }

    [Test]
    public void Properties_AreReadOnly()
    {
        // Arrange
        var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
        var predicateInfo = new PredicateInfo("TestPredicate", "TestGroup", true, PredicateType.StaticMethod, methodInfo, typeof(string));
        var inputType = typeof(int);
        var innerException = new InvalidOperationException("Test inner exception");
        var exception = new PredicateRegistrationException(predicateInfo, inputType, innerException);

        // Act & Assert
        Assert.That(exception.PredicateInfo, Is.EqualTo(predicateInfo));
        Assert.That(exception.InputType, Is.EqualTo(inputType));
        Assert.That(exception.InnerException, Is.EqualTo(innerException));
    }

    [Test]
    public void Exception_InheritsFromException()
    {
        // Arrange
        var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
        var predicateInfo = new PredicateInfo("TestPredicate", "TestGroup", true, PredicateType.StaticMethod, methodInfo, typeof(string));
        var inputType = typeof(int);
        var innerException = new InvalidOperationException("Test inner exception");

        // Act
        var exception = new PredicateRegistrationException(predicateInfo, inputType, innerException);

        // Assert
        Assert.That(exception, Is.InstanceOf<Exception>());
    }

    [Test]
    public void Constructor_WithDifferentPredicateTypes_WorksCorrectly()
    {
        // Arrange
        var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
        var predicateInfo = new PredicateInfo("TestPredicate", "TestGroup", true, PredicateType.Method, methodInfo, typeof(string));
        var inputType = typeof(int);
        var innerException = new InvalidOperationException("Test inner exception");

        // Act
        var exception = new PredicateRegistrationException(predicateInfo, inputType, innerException);

        // Assert
        Assert.That(exception.PredicateInfo.Type, Is.EqualTo(PredicateType.Method));
    }

    [Test]
    public void Constructor_WithDifferentInputTypes_WorksCorrectly()
    {
        // Arrange
        var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
        var predicateInfo = new PredicateInfo("TestPredicate", "TestGroup", true, PredicateType.StaticMethod, methodInfo, typeof(string));
        var inputType = typeof(double);
        var innerException = new InvalidOperationException("Test inner exception");

        // Act
        var exception = new PredicateRegistrationException(predicateInfo, inputType, innerException);

        // Assert
        Assert.That(exception.InputType, Is.EqualTo(typeof(double)));
    }

    [Test]
    public void Constructor_WithNullGroupInPredicateInfo_WorksCorrectly()
    {
        // Arrange
        var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
        var predicateInfo = new PredicateInfo("TestPredicate", null, true, PredicateType.StaticMethod, methodInfo, typeof(string));
        var inputType = typeof(int);
        var innerException = new InvalidOperationException("Test inner exception");

        // Act
        var exception = new PredicateRegistrationException(predicateInfo, inputType, innerException);

        // Assert
        Assert.That(exception.PredicateInfo.Group, Is.Null);
    }

    [Test]
    public void Constructor_WithPublicAndPrivatePredicates_WorksCorrectly()
    {
        // Arrange
        var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
        var publicPredicateInfo = new PredicateInfo("PublicPredicate", "TestGroup", true, PredicateType.StaticMethod, methodInfo, typeof(string));
        var privatePredicateInfo = new PredicateInfo("PrivatePredicate", "TestGroup", false, PredicateType.StaticMethod, methodInfo, typeof(string));
        var inputType = typeof(int);
        var innerException = new InvalidOperationException("Test inner exception");

        // Act
        var publicException = new PredicateRegistrationException(publicPredicateInfo, inputType, innerException);
        var privateException = new PredicateRegistrationException(privatePredicateInfo, inputType, innerException);

        // Assert
        Assert.That(publicException.PredicateInfo.IsPublic, Is.True);
        Assert.That(privateException.PredicateInfo.IsPublic, Is.False);
    }

    [Test]
    public void InnerException_Property_ShadowsBaseInnerException()
    {
        // Arrange
        var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
        var predicateInfo = new PredicateInfo("TestPredicate", "TestGroup", true, PredicateType.StaticMethod, methodInfo, typeof(string));
        var inputType = typeof(int);
        var innerException = new InvalidOperationException("Test inner exception");

        // Act
        var exception = new PredicateRegistrationException(predicateInfo, inputType, innerException);

        // Assert
        Assert.That(exception.InnerException, Is.EqualTo(innerException));
    }

    [Test]
    public void PredicateInfo_ContainsCorrectMethodInfo()
    {
        // Arrange
        var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
        var predicateInfo = new PredicateInfo("TestPredicate", "TestGroup", true, PredicateType.StaticMethod, methodInfo, typeof(string));
        var inputType = typeof(int);
        var innerException = new InvalidOperationException("Test inner exception");

        // Act
        var exception = new PredicateRegistrationException(predicateInfo, inputType, innerException);

        // Assert
        Assert.That(exception.PredicateInfo.MethodInfo, Is.EqualTo(methodInfo));
    }

    [Test]
    public void PredicateInfo_ContainsCorrectDeclaringType()
    {
        // Arrange
        var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
        var predicateInfo = new PredicateInfo("TestPredicate", "TestGroup", true, PredicateType.StaticMethod, methodInfo, typeof(string));
        var inputType = typeof(int);
        var innerException = new InvalidOperationException("Test inner exception");

        // Act
        var exception = new PredicateRegistrationException(predicateInfo, inputType, innerException);

        // Assert
        Assert.That(exception.PredicateInfo.DeclaringType, Is.EqualTo(typeof(string)));
    }
}
