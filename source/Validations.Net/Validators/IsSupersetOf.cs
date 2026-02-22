using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// The IsSupersetOf class provides methods for validation to ensure that
/// all elements of a subset exist in the source collection. Includes functionality to check, enforce,
/// and validate instances where superset relationships are required.
/// </summary>
public static class IsSupersetOf
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsSupersetOf";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must be a superset of the target";

    /// <summary>
    /// Checks if all elements of subset exist in source. Uses HashSet for performance.
    /// </summary>
    public static bool CheckIsSupersetOf<T>(this IEnumerable<T>? source, IEnumerable<T> subset)
    {
        if (source is null)
            return false;

        var sourceSet = source as HashSet<T> ?? new HashSet<T>(source);
        foreach (var item in subset)
        {
            if (!sourceSet.Contains(item))
                return false;
        }

        return true;
    }

    /// <summary>
    /// Validates whether all elements of subset exist in source.
    /// </summary>
    public static ValidationResult ValidateIsSupersetOf<T>(this IEnumerable<T>? source, IEnumerable<T> subset,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(source))] string? parameterName = null)
    {
        if (!source.CheckIsSupersetOf(subset))
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard,
                [("value", source), ("subset", subset)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures all elements of subset exist in source, throwing an exception if validation fails.
    /// </summary>
    public static IEnumerable<T>? EnsureIsSupersetOf<T>(this IEnumerable<T>? source, IEnumerable<T> subset,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(source))] string? parameterName = null)
    {
        var validationResult = source.ValidateIsSupersetOf(subset, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return source;
    }
}
