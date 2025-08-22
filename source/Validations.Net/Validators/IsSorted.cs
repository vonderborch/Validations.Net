using System;
using System.Collections.Generic;
using System.Linq;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a collection is sorted.
/// </summary>
public static class IsSorted
{
    private const string ValidatorName = nameof(IsSorted);

    /// <summary>
    /// Checks if the specified collection is sorted in ascending order.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <returns>True if the collection is sorted in ascending order; otherwise, false.</returns>
    public static bool CheckIsSorted<T>(this IEnumerable<T>? value) where T : IComparable<T>
    {
        if (value == null) return false;
        
        var previous = default(T);
        var first = true;
        
        foreach (var item in value)
        {
            if (first)
            {
                previous = item;
                first = false;
                continue;
            }
            
            if (previous != null && item != null && previous.CompareTo(item) > 0)
                return false;
                
            previous = item;
        }
        
        return true;
    }

    /// <summary>
    /// Checks if the specified collection is sorted in the specified order.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="ascending">True to check for ascending order; false for descending order.</param>
    /// <returns>True if the collection is sorted in the specified order; otherwise, false.</returns>
    public static bool CheckIsSorted<T>(this IEnumerable<T>? value, bool ascending) where T : IComparable<T>
    {
        if (value == null) return false;
        
        var previous = default(T);
        var first = true;
        
        foreach (var item in value)
        {
            if (first)
            {
                previous = item;
                first = false;
                continue;
            }
            
            if (previous != null && item != null)
            {
                var comparison = previous.CompareTo(item);
                if (ascending && comparison > 0) return false;
                if (!ascending && comparison < 0) return false;
            }
                
            previous = item;
        }
        
        return true;
    }

    /// <summary>
    /// Checks if the specified collection is sorted using the provided comparer.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="comparer">The comparer to use for sorting comparison.</param>
    /// <returns>True if the collection is sorted using the specified comparer; otherwise, false.</returns>
    public static bool CheckIsSorted<T>(this IEnumerable<T>? value, IComparer<T> comparer)
    {
        if (value == null || comparer == null) return false;
        
        var previous = default(T);
        var first = true;
        
        foreach (var item in value)
        {
            if (first)
            {
                previous = item;
                first = false;
                continue;
            }
            
            if (comparer.Compare(previous, item) > 0)
                return false;
                
            previous = item;
        }
        
        return true;
    }

    /// <summary>
    /// Checks if the specified array is sorted in ascending order.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <param name="value">The array to check.</param>
    /// <returns>True if the array is sorted in ascending order; otherwise, false.</returns>
    public static bool CheckIsSorted<T>(this T[]? value) where T : IComparable<T>
    {
        if (value == null) return false;
        
        for (int i = 1; i < value.Length; i++)
        {
            if (value[i - 1] != null && value[i] != null && value[i - 1].CompareTo(value[i]) > 0)
                return false;
        }
        
        return true;
    }

    /// <summary>
    /// Checks if the specified list is sorted in ascending order.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="value">The list to check.</param>
    /// <returns>True if the list is sorted in ascending order; otherwise, false.</returns>
    public static bool CheckIsSorted<T>(this IList<T>? value) where T : IComparable<T>
    {
        if (value == null) return false;
        
        for (int i = 1; i < value.Count; i++)
        {
            if (value[i - 1] != null && value[i] != null && value[i - 1].CompareTo(value[i]) > 0)
                return false;
        }
        
        return true;
    }

    /// <summary>
    /// Validates if the specified collection is sorted in ascending order and returns a ValidationResult.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the collection is sorted in ascending order.</returns>
    public static ValidationResult ValidateIsSorted<T>(this IEnumerable<T>? value, string? fieldName = null, IBlackboard? blackboard = null) where T : IComparable<T>
    {
        var isValid = CheckIsSorted(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Count", value?.Count() ?? 0)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "Collection must be sorted in ascending order.", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates if the specified collection is sorted in the specified order and returns a ValidationResult.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="ascending">True to check for ascending order; false for descending order.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the collection is sorted in the specified order.</returns>
    public static ValidationResult ValidateIsSorted<T>(this IEnumerable<T>? value, bool ascending, string? fieldName = null, IBlackboard? blackboard = null) where T : IComparable<T>
    {
        var isValid = CheckIsSorted(value, ascending);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Count", value?.Count() ?? 0),
            ("Ascending", ascending)
        };

        var orderText = ascending ? "ascending" : "descending";
        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, $"Collection must be sorted in {orderText} order.", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates if the specified array is sorted in ascending order and returns a ValidationResult.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <param name="value">The array to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the array is sorted in ascending order.</returns>
    public static ValidationResult ValidateIsSorted<T>(this T[]? value, string? fieldName = null, IBlackboard? blackboard = null) where T : IComparable<T>
    {
        var isValid = CheckIsSorted(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Length", value?.Length ?? 0)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "Array must be sorted in ascending order.", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates if the specified list is sorted in ascending order and returns a ValidationResult.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="value">The list to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating whether the list is sorted in ascending order.</returns>
    public static ValidationResult ValidateIsSorted<T>(this IList<T>? value, string? fieldName = null, IBlackboard? blackboard = null) where T : IComparable<T>
    {
        var isValid = CheckIsSorted(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Count", value?.Count ?? 0)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "List must be sorted in ascending order.", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Ensures that the specified collection is sorted in ascending order, throwing a ValidationException if it is not.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the collection is not sorted in ascending order.</exception>
    public static void EnsureIsSorted<T>(this IEnumerable<T>? value, string? fieldName = null, IBlackboard? blackboard = null) where T : IComparable<T>
    {
        var isValid = CheckIsSorted(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Count", value?.Count() ?? 0)
            };
            throw ValidationException.Create(ValidatorName, "Collection must be sorted in ascending order.", fieldName, blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified collection is sorted in the specified order, throwing a ValidationException if it is not.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="ascending">True to check for ascending order; false for descending order.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the collection is not sorted in the specified order.</exception>
    public static void EnsureIsSorted<T>(this IEnumerable<T>? value, bool ascending, string? fieldName = null, IBlackboard? blackboard = null) where T : IComparable<T>
    {
        var isValid = CheckIsSorted(value, ascending);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Count", value?.Count() ?? 0),
                ("Ascending", ascending)
            };
            var orderText = ascending ? "ascending" : "descending";
            throw ValidationException.Create(ValidatorName, $"Collection must be sorted in {orderText} order.", fieldName, blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified array is sorted in ascending order, throwing a ValidationException if it is not.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <param name="value">The array to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the array is not sorted in ascending order.</exception>
    public static void EnsureIsSorted<T>(this T[]? value, string? fieldName = null, IBlackboard? blackboard = null) where T : IComparable<T>
    {
        var isValid = CheckIsSorted(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Length", value?.Length ?? 0)
            };
            throw ValidationException.Create(ValidatorName, "Array must be sorted in ascending order.", fieldName, blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified list is sorted in ascending order, throwing a ValidationException if it is not.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="value">The list to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the list is not sorted in ascending order.</exception>
    public static void EnsureIsSorted<T>(this IList<T>? value, string? fieldName = null, IBlackboard? blackboard = null) where T : IComparable<T>
    {
        var isValid = CheckIsSorted(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Count", value?.Count ?? 0)
            };
            throw ValidationException.Create(ValidatorName, "List must be sorted in ascending order.", fieldName, blackboard, contextList);
        }
    }
}
