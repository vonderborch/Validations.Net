using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Base class for strongly-typed validators. Implements <see cref="IValidator{T}"/> and
/// bridges to the untyped <see cref="IValidator"/> interface via cast-and-delegate.
/// Subclasses only need to implement the typed <see cref="Validate(T,string?,IBlackboard?)"/> method.
/// </summary>
/// <typeparam name="T">The type of value this validator accepts.</typeparam>
public abstract class ValidatorBase<T> : IValidator<T>
{
    public abstract string Name { get; }
    public abstract string DefaultFailureMessage { get; }

    public abstract ValidationResult Validate(T value, string? memberName = null,
        IBlackboard? blackboard = null);

    ValidationResult IValidator.Validate(object? value, string? memberName, IBlackboard? blackboard)
        => Validate((T)value!, memberName, blackboard);
}
