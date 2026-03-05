using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
/// Shared abstraction for all validators. Both ValidationAttributes and the ValidationSetBuilder
/// delegate to IValidator implementations, which in turn call the static extension methods.
/// The object? parameter on Validate exists because the attribute/reflection pipeline provides
/// property values as object via PropertyInfo.GetValue.
/// </summary>
public interface IValidator
{
    /// <summary>
    /// The unique name identifying this validator (e.g. "IsNotNull", "IsInRange").
    /// </summary>
    string Name { get; }

    /// <summary>
    /// The default message used when validation fails and no override is provided.
    /// </summary>
    string DefaultFailureMessage { get; }

    /// <summary>
    /// Validates the given value and returns a result indicating success or failure.
    /// </summary>
    ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null);
}
