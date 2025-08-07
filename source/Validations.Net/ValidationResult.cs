using SimpleBlackboard.Net;

namespace Validations.Net;

/// <summary>
/// Represents the result of a validation process, indicating whether the validation
/// was successful and providing details about any encountered exceptions during validation.
/// </summary>
public record struct ValidationResult
{
    /// <summary>
    /// Represents the outcome of a process to determine if the given input meets
    /// predefined conditions or rules.
    /// </summary>
    public readonly bool IsValid;

    /// <summary>
    /// Represents an exception that occurs during the validation process,
    /// typically when input data does not meet specified validation rules or conditions.
    /// </summary>
    public readonly ValidationException? ValidationException;

    /// <summary>
    /// Represents an exception that is raised when a predicate used in a validation
    /// process fails to meet the required conditions or expected logic.
    /// </summary>
    public readonly PredicateException? PredicateException;

    /// <summary>
    /// Represents a message describing the exception that occurred during the validation process,
    /// providing additional contextual information about the nature or cause of the validation error.
    /// </summary>
    public readonly string? ExceptionMessage;

    /// <summary>
    /// Represents the result of a validation operation, providing details about
    /// whether the validation was successful and any exceptions that occurred during the process.
    /// </summary>
    private ValidationResult(bool isValid, ValidationException? validationException,
        PredicateException? predicateException)
    {
        this.IsValid = isValid;
        this.ValidationException = validationException;
        this.PredicateException = predicateException;
        this.ExceptionMessage = validationException?.Message ?? predicateException?.Message;
    }

    /// <summary>
    /// Creates a new instance of <see cref="ValidationResult"/> representing a successful validation result,
    /// with no associated exceptions or error messages.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="ValidationResult"/> indicating that the validation was successful.
    /// </returns>
    public static ValidationResult CreateFromValidationSuccess()
    {
        ValidationResult result = new ValidationResult(true, null, null);
        return result;
    }

    /// <summary>
    /// Creates a new instance of <see cref="ValidationResult"/> based on a validation failure
    /// caused by a validation exception provided as input.
    /// </summary>
    /// <param name="validationException">The exception that represents the details of the validation failure.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating a failed validation with the associated exception.</returns>
    public static ValidationResult CreateFromValidationFailure(ValidationException validationException)
    {
        ValidationResult result = new ValidationResult(false, validationException, null);
        return result;
    }

    /// <summary>
    /// Creates a <see cref="ValidationResult"/> representing a validation failure using the specified parameters.
    /// </summary>
    /// <param name="validator">The name of the validator that reported the failure.</param>
    /// <param name="message">The error message associated with the validation failure.</param>
    /// <param name="parameterName">The name of the parameter involved in the validation failure, or null if not applicable.</param>
    /// <param name="blackboard">The blackboard containing contextual data related to the validation, or null if not applicable.</param>
    /// <param name="context">A list of key-value pairs providing additional contextual information about the validation failure.</param>
    /// <returns>A <see cref="ValidationResult"/> constructed from the provided validation failure details.</returns>
    public static ValidationResult CreateFromValidationFailure(
        string validator, string message, string? parameterName, IBlackboard? blackboard,
        List<(string key, object? value)> context)
    {
        ValidationException exception = ValidationException.Create(validator, message, parameterName, blackboard, context);
        ValidationResult result = CreateFromValidationFailure(exception);
        return result;
    }

    /// <summary>
    /// Creates a <see cref="ValidationResult"/> instance representing a validation failure.
    /// This method utilizes validation details including the validator, error message,
    /// parameter name, additional blackboard data, and a validation context to construct
    /// a comprehensive validation failure result.
    /// </summary>
    /// <param name="validator">The name or identifier of the validator that triggered the failure.</param>
    /// <param name="message">The error message associated with the validation failure.</param>
    /// <param name="parameterName">The name of the parameter involved in the validation failure, if applicable.</param>
    /// <param name="blackboard">An optional blackboard instance containing additional data for validation.</param>
    /// <param name="context">The validation context providing additional information or metadata about the failure.</param>
    /// <returns>A <see cref="ValidationResult"/> instance encapsulating the validation failure details.</returns>
    public static ValidationResult CreateFromValidationFailure(
        string validator, string message, string? parameterName, IBlackboard? blackboard,
        ValidationContext context)
    {
        ValidationException exception = ValidationException.Create(validator, message, parameterName, blackboard, context);
        ValidationResult result = CreateFromValidationFailure(exception);
        return result;
    }

    /// <summary>
    /// Creates a validation result indicating a failure caused by a predicate exception.
    /// The validation process did not succeed due to a specific failure in the predicate logic.
    /// </summary>
    /// <param name="predicateException">The exception that represents the failure caused by predicate logic during validation.</param>
    /// <returns>A <see cref="ValidationResult"/> that represents the failed validation and includes details about the predicate exception.</returns>
    public static ValidationResult CreateFromPredicateFailure(PredicateException predicateException)
    {
        ValidationResult result = new ValidationResult(false, null, predicateException);
        return result;
    }
}
