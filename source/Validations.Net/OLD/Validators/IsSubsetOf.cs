using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
/// The IsSubsetOf class provides methods for validation to ensure that
/// all elements of a source collection exist in a superset. Includes functionality to check, enforce,
/// and validate instances where subset relationships are required.
/// </summary>
public static class IsSubsetOf
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsSubsetOf";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must be a subset of the target";

    /// <summary>
    /// Checks if all elements of source exist in superset. Uses HashSet for performance.
    /// </summary>
    public static bool CheckIsSubsetOf<T>(this IEnumerable<T>? source, IEnumerable<T> superset)
    {
        ArgumentNullException.ThrowIfNull(superset);
        if (source is null)
            return false;

        var supersetSet = superset as HashSet<T> ?? new HashSet<T>(superset);
        foreach (var item in source)
        {
            if (!supersetSet.Contains(item))
                return false;
        }

        return true;
    }

    /// <summary>
    /// Validates whether all elements of source exist in superset.
    /// </summary>
    public static ValidationResult ValidateIsSubsetOf<T>(this IEnumerable<T>? source, IEnumerable<T> superset,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(source))] string? parameterName = null)
    {
        if (!source.CheckIsSubsetOf(superset))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", source), ("superset", superset)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures all elements of source exist in superset, throwing an exception if validation fails.
    /// </summary>
    public static IEnumerable<T>? EnsureIsSubsetOf<T>(this IEnumerable<T>? source, IEnumerable<T> superset,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(source))] string? parameterName = null)
    {
        var validationResult = source.ValidateIsSubsetOf(superset, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return source;
    }
}
