using SimpleBlackboard.Net;

namespace Validations.Net.ValidationSets;

/// <summary>
/// Represents a single validation step that can be executed against a value.
/// </summary>
public interface IValidationStep<in T>
{
    /// <summary>
    /// Executes this validation step against the given value.
    /// </summary>
    ValidationResult Execute(T value, IBlackboard? blackboard = null);
}
