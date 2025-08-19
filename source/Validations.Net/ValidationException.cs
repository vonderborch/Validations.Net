using SimpleBlackboard.Net;

namespace Validations.Net;

/// <summary>
/// Represents an exception that occurs during validation processes.
/// This exception can provide additional context and information about the validation failure,
/// such as the parameter that failed validation, the specific validator that failed, and any associated data.
/// </summary>
public class ValidationException : Exception
{
    private ValidationException(string message, string validator, string? parameterName, ValidationContext context,
        IBlackboard? blackboard = null) : base(message)
    {
        this.ParameterName = parameterName;
        this.Validator = validator;
        this.Context = context;
        this.Blackboard = blackboard;
    }

    /// <summary>
    ///     Gets the blackboard associated with the validation exception, which can contain additional context or data
    ///     related to the validation failure.
    /// </summary>
    public readonly IBlackboard? Blackboard;
    
    /// <summary>
    ///     Gets the context associated with the validation exception, which can provide additional information about
    ///     the validation failure.
    /// </summary>
    public readonly ValidationContext Context;

    /// <summary>
    ///     The validator that failed, which can be used to identify the specific validation rule that was not met.
    /// </summary>
    public readonly string Validator;

    /// <summary>
    ///     Gets the name of the parameter that failed validation, which can be used to identify the specific input that
    ///     caused the validation failure.
    /// </summary>
    public readonly string? ParameterName;

    /// <summary>
    /// Creates a new instance of <see cref="ValidationException"/> with a detailed error message,
    /// including information about the validator that failed, the parameter that caused the failure,
    /// the validation context, and an optional blackboard for additional context.
    /// </summary>
    /// <param name="validator">The name of the validator that failed the validation process.</param>
    /// <param name="message">An additional message to display as part of the exception.</param>
    /// <param name="parameterName">The name of the parameter that failed validation.</param>
    /// <param name="blackboard">Optional parameter that provides additional context information, allowing for key/value storage.</param>
    /// <param name="context">The context in which the validation occurred, providing additional details about the failure.</param>
    /// <returns>A new instance of the <see cref="ValidationException"/> class with the provided details.</returns>
    public static ValidationException Create(string validator, string message, string? parameterName, IBlackboard? blackboard, ValidationContext context)
    {
        ValidationException exception = new ValidationException($"`{validator}` failed against parameter `{parameterName}`: {message}", validator, parameterName, context, blackboard);
        return exception;
    }

    /// <summary>
    /// Creates a new instance of <see cref="ValidationException"/> with specific details about the validation failure,
    /// such as the validator name, parameter name, validation context, and optional blackboard for additional data.
    /// </summary>
    /// <param name="validator">The name of the validator that triggered the validation failure.</param>
    /// <param name="message">An additional message to display as part of the exception.</param>
    /// <param name="parameterName">The name of the parameter that caused the failure, or null if not applicable.</param>
    /// <param name="blackboard">Optional blackboard for storing additional context or data about the failure.</param>
    /// <param name="context">The validation context containing detailed information about the failure.</param>
    /// <returns>An instance of <see cref="ValidationException"/> populated with the provided information.</returns>
    public static ValidationException Create(string validator, string message, string? parameterName, IBlackboard? blackboard,
        List<(string key, object? value)> context
        )
    {
        Dictionary<string, object?> contextDictionary = new();
        foreach (var (key, value) in context)
        {
            contextDictionary[key] = value;
        }
        
        ValidationContext validationContext = new(contextDictionary);
        ValidationException exception = Create(validator, message, parameterName, blackboard, validationContext);
        return exception;
    }
}
