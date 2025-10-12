using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a collection is not sorted.
/// </summary>
public static class IsNotSorted
{
    /// <summary>
    /// Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNotSorted";

    /// <summary>
    /// Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must not be sorted";

    /// <summary>
    /// Checks if the specified collection is not sorted in ascending order.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <returns>True if the collection is not sorted in ascending order; otherwise, false.</returns>
    public static bool CheckIsNotSorted<T>(IEnumerable<T>? value) where T : IComparable<T>
    {
        return !IsSorted.CheckIsSorted(value);
    }

    /// <summary>
    /// Checks if the specified collection is not sorted in the specified order.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="ascending">True to check for ascending order; false for descending order.</param>
    /// <returns>True if the collection is not sorted in the specified order; otherwise, false.</returns>
    public static bool CheckIsNotSorted<T>(IEnumerable<T>? value, bool ascending) where T : IComparable<T>
    {
        return !IsSorted.CheckIsSorted(value, ascending);
    }

    /// <summary>
    /// Ensures that the specified collection is not sorted in ascending order, throwing a ValidationException if it is.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the collection is sorted in ascending order.</exception>
    public static void EnsureIsNotSorted<T>(IEnumerable<T>? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IComparable<T>
    {
        var isValid = CheckIsNotSorted(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Count", value?.Count() ?? 0)
            };
            throw ValidationException.Create(ValidatorName, "Collection must not be sorted in ascending order.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified collection is not sorted in the specified order, throwing a ValidationException if it is.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="ascending">True to check for ascending order; false for descending order.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the collection is sorted in the specified order.</exception>
    public static void EnsureIsNotSorted<T>(IEnumerable<T>? value, bool ascending, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IComparable<T>
    {
        var isValid = CheckIsNotSorted(value, ascending);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Count", value?.Count() ?? 0),
                ("Ascending", ascending)
            };
            var orderText = ascending ? "ascending" : "descending";
            throw ValidationException.Create(ValidatorName, $"Collection must not be sorted in {orderText} order.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified array is not sorted in ascending order, throwing a ValidationException if it is.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <param name="value">The array to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the array is sorted in ascending order.</exception>
    public static void EnsureIsNotSorted<T>(T[]? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IComparable<T>
    {
        var isValid = CheckIsNotSorted(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Length", value?.Length ?? 0)
            };
            throw ValidationException.Create(ValidatorName, "Array must not be sorted in ascending order.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified list is not sorted in ascending order, throwing a ValidationException if it is.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="value">The list to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the list is sorted in ascending order.</exception>
    public static void EnsureIsNotSorted<T>(IList<T>? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IComparable<T>
    {
        var isValid = CheckIsNotSorted(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Count", value?.Count ?? 0)
            };
            throw ValidationException.Create(ValidatorName, "List must not be sorted in ascending order.", parameterName,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Validates if the specified collection is not sorted in ascending order and returns a ValidationResult.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the collection is not sorted in ascending order.</returns>
    public static ValidationResult ValidateIsNotSorted<T>(IEnumerable<T>? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IComparable<T>
    {
        var isValid = CheckIsNotSorted(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Count", value?.Count() ?? 0),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage,
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates if the specified collection is not sorted in the specified order and returns a ValidationResult.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="ascending">True to check for ascending order; false for descending order.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the collection is not sorted in the specified order.</returns>
    public static ValidationResult ValidateIsNotSorted<T>(IEnumerable<T>? value, bool ascending, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IComparable<T>
    {
        var isValid = CheckIsNotSorted(value, ascending);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Count", value?.Count() ?? 0),
            ("Ascending", ascending),
            ("ParameterName", parameterName)
        };

        var orderText = ascending ? "ascending" : "descending";
        return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage,
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates if the specified array is not sorted in ascending order and returns a ValidationResult.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <param name="value">The array to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the array is not sorted in ascending order.</returns>
    public static ValidationResult ValidateIsNotSorted<T>(T[]? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IComparable<T>
    {
        var isValid = CheckIsNotSorted(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Length", value?.Length ?? 0),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage,
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates if the specified list is not sorted in ascending order and returns a ValidationResult.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="value">The list to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the list is not sorted in ascending order.</returns>
    public static ValidationResult ValidateIsNotSorted<T>(IList<T>? value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IComparable<T>
    {
        var isValid = CheckIsNotSorted(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Count", value?.Count ?? 0),
            ("ParameterName", parameterName)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage,
            parameterName, blackboard, contextList);
    }
}
