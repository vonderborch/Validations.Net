using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
/// Strongly-typed counterpart to <see cref="IValidator"/> that avoids the <c>object?</c> boxing/casting penalty.
/// Extends <see cref="IValidator"/> so implementations remain compatible with existing infrastructure.
/// </summary>
/// <typeparam name="T">The type of value this validator accepts.</typeparam>
public interface IValidator<in T> : IValidator
{
    /// <summary>
    /// Validates the given value and returns a result indicating success or failure.
    /// </summary>
    ValidationResult Validate(T value, string? memberName = null, IBlackboard? blackboard = null);
}

/// <summary>
/// Strongly-typed counterpart to <see cref="IAsyncValidator"/>.
/// </summary>
/// <typeparam name="T">The type of value this validator accepts.</typeparam>
public interface IAsyncValidator<in T> : IAsyncValidator
{
    /// <summary>
    /// Asynchronously validates the given value and returns a result indicating success or failure.
    /// </summary>
    Task<ValidationResult> ValidateAsync(T value, string? memberName = null,
        IBlackboard? blackboard = null, CancellationToken cancellationToken = default);
}
