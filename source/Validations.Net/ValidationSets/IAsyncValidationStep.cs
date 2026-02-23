using SimpleBlackboard.Net;

namespace Validations.Net.ValidationSets;

/// <summary>
/// Async counterpart to <see cref="IValidationStep{T}"/>. Validation steps that need to perform
/// asynchronous work implement this interface. The async execution path on <see cref="ValidationSet{T}"/>
/// detects this interface at runtime and awaits it; synchronous steps are wrapped automatically.
/// </summary>
public interface IAsyncValidationStep<in T>
{
    /// <summary>
    /// Asynchronously executes this validation step against the given value.
    /// </summary>
    Task<ValidationResult> ExecuteAsync(T value, IBlackboard? blackboard = null,
        CancellationToken cancellationToken = default);
}
