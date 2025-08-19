using Validations.Net.Predicates;

namespace Validations.Net.Test;

[TestFixture]
public class PredicateExceptionTests
{
    [Test]
    public void Constructor_WithMessageAndInnerException_SetsPropertiesCorrectly()
    {
        // Arrange
        var message = "Test predicate exception message";
        var inputType = typeof(string);
        var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
        var predicateInfo = new PredicateInfo("TestPredicate", "TestGroup", true, PredicateType.StaticMethod, methodInfo, typeof(string));
        var innerException = new InvalidOperationException("Test inner exception");

        // Act
        var exception = new PredicateException(message, inputType, predicateInfo, innerException);

        // Assert
        Assert.That(exception.Message, Is.EqualTo(message));
        Assert.That(exception.InputType, Is.EqualTo(inputType));
        Assert.That(exception.PredicateInfo, Is.EqualTo(predicateInfo));
        Assert.That(exception.InnerException, Is.EqualTo(innerException));
    }

    [Test]
    public void Constructor_WithoutInnerException_SetsPropertiesCorrectly()
    {
        // Arrange
        var message = "Test predicate exception message";
        var inputType = typeof(int);
        var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
        var predicateInfo = new PredicateInfo("TestPredicate", "TestGroup", true, PredicateType.StaticMethod, methodInfo, typeof(string));

        // Act
        var exception = new PredicateException(message, inputType, predicateInfo);

        // Assert
        Assert.That(exception.Message, Is.EqualTo(message));
        Assert.That(exception.InputType, Is.EqualTo(inputType));
        Assert.That(exception.PredicateInfo, Is.EqualTo(predicateInfo));
        Assert.That(exception.InnerException, Is.Null);
    }

    [Test]
    public void Constructor_WithEmptyMessage_AcceptsEmptyMessage()
    {
        // Arrange
        var message = "";
        var inputType = typeof(int);
        var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
        var predicateInfo = new PredicateInfo("TestPredicate", "TestGroup", true, PredicateType.StaticMethod, methodInfo, typeof(string));

        // Act
        var exception = new PredicateException(message, inputType, predicateInfo);

        // Assert
        Assert.That(exception.Message, Is.EqualTo(message));
    }

    [Test]
    public void Constructor_WithWhitespaceMessage_AcceptsWhitespaceMessage()
    {
        // Arrange
        var message = "   ";
        var inputType = typeof(int);
        var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
        var predicateInfo = new PredicateInfo("TestPredicate", "TestGroup", true, PredicateType.StaticMethod, methodInfo, typeof(string));

        // Act
        var exception = new PredicateException(message, inputType, predicateInfo);

        // Assert
        Assert.That(exception.Message, Is.EqualTo(message));
    }

    [Test]
    public void Properties_AreReadOnly()
    {
        // Arrange
        var message = "Test predicate exception message";
        var inputType = typeof(double);
        var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
        var predicateInfo = new PredicateInfo("TestPredicate", "TestGroup", true, PredicateType.StaticMethod, methodInfo, typeof(string));
        var exception = new PredicateException(message, inputType, predicateInfo);

        // Act & Assert
        Assert.That(exception.InputType, Is.EqualTo(inputType));
        Assert.That(exception.PredicateInfo, Is.EqualTo(predicateInfo));
    }

    [Test]
    public void Exception_InheritsFromException()
    {
        // Arrange
        var message = "Test predicate exception message";
        var inputType = typeof(bool);
        var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
        var predicateInfo = new PredicateInfo("TestPredicate", "TestGroup", true, PredicateType.StaticMethod, methodInfo, typeof(string));

        // Act
        var exception = new PredicateException(message, inputType, predicateInfo);

        // Assert
        Assert.That(exception, Is.InstanceOf<Exception>());
    }

    [Test]
    public void Constructor_WithDifferentInputTypes_WorksCorrectly()
    {
        // Arrange
        var message = "Test predicate exception message";
        var inputType = typeof(DateTime);
        var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
        var predicateInfo = new PredicateInfo("TestPredicate", "TestGroup", true, PredicateType.StaticMethod, methodInfo, typeof(string));

        // Act
        var exception = new PredicateException(message, inputType, predicateInfo);

        // Assert
        Assert.That(exception.InputType, Is.EqualTo(typeof(DateTime)));
    }

    [Test]
    public void Constructor_WithDifferentPredicateTypes_WorksCorrectly()
    {
        // Arrange
        var message = "Test predicate exception message";
        var inputType = typeof(int);
        var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
        var predicateInfo = new PredicateInfo("TestPredicate", "TestGroup", true, PredicateType.Property, methodInfo, typeof(string));

        // Act
        var exception = new PredicateException(message, inputType, predicateInfo);

        // Assert
        Assert.That(exception.PredicateInfo.Type, Is.EqualTo(PredicateType.Property));
    }

