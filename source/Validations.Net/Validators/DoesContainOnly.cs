using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a collection contains only the specified items.
/// </summary>
public static class DoesContainOnly
{
    private const string ValidatorName = nameof(DoesContainOnly);

    /// <summary>
    /// Checks if the collection contains only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="items">The items that the collection should contain only.</param>
    /// <returns>True if the collection contains only the specified items; otherwise, false.</returns>
    public static bool CheckDoesContainOnly<T>(IEnumerable<T>? value, params T?[]? items)
    {
        if (value == null || items == null || items.Length == 0)
            return false;

        var valueList = value.ToList();
        var itemsList = items.Where(x => x != null).ToList();

        if (valueList.Count != itemsList.Count)
            return false;

        foreach (var item in valueList)
        {
            if (!itemsList.Contains(item))
                return false;
        }

        return true;
    }

    /// <summary>
    /// Ensures that the collection contains only the specified items, throwing a ValidationException if it does not.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="items">The items that the collection should contain only.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the collection does not contain only the specified items.</exception>
    public static void EnsureDoesContainOnly<T>(IEnumerable<T>? value, IBlackboard? blackboard = null, params T?[]? items)
        where T : notnull
    {
        var isValid = CheckDoesContainOnly(value, items);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Items", items)
            };
            throw ValidationException.Create(ValidatorName, "Collection must contain only the specified items.", null,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the list contains only the specified items, throwing a ValidationException if it does not.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="value">The list to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="items">The items that the list should contain only.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the list does not contain only the specified items.</exception>
    public static void EnsureDoesContainOnly<T>(IList<T>? value, IBlackboard? blackboard = null, params T?[]? items)
        where T : notnull
    {
        var isValid = CheckDoesContainOnly(value, items);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Items", items)
            };
            throw ValidationException.Create(ValidatorName, "List must contain only the specified items.", null,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the list contains only the specified items, throwing a ValidationException if it does not.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="value">The list to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="items">The items that the list should contain only.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the list does not contain only the specified items.</exception>
    public static void EnsureDoesContainOnly<T>(List<T>? value, IBlackboard? blackboard = null, params T?[]? items)
        where T : notnull
    {
        var isValid = CheckDoesContainOnly(value, items);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Items", items)
            };
            throw ValidationException.Create(ValidatorName, "List must contain only the specified items.", null,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the array contains only the specified items, throwing a ValidationException if it does not.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <param name="value">The array to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="items">The items that the array should contain only.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the array does not contain only the specified items.</exception>
    public static void EnsureDoesContainOnly<T>(T[]? value, IBlackboard? blackboard = null, params T?[]? items)
        where T : notnull
    {
        var isValid = CheckDoesContainOnly(value, items);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Items", items)
            };
            throw ValidationException.Create(ValidatorName, "Array must contain only the specified items.", null,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the span contains only the specified items, throwing a ValidationException if it does not.
    /// </summary>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <param name="value">The span to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="items">The items that the span should contain only.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the span does not contain only the specified items.</exception>
    public static void EnsureDoesContainOnly<T>(ReadOnlySpan<T> value, IBlackboard? blackboard = null, params T?[]? items)
        where T : notnull
    {
        var isValid = CheckDoesContainOnly(value.ToArray(), items);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value.ToArray()),
                ("Items", items)
            };
            throw ValidationException.Create(ValidatorName, "Span must contain only the specified items.", null,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the span contains only the specified items, throwing a ValidationException if it does not.
    /// </summary>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <param name="value">The span to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="items">The items that the span should contain only.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the span does not contain only the specified items.</exception>
    public static void EnsureDoesContainOnly<T>(Span<T> value, IBlackboard? blackboard = null, params T?[]? items)
        where T : notnull
    {
        var isValid = CheckDoesContainOnly(value.ToArray(), items);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value.ToArray()),
                ("Items", items)
            };
            throw ValidationException.Create(ValidatorName, "Span must contain only the specified items.", null,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the memory contains only the specified items, throwing a ValidationException if it does not.
    /// </summary>
    /// <typeparam name="T">The type of elements in the memory.</typeparam>
    /// <param name="value">The memory to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="items">The items that the memory should contain only.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the memory does not contain only the specified items.</exception>
    public static void EnsureDoesContainOnly<T>(Memory<T> value, IBlackboard? blackboard = null, params T?[]? items)
        where T : notnull
    {
        var isValid = CheckDoesContainOnly(value.ToArray(), items);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value.ToArray()),
                ("Items", items)
            };
            throw ValidationException.Create(ValidatorName, "Memory must contain only the specified items.", null,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Validates that the collection contains only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="items">The items that the collection should contain only.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the collection contains only the specified items.</returns>
    public static ValidationResult ValidateDoesContainOnly<T>(IEnumerable<T>? value, IBlackboard? blackboard = null, params T?[]? items)
        where T : notnull
    {
        var isValid = CheckDoesContainOnly(value, items);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Items", items),
            ("ParameterName", null)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Collection must contain only the specified items.",
            null, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the list contains only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="value">The list to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="items">The items that the list should contain only.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the list contains only the specified items.</returns>
    public static ValidationResult ValidateDoesContainOnly<T>(IList<T>? value, IBlackboard? blackboard = null, params T?[]? items)
        where T : notnull
    {
        var isValid = CheckDoesContainOnly(value, items);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Items", items),
            ("ParameterName", null)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "List must contain only the specified items.",
            null, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the list contains only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="value">The list to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="items">The items that the list should contain only.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the list contains only the specified items.</returns>
    public static ValidationResult ValidateDoesContainOnly<T>(List<T>? value, IBlackboard? blackboard = null, params T?[]? items)
        where T : notnull
    {
        var isValid = CheckDoesContainOnly(value, items);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Items", items),
            ("ParameterName", null)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "List must contain only the specified items.",
            null, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the array contains only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <param name="value">The array to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="items">The items that the array should contain only.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the array contains only the specified items.</returns>
    public static ValidationResult ValidateDoesContainOnly<T>(T[]? value, IBlackboard? blackboard = null, params T?[]? items)
        where T : notnull
    {
        var isValid = CheckDoesContainOnly(value, items);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Items", items),
            ("ParameterName", null)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Array must contain only the specified items.",
            null, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the span contains only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <param name="value">The span to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="items">The items that the span should contain only.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the span contains only the specified items.</returns>
    public static ValidationResult ValidateDoesContainOnly<T>(ReadOnlySpan<T> value, IBlackboard? blackboard = null, params T?[]? items)
        where T : notnull
    {
        var isValid = CheckDoesContainOnly(value.ToArray(), items);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value.ToArray()),
            ("Items", items),
            ("ParameterName", null)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Span must contain only the specified items.",
            null, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the span contains only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <param name="value">The span to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="items">The items that the span should contain only.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the span contains only the specified items.</returns>
    public static ValidationResult ValidateDoesContainOnly<T>(Span<T> value, IBlackboard? blackboard = null, params T?[]? items)
        where T : notnull
    {
        var isValid = CheckDoesContainOnly(value.ToArray(), items);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value.ToArray()),
            ("Items", items),
            ("ParameterName", null)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Span must contain only the specified items.",
            null, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the memory contains only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the memory.</typeparam>
    /// <param name="value">The memory to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="items">The items that the memory should contain only.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the memory contains only the specified items.</returns>
    public static ValidationResult ValidateDoesContainOnly<T>(Memory<T> value, IBlackboard? blackboard = null, params T?[]? items)
        where T : notnull
    {
        var isValid = CheckDoesContainOnly(value.ToArray(), items);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("Value", value.ToArray()),
            ("Items", items),
            ("ParameterName", null)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Memory must contain only the specified items.",
            null, blackboard, contextList);
    }
}
