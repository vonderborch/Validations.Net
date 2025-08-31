using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a collection does not contain only the specified items.
/// </summary>
public static class DoesNotContainOnly
{
    private const string ValidatorName = nameof(DoesNotContainOnly);

    /// <summary>
    /// Checks if the collection does not contain only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="items">The items that the collection should not contain only.</param>
    /// <returns>True if the collection does not contain only the specified items; otherwise, false.</returns>
    public static bool CheckDoesNotContainOnly<T>(IEnumerable<T>? value, params T?[]? items)
    {
        return !DoesContainOnly.CheckDoesContainOnly(value, items);
    }

    /// <summary>
    /// Ensures that the collection does not contain only the specified items, throwing a ValidationException if it does.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="items">The items that the collection should not contain only.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the collection contains only the specified items.</exception>
    public static void EnsureDoesNotContainOnly<T>(IEnumerable<T>? value, IBlackboard? blackboard = null, params T?[]? items)
        where T : notnull
    {
        var isValid = CheckDoesNotContainOnly(value, items);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Items", items)
            };
            throw ValidationException.Create(ValidatorName, "Collection must not contain only the specified items.", null,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the list does not contain only the specified items, throwing a ValidationException if it does.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="value">The list to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="items">The items that the list should not contain only.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the list contains only the specified items.</exception>
    public static void EnsureDoesNotContainOnly<T>(IList<T>? value, IBlackboard? blackboard = null, params T?[]? items)
        where T : notnull
    {
        var isValid = CheckDoesNotContainOnly(value, items);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Items", items)
            };
            throw ValidationException.Create(ValidatorName, "List must not contain only the specified items.", null,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the list does not contain only the specified items, throwing a ValidationException if it does.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="value">The list to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="items">The items that the list should not contain only.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the list contains only the specified items.</exception>
    public static void EnsureDoesNotContainOnly<T>(List<T>? value, IBlackboard? blackboard = null, params T?[]? items)
        where T : notnull
    {
        var isValid = CheckDoesNotContainOnly(value, items);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Items", items)
            };
            throw ValidationException.Create(ValidatorName, "List must not contain only the specified items.", null,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the array does not contain only the specified items, throwing a ValidationException if it does.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <param name="value">The array to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="items">The items that the array should not contain only.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the array contains only the specified items.</exception>
    public static void EnsureDoesNotContainOnly<T>(T[]? value, IBlackboard? blackboard = null, params T?[]? items)
        where T : notnull
    {
        var isValid = CheckDoesNotContainOnly(value, items);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value),
                ("Items", items)
            };
            throw ValidationException.Create(ValidatorName, "Array must not contain only the specified items.", null,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the span does not contain only the specified items, throwing a ValidationException if it does.
    /// </summary>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <param name="value">The span to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="items">The items that the span should not contain only.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the span contains only the specified items.</exception>
    public static void EnsureDoesNotContainOnly<T>(ReadOnlySpan<T> value, IBlackboard? blackboard = null, params T?[]? items)
        where T : notnull
    {
        var isValid = CheckDoesNotContainOnly(value.ToArray(), items);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value.ToArray()),
                ("Items", items)
            };
            throw ValidationException.Create(ValidatorName, "Span must not contain only the specified items.", null,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the span does not contain only the specified items, throwing a ValidationException if it does.
    /// </summary>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <param name="value">The span to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="items">The items that the span should not contain only.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the span contains only the specified items.</exception>
    public static void EnsureDoesNotContainOnly<T>(Span<T> value, IBlackboard? blackboard = null, params T?[]? items)
        where T : notnull
    {
        var isValid = CheckDoesNotContainOnly(value.ToArray(), items);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value.ToArray()),
                ("Items", items)
            };
            throw ValidationException.Create(ValidatorName, "Span must not contain only the specified items.", null,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Ensures that the memory does not contain only the specified items, throwing a ValidationException if it does.
    /// </summary>
    /// <typeparam name="T">The type of elements in the memory.</typeparam>
    /// <param name="value">The memory to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="items">The items that the memory should not contain only.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the memory contains only the specified items.</exception>
    public static void EnsureDoesNotContainOnly<T>(Memory<T> value, IBlackboard? blackboard = null, params T?[]? items)
        where T : notnull
    {
        var isValid = CheckDoesNotContainOnly(value.ToArray(), items);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("Value", value.ToArray()),
                ("Items", items)
            };
            throw ValidationException.Create(ValidatorName, "Memory must not contain only the specified items.", null,
                blackboard, contextList);
        }
    }

    /// <summary>
    /// Validates that the collection does not contain only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="items">The items that the collection should not contain only.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the collection does not contain only the specified items.</returns>
    public static ValidationResult ValidateDoesNotContainOnly<T>(IEnumerable<T>? value, IBlackboard? blackboard = null, params T?[]? items)
        where T : notnull
    {
        var isValid = CheckDoesNotContainOnly(value, items);
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

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Collection must not contain only the specified items.",
            null, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the list does not contain only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="value">The list to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="items">The items that the list should not contain only.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the list does not contain only the specified items.</returns>
    public static ValidationResult ValidateDoesNotContainOnly<T>(IList<T>? value, IBlackboard? blackboard = null, params T?[]? items)
        where T : notnull
    {
        var isValid = CheckDoesNotContainOnly(value, items);
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

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "List must not contain only the specified items.",
            null, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the list does not contain only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="value">The list to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="items">The items that the list should not contain only.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the list does not contain only the specified items.</returns>
    public static ValidationResult ValidateDoesNotContainOnly<T>(List<T>? value, IBlackboard? blackboard = null, params T?[]? items)
        where T : notnull
    {
        var isValid = CheckDoesNotContainOnly(value, items);
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

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "List must not contain only the specified items.",
            null, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the array does not contain only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <param name="value">The array to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="items">The items that the array should not contain only.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the array does not contain only the specified items.</returns>
    public static ValidationResult ValidateDoesNotContainOnly<T>(T[]? value, IBlackboard? blackboard = null, params T?[]? items)
        where T : notnull
    {
        var isValid = CheckDoesNotContainOnly(value, items);
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

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Array must not contain only the specified items.",
            null, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the span does not contain only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <param name="value">The span to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="items">The items that the span should not contain only.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the span does not contain only the specified items.</returns>
    public static ValidationResult ValidateDoesNotContainOnly<T>(ReadOnlySpan<T> value, IBlackboard? blackboard = null, params T?[]? items)
        where T : notnull
    {
        var isValid = CheckDoesNotContainOnly(value.ToArray(), items);
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

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Span must not contain only the specified items.",
            null, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the span does not contain only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <param name="value">The span to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="items">The items that the span should not contain only.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the span does not contain only the specified items.</returns>
    public static ValidationResult ValidateDoesNotContainOnly<T>(Span<T> value, IBlackboard? blackboard = null, params T?[]? items)
        where T : notnull
    {
        var isValid = CheckDoesNotContainOnly(value.ToArray(), items);
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

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Span must not contain only the specified items.",
            null, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the memory does not contain only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the memory.</typeparam>
    /// <param name="value">The memory to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="items">The items that the memory should not contain only.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the memory does not contain only the specified items.</returns>
    public static ValidationResult ValidateDoesNotContainOnly<T>(Memory<T> value, IBlackboard? blackboard = null, params T?[]? items)
        where T : notnull
    {
        var isValid = CheckDoesNotContainOnly(value.ToArray(), items);
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

        return ValidationResult.CreateFromValidationFailure(ValidatorName, "Memory must not contain only the specified items.",
            null, blackboard, contextList);
    }
}
