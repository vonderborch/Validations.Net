using SimpleBlackboard.Net;
using Validations.Net.ValidationAttributes.Helpers;

namespace Validations.Net;

/// <summary>
///     /// Represents an exception that is thrown when a validation fails.
/// </summary>
public class ValidationException : Exception
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ValidationException" /> class with a specified parameter name,
    /// </summary>
    /// <param name="validator">
    ///     The validator that failed, which can be used to identify the specific validation rule that was
    ///     not met.
    /// </param>
    /// <param name="parameterName">The name of the parameter that failed validation.</param>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="blackboard">
    ///     An optional object that can contain additional context or data related to the validation
    ///     failure.
    /// </param>
    /// <param name="exceptionContext">
    ///     An optional context object that can provide additional information about the validation
    ///     failure.
    /// </param>
    public ValidationException(string validator, string parameterName, string message, IBlackboard? blackboard,
        Dictionary<string, object?> exceptionContext = null) : base(message)
    {
        this.ParameterName = parameterName;
        this.Blackboard = blackboard;
        this.ExceptionContext = new ValidationExceptionContext(exceptionContext);
        this.Validator = validator;
    }

    /// <summary>
    ///     Gets the blackboard associated with the validation exception, which can contain additional context or data
    ///     related to the validation failure.
    /// </summary>
    public object? Blackboard { get; }

    /// <summary>
    ///     Gets the context associated with the validation exception, which can provide additional information about the
    ///     validation failure.
    /// </summary>
    public ValidationExceptionContext ExceptionContext { get; }

    /// <summary>
    ///     Gets the name of the parameter that failed validation, which can be used to identify the specific input that
    ///     caused the validation failure.
    /// </summary>
    public string ParameterName { get; }

    /// <summary>
    ///     The validator that failed, which can be used to identify the specific validation rule that was not met.
    /// </summary>
    public string Validator { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="ValidationException" /> class
    /// when a value does not match the expected type.
    /// </summary>
    /// <typeparam name="T">The expected type of the value.</typeparam>
    /// <param name="validator">
    /// The name of the validator triggering the exception, typically identifying the validation rule.
    /// </param>
    /// <param name="parameterName">
    /// The name of the parameter whose value failed validation.
    /// </param>
    /// <param name="value">
    /// The actual value provided, which didn't match the expected type.
    /// </param>
    /// <param name="blackboard">
    /// An optional object that can provide additional context or state information
    /// related to the validation failure.
    /// </param>
    /// <returns>
    /// A <see cref="ValidationException" /> initialized with details about the type mismatch failure.
    /// </returns>
    public static ValidationException CreateFromTypeMisMatch<T>(string validator, string parameterName, object? value,
        IBlackboard? blackboard = null)
    {
        return new ValidationException($"{validator}->TypeMismatch", parameterName,
            $"{parameterName} must be of type {typeof(T).Name}.", blackboard, new Dictionary<string, object?>
            {
                { "expectedType", typeof(T).Name },
                { "actualType", value?.GetType().Name ?? "null" },
                { "value", value }
            });
    }

    /// <summary>
    /// Creates a new instance of the <see cref="ValidationException" /> class when a failure occurs while
    /// fetching a validation predicate. This method populates the exception with details about the failed
    /// predicate fetch operation, including relevant metadata and context.
    /// </summary>
    /// <param name="validator">
    /// The name of the validator that encountered the predicate fetch failure.
    /// </param>
    /// <param name="parameterName">
    /// The name of the parameter associated with the validation.
    /// </param>
    /// <param name="predicateName">
    /// The name of the predicate that could not be fetched.
    /// </param>
    /// <param name="predicateGroup">
    /// The optional group to which the predicate belongs.
    /// </param>
    /// <param name="instance">
    /// The instance that was being validated when the fetch failure occurred.
    /// </param>
    /// <param name="blackboard">
    /// An optional object that can provide additional context or data related to the validation failure.
    /// </param>
    /// <returns>
    /// A <see cref="ValidationException" /> instance populated with details regarding the failed
    /// predicate fetch operation.
    /// </returns>
    public static ValidationException CreateFromFetchPredicateFailure(string validator, string parameterName,
        string predicateName, string? predicateGroup,
        object? instance, IBlackboard? blackboard = null)
    {
        var predicateKey = PredicateInfo.GetKey(predicateName, predicateGroup);
        return new ValidationException($"{validator}->PredicateFetchFailure", parameterName,
            $"Failed to fetch predicate with key {predicateKey}.", blackboard, new Dictionary<string, object?>
            {
                { "predicateName", predicateName },
                { "predicateGroup", predicateGroup },
                { "predicateKey", predicateKey },
                { "instance", instance }
            });
    }
}
