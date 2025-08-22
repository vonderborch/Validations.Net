using System;
using System.Collections.Generic;
using System.Linq;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a collection is NOT sorted.
/// </summary>
public static class IsNotSorted
{
    private const string ValidatorName = nameof(IsNotSorted);

    /// <summary>
    /// Checks if the specified collection is NOT sorted in ascending order.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <returns>True if the collection is NOT sorted in ascending order; otherwise, false.</returns>
    public static bool CheckIsNotSorted<T>(this IEnumerable<T>? value) where T : IComparable<T>
    {
        return !IsSorted.CheckIsSorted(value);
    }

    /// <summary>
    /// Checks if the specified collection is NOT sorted in the specified order.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="ascending">True to check for ascending order; false for descending order.</param>
    /// <returns>True if the collection is NOT sorted in the specified order; otherwise, false.</returns>
    public static bool CheckIsNotSorted<T>(this IEnumerable<T>? value, bool ascending) where T : IComparable<T>
    {
        return !IsSorted.CheckIsSorted(value, ascending);
    }

    /// <summary>
    /// Checks if the specified collection is NOT sorted using the provided comparer.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="comparer">The comparer to use for sorting comparison.</param>
    /// <returns>True if the collection is NOT sorted using the specified comparer; otherwise, false.</returns>
    public static bool CheckIsNotSorted<T>(this IEnumerable<T>? value, IComparer<T> comparer)
    {
        return !IsSorted.CheckIsSorted(value, comparer);
    }

    /// <summary>
    /// Checks if the specified array is NOT sorted in ascending order.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <param name="value">The array to check.</param>
    /// <returns>True if the array is NOT sorted in ascending order; otherwise, false.</returns>
    public static bool CheckIsNotSorted<T>(this T[]? value) where T : IComparable<T>
    {
        return !IsSorted.CheckIsSorted(value);
    }

    /// <summary>
    /// Checks if the specified list is NOT sorted in ascending order.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="value">The list to check.</param>
    /// <returns>True if the list is NOT sorted in ascending order; otherwise, false.</returns>
    public static bool CheckIsNotSorted<T>(this IList<T>? value) where T : IComparable<T>
    {
        return !IsSorted.CheckIsSorted(value);
    }

    /// <summary>
    /// Validates if the specified collection is NOT sorted in ascending order and returns a ValidationResult.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the collection is NOT sorted in ascending order.</returns>
    public static ValidationResult ValidateIsNotSorted<T>(this IEnumerable<T>? value, string? fieldName = null, IBlackboard? blackboard = null) where T : IComparable<T>
    {
        var isValid = CheckIsNotSorted(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Count", value?.Count() ?? 0)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "Collection must NOT be sorted in ascending order.", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates if the specified collection is NOT sorted in the specified order and returns a ValidationResult.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="ascending">True to check for ascending order; false for descending order.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the collection is NOT sorted in the specified order.</returns>
    public static ValidationResult ValidateIsNotSorted<T>(this IEnumerable<T>? value, bool ascending, string? fieldName = null, IBlackboard? blackboard = null) where T : IComparable<T>
    {
        var isValid = CheckIsNotSorted(value, ascending);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Count", value?.Count() ?? 0),
            ("Ascending", ascending)
        };

        var orderText = ascending ? "ascending" : "descending";
        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, $"Collection must NOT be sorted in {orderText} order.", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates if the specified array is NOT sorted in ascending order and returns a ValidationResult.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <param name="value">The array to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the array is NOT sorted in ascending order.</returns>
    public static ValidationResult ValidateIsNotSorted<T>(this T[]? value, string? fieldName = null, IBlackboard? blackboard = null) where T : IComparable<T>
    {
        var isValid = CheckIsNotSorted(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Length", value?.Length ?? 0)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "Array must NOT be sorted in ascending order.", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates if the specified list is NOT sorted in ascending order and returns a ValidationResult.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="value">The list to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the list is NOT sorted in ascending order.</returns>
    public static ValidationResult ValidateIsNotSorted<T>(this IList<T>? value, string? fieldName = null, IBlackboard? blackboard = null) where T : IComparable<T>
    {
        var isValid = CheckIsNotSorted(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Count", value?.Count ?? 0)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "List must NOT be sorted in ascending order.", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Ensures that the specified collection is NOT sorted in ascending order, throwing a ValidationException if it is.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the collection is sorted in ascending order.</exception>
    public static void EnsureIsNotSorted<T>(this IEnumerable<T>? value, string? fieldName = null, IBlackboard? blackboard = null) where T : IComparable<T>
    {
        var isValid = CheckIsNotSorted(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Count", value?.Count() ?? 0)
            };
            throw ValidationException.Create(ValidatorName, "Collection must NOT be sorted in ascending order.", fieldName, blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified collection is NOT sorted in the specified order, throwing a ValidationException if it is.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="ascending">True to check for ascending order; false for descending order.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the collection is sorted in the specified order.</exception>
    public static void EnsureIsNotSorted<T>(this IEnumerable<T>? value, bool ascending, string? fieldName = null, IBlackboard? blackboard = null) where T : IComparable<T>
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
            throw ValidationException.Create(ValidatorName, $"Collection must NOT be sorted in {orderText} order.", fieldName, blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified array is NOT sorted in ascending order, throwing a ValidationException if it is.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <param name="value">The array to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the array is sorted in ascending order.</exception>
    public static void EnsureIsNotSorted<T>(this T[]? value, string? fieldName = null, IBlackboard? blackboard = null) where T : IComparable<T>
    {
        var isValid = CheckIsNotSorted(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Length", value?.Length ?? 0)
            };
            throw ValidationException.Create(ValidatorName, "Array must NOT be sorted in ascending order.", fieldName, blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified list is NOT sorted in ascending order, throwing a ValidationException if it is.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="value">The list to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the list is sorted in ascending order.</exception>
    public static void EnsureIsNotSorted<T>(this IList<T>? value, string? fieldName = null, IBlackboard? blackboard = null) where T : IComparable<T>
    {
        var isValid = CheckIsNotSorted(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Count", value?.Count ?? 0)
            };
            throw ValidationException.Create(ValidatorName, "List must NOT be sorted in ascending order.", fieldName, blackboard, contextList);
        }
    }
}
