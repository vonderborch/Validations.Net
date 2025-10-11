namespace Validations.Net.OLD;

/// <summary>
/// Represents the result of a validation process, including whether the validation succeeded
/// and any exception details if it failed.
/// </summary>
public struct ValidationResult
{
    /// <summary>
    /// Represents the result of a validation process, encapsulating the status of the validation
    /// and any relevant exception details in case of validation failure.
    /// </summary>
    public ValidationResult()
    {
        this.IsValid = true;
        this.ValidationException = null;
        this.ExceptionMessage = null;
    }

    /// <summary>
    /// Represents the outcome of a validation, indicating whether it succeeded or failed,
    /// along with relevant exception information when applicable.
    /// </summary>
    public ValidationResult(ValidationException exception)
    {
        this.IsValid = false;
        this.ValidationException = exception;
        this.ExceptionMessage = exception.Message;
    }
    
    /// <summary>
    /// Gets a value indicating whether the validation was successful.
    /// </summary>
    /// <remarks>
    /// This property returns true if the validation was successful, and false otherwise.
    /// The result of this property represents the overall success or failure of the validation process.
    /// </remarks>
    public bool IsValid { get; }

    /// <summary>
    /// Gets the validation exception associated with the validation result, if any.
    /// </summary>
    /// <remarks>
    /// This property provides access to the <see cref="ValidationException"/> instance that represents
    /// the exception encountered during validation, if the validation failed and an exception was generated.
    /// If the validation succeeded or no exception is available, this property will return null.
    /// </remarks>
    public ValidationException? ValidationException { get; }

    /// <summary>
    /// Gets the exception message associated with a validation failure, if one exists.
    /// </summary>
    /// <remarks>
    /// This property provides details about the exception message that may have occurred during
    /// the validation process. It is typically null if the validation was successful.
    /// </remarks>
    public string? ExceptionMessage { get; }
}
