using SimpleBlackboard.Net;

namespace Validations.Net.OLD;

/// <summary>
/// Base class for all validation attributes. Concrete implementations delegate to static validators
/// so validation logic lives in one place.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public abstract class ValidationAttribute(string name) : Attribute
{
    /// <summary>
    /// The name of the validator this attribute represents.
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Optional per-instance override for the failure message. When set, the attribute uses this
    /// instead of the validator's DefaultValidationFailureMessage.
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Controls whether a failure from this attribute is treated as a blocking error or a
    /// non-blocking warning. Defaults to <see cref="ValidationSeverity.Error"/>.
    /// </summary>
    public ValidationSeverity Severity { get; set; } = ValidationSeverity.Error;

    /// <summary>
    /// Validates the given value and returns a result indicating success or failure.
    /// </summary>
    /// <param name="value">The value to validate (member value or instance for class-level attributes).</param>
    /// <param name="memberName">The name of the member being validated, or null for class-level validation.</param>
    /// <param name="blackboard">Optional contextual data for the validation.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating the outcome.</returns>
    public abstract ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null);

    /// <summary>
    /// Asynchronously validates the given value. The default implementation wraps the synchronous
    /// <see cref="Validate"/> call in a completed task, so existing attributes work in async
    /// contexts without modification. Override to perform truly asynchronous validation.
    /// </summary>
    public virtual Task<ValidationResult> ValidateAsync(object? value, string? memberName = null,
        IBlackboard? blackboard = null, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Validate(value, memberName, blackboard));
    }
}
