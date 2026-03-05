using SimpleBlackboard.Net;
using Validations.Net.OLD.Validator;

namespace Validations.Net.OLD;

/// <summary>
/// Clean interface for types that can validate instances of <typeparamref name="T"/>.
/// Does not expose <see cref="IBlackboard"/> parameters for simpler DI registration and mocking.
/// For blackboard-aware validation, use <see cref="IValidateWithContext{T}"/>.
/// </summary>
public interface IValidate<T>
{
    ValidationResult Validate(T value);
    bool Check(T value);
    T Ensure(T value, string validationFailureMessage = "Validation failed");
    Task<ValidationResult> ValidateAsync(T value, CancellationToken cancellationToken = default);
    Task<bool> CheckAsync(T value, CancellationToken cancellationToken = default);
    Task<T> EnsureAsync(T value, string validationFailureMessage = "Validation failed",
        CancellationToken cancellationToken = default);
    ValidationSets.ValidationSet<T> Build();
}

/// <summary>
/// Extended interface that adds <see cref="IBlackboard"/>-aware overloads for cross-rule state sharing.
/// Implemented by <see cref="Validator{T}"/> and <see cref="AbstractValidator{T}"/>.
/// </summary>
public interface IValidateWithContext<T> : IValidate<T>
{
    ValidationResult Validate(T value, IBlackboard? blackboard);
    bool Check(T value, IBlackboard? blackboard);
    T Ensure(T value, IBlackboard? blackboard, string validationFailureMessage = "Validation failed");
    Task<ValidationResult> ValidateAsync(T value, IBlackboard? blackboard,
        CancellationToken cancellationToken = default);
    Task<bool> CheckAsync(T value, IBlackboard? blackboard,
        CancellationToken cancellationToken = default);
    Task<T> EnsureAsync(T value, IBlackboard? blackboard, string validationFailureMessage = "Validation failed",
        CancellationToken cancellationToken = default);
}
