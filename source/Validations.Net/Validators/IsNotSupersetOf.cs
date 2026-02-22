using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The IsNotSupersetOf class provides methods for validation to ensure that
/// not all elements of a subset exist in the source collection. Includes functionality to check, enforce,
/// and validate instances where a non-superset relationship is required.
/// </summary>
public static class IsNotSupersetOf
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNotSupersetOf";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must not be a superset of the target";

    /// <summary>
    /// Checks if not all elements of subset exist in source.
    /// </summary>
    public static bool CheckIsNotSupersetOf<T>(this IEnumerable<T>? source, IEnumerable<T> subset)
    {
        return !source.CheckIsSupersetOf(subset);
    }

    /// <summary>
    /// Validates whether not all elements of subset exist in source.
    /// </summary>
    public static ValidationResult ValidateIsNotSupersetOf<T>(this IEnumerable<T>? source, IEnumerable<T> subset,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(source))] string? parameterName = null)
    {
        if (!source.CheckIsNotSupersetOf(subset))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", source), ("subset", subset)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures not all elements of subset exist in source, throwing an exception if validation fails.
    /// </summary>
    public static IEnumerable<T>? EnsureIsNotSupersetOf<T>(this IEnumerable<T>? source, IEnumerable<T> subset,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(source))] string? parameterName = null)
    {
        var validationResult = source.ValidateIsNotSupersetOf(subset, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return source;
    }
}
