using SimpleBlackboard.Net;

namespace Validations.Net.ValidationSets;

/// <summary>
/// Wraps a validation step and overrides the failure message when the inner step fails.
/// </summary>
internal sealed class MessageOverrideValidationStep<T> : IValidationStep<T>, IAsyncValidationStep<T>
{
    private readonly IValidationStep<T> _inner;
    private readonly string _message;
    private readonly string? _memberPath;

    internal MessageOverrideValidationStep(IValidationStep<T> inner, string message, string? memberPath)
    {
        _inner = inner;
        _message = message;
        _memberPath = memberPath;
    }

    public ValidationResult Execute(T value, IBlackboard? blackboard = null)
    {
        var result = _inner.Execute(value, blackboard);
        return OverrideIfInvalid(result, blackboard);
    }

    public async Task<ValidationResult> ExecuteAsync(T value, IBlackboard? blackboard = null,
        CancellationToken cancellationToken = default)
    {
        var result = _inner is IAsyncValidationStep<T> asyncStep
            ? await asyncStep.ExecuteAsync(value, blackboard, cancellationToken).ConfigureAwait(false)
            : _inner.Execute(value, blackboard);
        return OverrideIfInvalid(result, blackboard);
    }

    private ValidationResult OverrideIfInvalid(ValidationResult result, IBlackboard? blackboard)
    {
        if (result.IsValid)
            return result;
        var validatorName = result.ValidationException?.Validator ?? "Validation";
        return ValidationResult.CreateFromValidationFailure(
            validatorName, _message, _memberPath, blackboard,
            result.ValidationException?.Context ?? new ValidationContext());
    }
}
