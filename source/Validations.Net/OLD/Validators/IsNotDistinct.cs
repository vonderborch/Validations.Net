using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
/// The IsNotDistinct class provides methods for validation to ensure that
/// a sequence contains at least one duplicate. Includes functionality to check, enforce,
/// and validate instances where duplicates are required.
/// </summary>
public static class IsNotDistinct
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNotDistinct";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must contain duplicate elements";

    /// <summary>
    /// Checks if the given non-generic enumerable contains at least one duplicate (non-generic overload for boxed values).
    /// </summary>
    public static bool CheckIsNotDistinct(this System.Collections.IEnumerable? enumerable)
    {
        if (enumerable is null) return false;
        return !enumerable.CheckIsDistinct();
    }

    /// <summary>
    /// Checks if the given enumerable contains at least one duplicate.
    /// </summary>
    /// <param name="enumerable">The enumerable to check.</param>
    /// <returns>True if there is at least one duplicate; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotDistinct<T>(this IEnumerable<T>? enumerable)
    {
        return !enumerable.CheckIsDistinct();
    }

    /// <summary>
    /// Validates whether the given enumerable contains at least one duplicate.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotDistinct<T>(this IEnumerable<T>? enumerable, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(enumerable))] string? parameterName = null)
    {
        if (!enumerable.CheckIsNotDistinct())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", enumerable)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given enumerable contains at least one duplicate, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T>? EnsureIsNotDistinct<T>(this IEnumerable<T>? enumerable, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(enumerable))] string? parameterName = null)
    {
        var validationResult = enumerable.ValidateIsNotDistinct(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return enumerable;
    }
}

public sealed class NotDistinctValidator : IValidator
{
    public static readonly NotDistinctValidator Instance = new();
    public string Name => IsNotDistinct.ValidatorName;
    public string DefaultFailureMessage => IsNotDistinct.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        return value switch
        {
            null => ValidationResult.CreateFromValidationFailure(this.Name, this.DefaultFailureMessage, memberName, blackboard, [("value", value)]),
            System.Collections.IEnumerable e when value is not string => e.Cast<object?>().ValidateIsNotDistinct(blackboard, this.DefaultFailureMessage, memberName),
            _ => ValidationResult.CreateFromValidationFailure(this.Name, this.DefaultFailureMessage, memberName, blackboard, [("value", value)])
        };
    }
}

public sealed class ValidateIsNotDistinctAttribute() : ValidatorAttribute(NotDistinctValidator.Instance);
