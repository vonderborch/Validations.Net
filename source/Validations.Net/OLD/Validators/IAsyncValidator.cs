using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
/// Async counterpart to <see cref="IValidator"/>. Validators that need to perform I/O-bound
/// work (database lookups, API calls, etc.) implement this interface alongside or instead of
/// <see cref="IValidator"/>. The async runner and attribute pipeline detect this interface
/// at runtime and await it; synchronous validators are wrapped in completed tasks automatically.
/// </summary>
public interface IAsyncValidator
{
    /// <summary>
    /// Asynchronously validates the given value and returns a result indicating success or failure.
    /// </summary>
    Task<ValidationResult> ValidateAsync(object? value, string? memberName = null,
        IBlackboard? blackboard = null, CancellationToken cancellationToken = default);
}
