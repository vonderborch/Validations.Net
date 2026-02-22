using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The IsNotSubsetOf class provides methods for validation to ensure that
/// not all elements of a source collection exist in a superset. Includes functionality to check, enforce,
/// and validate instances where a non-subset relationship is required.
/// </summary>
public static class IsNotSubsetOf
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNotSubsetOf";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must not be a subset of the target";

    /// <summary>
    /// Checks if not all elements of source exist in superset.
    /// </summary>
    public static bool CheckIsNotSubsetOf<T>(this IEnumerable<T>? source, IEnumerable<T> superset)
    {
        return !source.CheckIsSubsetOf(superset);
    }

    /// <summary>
    /// Validates whether not all elements of source exist in superset.
    /// </summary>
    public static ValidationResult ValidateIsNotSubsetOf<T>(this IEnumerable<T>? source, IEnumerable<T> superset,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(source))] string? parameterName = null)
    {
        if (!source.CheckIsNotSubsetOf(superset))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", source), ("superset", superset)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures not all elements of source exist in superset, throwing an exception if validation fails.
    /// </summary>
    public static IEnumerable<T>? EnsureIsNotSubsetOf<T>(this IEnumerable<T>? source, IEnumerable<T> superset,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(source))] string? parameterName = null)
    {
        var validationResult = source.ValidateIsNotSubsetOf(superset, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return source;
    }
}
