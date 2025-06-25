using SimpleBlackboard.Net;

namespace Validations.Net;

/// <summary>
/// Represents an exception that occurs during validation processes.
/// This exception can provide additional context and information about the validation failure,
/// such as the parameter that failed validation, the specific validator that failed, and any associated data.
/// </summary>
public class ValidationException : Exception
{
    private ValidationException(string message, string validator, string parameterName, ValidationContext context,
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
    public readonly string ParameterName;

    /// <summary>
    /// Creates a new instance of <see cref="ValidationException"/> with a detailed error message,
    /// including information about the validator that failed, the parameter that caused the failure,
    /// the validation context, and an optional blackboard for additional context.
    /// </summary>
    /// <param name="validator">The name of the validator that failed the validation process.</param>
    /// <param name="parameterName">The name of the parameter that failed validation.</param>
    /// <param name="context">The context in which the validation occurred, providing additional details about the failure.</param>
    /// <param name="blackboard">Optional parameter that provides additional context information, allowing for key/value storage.</param>
    /// <returns>A new instance of the <see cref="ValidationException"/> class with the provided details.</returns>
    public static ValidationException Create(string validator, string parameterName, ValidationContext context,
        IBlackboard? blackboard = null)
    {
        return new ValidationException($"`{validator}` failed against parameter `{parameterName}`", validator, parameterName, context, blackboard);
    }
}
