using SimpleBlackboard.Net;

namespace Validations.Net;

/// <summary>
/// /// Represents an exception that is thrown when a validation fails.
/// </summary>
public class ValidationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class with a specified parameter name,
    /// </summary>
    /// <param name="validator">The validator that failed, which can be used to identify the specific validation rule that was not met.</param>
    /// <param name="parameterName">The name of the parameter that failed validation.</param>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="blackboard">An optional object that can contain additional context or data related to the validation failure.</param>
    /// <param name="exceptionContext">An optional context object that can provide additional information about the validation failure.</param>
    public ValidationException(string validator, string parameterName, string message, Blackboard? blackboard, Dictionary<string, object?> exceptionContext = null) : base(message)
    {
        this.ParameterName = parameterName;
        this.Blackboard = blackboard;
        this.ExceptionContext = new(exceptionContext);
        this.Validator = validator;
    }
    
    /// <summary>
    /// Gets the name of the parameter that failed validation, which can be used to identify the specific input that
    /// caused the validation failure.
    /// </summary>
    public string ParameterName { get; }
    
    /// <summary>
    /// Gets the blackboard associated with the validation exception, which can contain additional context or data
    /// related to the validation failure.
    /// </summary>
    public object? Blackboard { get; }

    /// <summary>
    /// Gets the context associated with the validation exception, which can provide additional information about the
    /// validation failure.
    /// </summary>
    public ValidationExceptionContext ExceptionContext { get; }
    
    /// <summary>
    /// The validator that failed, which can be used to identify the specific validation rule that was not met.
    /// </summary>
    public string Validator { get; }

    public static ValidationException CreateFromTypeMisMatch<T>(string validator, string parameterName, object? value, Blackboard? blackboard = null)
    {
        return new ValidationException($"{validator}->TypeMismatch", parameterName, $"{parameterName} must be of type {typeof(T).Name}.", blackboard, new Dictionary<string, object?>
        {
            { "expectedType", typeof(T).Name },
            { "actualType", value?.GetType().Name ?? "null" },
            { "value", value }
        });
    }
}
