using SimpleBlackboard.Net;

namespace Validations.Net;

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
    /// Validates the given value and returns a result indicating success or failure.
    /// </summary>
    /// <param name="value">The value to validate (member value or instance for class-level attributes).</param>
    /// <param name="memberName">The name of the member being validated, or null for class-level validation.</param>
    /// <param name="blackboard">Optional contextual data for the validation.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating the outcome.</returns>
    public abstract ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null);
}