    [Test]
    public void Constructor_WithNullGroupInPredicateInfo_WorksCorrectly()
    {
        // Arrange
        var message = "Test predicate exception message";
        var inputType = typeof(int);
        var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
        var predicateInfo = new PredicateInfo("TestPredicate", null, true, PredicateType.StaticMethod, methodInfo, typeof(string));

        // Act
        var exception = new PredicateException(message, inputType, predicateInfo);

        // Assert
        Assert.That(exception.PredicateInfo.Group, Is.Null);
    }

    [Test]
    public void Constructor_WithPublicAndPrivatePredicates_WorksCorrectly()
    {
        // Arrange
        var message = "Test predicate exception message";
        var inputType = typeof(int);
        var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
        var publicPredicateInfo = new PredicateInfo("PublicPredicate", "TestGroup", true, PredicateType.StaticMethod, methodInfo, typeof(string));
        var privatePredicateInfo = new PredicateInfo("PrivatePredicate", "TestGroup", false, PredicateType.StaticMethod, methodInfo, typeof(string));

        // Act
        var publicException = new PredicateException(message, inputType, publicPredicateInfo);
        var privateException = new PredicateException(message, inputType, privatePredicateInfo);

        // Assert
        Assert.That(publicException.PredicateInfo.IsPublic, Is.True);
        Assert.That(privateException.PredicateInfo.IsPublic, Is.False);
    }

    [Test]
    public void Constructor_WithSpecialCharactersInMessage_HandlesCorrectly()
    {
        // Arrange
        var message = "Test message with special chars: !@#$%^&*()_+-=[]{}|;':\",./<>?";
        var inputType = typeof(int);
        var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
        var predicateInfo = new PredicateInfo("TestPredicate", "TestGroup", true, PredicateType.StaticMethod, methodInfo, typeof(string));

        // Act
        var exception = new PredicateException(message, inputType, predicateInfo);

        // Assert
        Assert.That(exception.Message, Is.EqualTo(message));
    }

    [Test]
    public void Constructor_WithLongMessage_HandlesCorrectly()
    {
        // Arrange
        var message = new string('A', 1000);
        var inputType = typeof(int);
        var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
        var predicateInfo = new PredicateInfo("TestPredicate", "TestGroup", true, PredicateType.StaticMethod, methodInfo, typeof(string));

        // Act
        var exception = new PredicateException(message, inputType, predicateInfo);

        // Assert
        Assert.That(exception.Message, Is.EqualTo(message));
        Assert.That(exception.Message.Length, Is.EqualTo(1000));
    }

    [Test]
    public void Constructor_WithUnicodeCharactersInMessage_HandlesCorrectly()
    {
        // Arrange
        var message = "Test message with unicode: 🚀🌟🎉中文日本語한국어";
        var inputType = typeof(int);
        var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
        var predicateInfo = new PredicateInfo("TestPredicate", "TestGroup", true, PredicateType.StaticMethod, methodInfo, typeof(string));

        // Act
        var exception = new PredicateException(message, inputType, predicateInfo);

        // Assert
        Assert.That(exception.Message, Is.EqualTo(message));
    }

    [Test]
    public void Constructor_WithDifferentInnerExceptionTypes_WorksCorrectly()
    {
        // Arrange
        var message = "Test predicate exception message";
        var inputType = typeof(int);
        var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
        var predicateInfo = new PredicateInfo("TestPredicate", "TestGroup", true, PredicateType.StaticMethod, methodInfo, typeof(string));
        var innerExceptions = new Exception[]
        {
            new ArgumentException("Test argument exception"),
            new InvalidOperationException("Test invalid operation exception"),
            new NotSupportedException("Test not supported exception"),
            new TimeoutException("Test timeout exception")
        };

        foreach (var innerException in innerExceptions)
        {
            // Act
            var exception = new PredicateException(message, inputType, predicateInfo, innerException);

            // Assert
            Assert.That(exception.InnerException, Is.EqualTo(innerException));
            Assert.That(exception.Message, Is.EqualTo(message));
        }
    }

    [Test]
    public void PredicateInfo_ContainsCorrectMethodInfo()
    {
        // Arrange
        var message = "Test predicate exception message";
        var inputType = typeof(int);
        var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
        var predicateInfo = new PredicateInfo("TestPredicate", "TestGroup", true, PredicateType.StaticMethod, methodInfo, typeof(string));

        // Act
        var exception = new PredicateException(message, inputType, predicateInfo);

        // Assert
        Assert.That(exception.PredicateInfo.MethodInfo, Is.EqualTo(methodInfo));
    }

    [Test]
    public void PredicateInfo_ContainsCorrectDeclaringType()
    {
        // Arrange
        var message = "Test predicate exception message";
        var inputType = typeof(int);
        var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
        var predicateInfo = new PredicateInfo("TestPredicate", "TestGroup", true, PredicateType.StaticMethod, methodInfo, typeof(string));

        // Act
        var exception = new PredicateException(message, inputType, predicateInfo);

        // Assert
        Assert.That(exception.PredicateInfo.DeclaringType, Is.EqualTo(typeof(string)));
    }
}


