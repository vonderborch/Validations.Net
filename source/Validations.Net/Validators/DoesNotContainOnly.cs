using System;
using System.Collections.Generic;
using System.Linq;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Validates that a collection does not contain only the specified items.
/// </summary>
public static class DoesNotContainOnly
{
    private const string ValidatorName = nameof(DoesNotContainOnly);

    /// <summary>
    /// Checks if the collection does not contain only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should not contain exclusively.</param>
    /// <returns>True if the collection does not contain only the specified items; otherwise, false.</returns>
    public static bool CheckDoesNotContainOnly<T>(this IEnumerable<T>? value, params T?[] items)
    {
        return !DoesContainOnly.CheckDoesContainOnly(value, items);
    }

    /// <summary>
    /// Checks if the collection does not contain only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should not contain exclusively.</param>
    /// <returns>True if the collection does not contain only the specified items; otherwise, false.</returns>
    public static bool CheckDoesNotContainOnly<T>(this IList<T>? value, params T?[] items)
    {
        return !DoesContainOnly.CheckDoesContainOnly(value, items);
    }

    /// <summary>
    /// Checks if the collection does not contain only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should not contain exclusively.</param>
    /// <returns>True if the collection does not contain only the specified items; otherwise, false.</returns>
    public static bool CheckDoesNotContainOnly<T>(this List<T>? value, params T?[] items)
    {
        return !DoesContainOnly.CheckDoesContainOnly(value, items);
    }

    /// <summary>
    /// Checks if the collection does not contain only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should not contain exclusively.</param>
    /// <returns>True if the collection does not contain only the specified items; otherwise, false.</returns>
    public static bool CheckDoesNotContainOnly<T>(this T[]? value, params T?[] items)
    {
        return !DoesContainOnly.CheckDoesContainOnly(value, items);
    }

    /// <summary>
    /// Checks if the collection does not contain only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should not contain exclusively.</param>
    /// <returns>True if the collection does not contain only the specified items; otherwise, false.</returns>
    public static bool CheckDoesNotContainOnly<T>(this ReadOnlySpan<T> value, params T?[] items)
    {
        return !DoesContainOnly.CheckDoesContainOnly(value, items);
    }

    /// <summary>
    /// Checks if the collection does not contain only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should not contain exclusively.</param>
    /// <returns>True if the collection does not contain only the specified items; otherwise, false.</returns>
    public static bool CheckDoesNotContainOnly<T>(this Span<T> value, params T?[] items)
    {
        return !DoesContainOnly.CheckDoesContainOnly(value, items);
    }

    /// <summary>
    /// Checks if the collection does not contain only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should not contain exclusively.</param>
    /// <returns>True if the collection does not contain only the specified items; otherwise, false.</returns>
    public static bool CheckDoesNotContainOnly<T>(this Memory<T> value, params T?[] items)
    {
        return !DoesContainOnly.CheckDoesContainOnly(value, items);
    }

    /// <summary>
    /// Validates that the collection does not contain only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should not contain exclusively.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateDoesNotContainOnly<T>(this IEnumerable<T>? value, string fieldName, IBlackboard? blackboard, params T?[] items)
    {
        var isValid = CheckDoesNotContainOnly(value, items);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Items", items),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, $"The collection must not contain only the specified items: [{string.Join(", ", items ?? Array.Empty<T>())}]", fieldName, blackboard, contextList);
    }

    /// <summary>
    /// Validates that the collection does not contain only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should not contain exclusively.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateDoesNotContainOnly<T>(this IList<T>? value, string fieldName, IBlackboard? blackboard, params T?[] items)
    {
        return ValidateDoesNotContainOnly((IEnumerable<T>?)value, fieldName, blackboard, items);
    }

    /// <summary>
    /// Validates that the collection does not contain only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should not contain exclusively.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateDoesNotContainOnly<T>(this List<T>? value, string fieldName, IBlackboard? blackboard, params T?[] items)
    {
        return ValidateDoesNotContainOnly((IEnumerable<T>?)value, fieldName, blackboard, items);
    }

    /// <summary>
    /// Validates that the collection does not contain only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should not contain exclusively.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateDoesNotContainOnly<T>(this T[]? value, string fieldName, IBlackboard? blackboard, params T?[] items)
    {
        return ValidateDoesNotContainOnly((IEnumerable<T>?)value, fieldName, blackboard, items);
    }

    /// <summary>
    /// Validates that the collection does not contain only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should not contain exclusively.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateDoesNotContainOnly<T>(this ReadOnlySpan<T> value, string fieldName, IBlackboard? blackboard, params T?[] items)
    {
        return ValidateDoesNotContainOnly(value.ToArray(), fieldName, blackboard, items);
    }

    /// <summary>
    /// Validates that the collection does not contain only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should not contain exclusively.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateDoesNotContainOnly<T>(this Span<T> value, string fieldName, IBlackboard? blackboard, params T?[] items)
    {
        return ValidateDoesNotContainOnly((ReadOnlySpan<T>)value, fieldName, blackboard, items);
    }

    /// <summary>
    /// Validates that the collection does not contain only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should not contain exclusively.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateDoesNotContainOnly<T>(this Memory<T> value, string fieldName, IBlackboard? blackboard, params T?[] items)
    {
        return ValidateDoesNotContainOnly(value.Span, fieldName, blackboard, items);
    }

    /// <summary>
    /// Ensures that the collection does not contain only the specified items, throwing an exception if validation fails.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should not contain exclusively.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the collection contains only the specified items.</exception>
    public static void EnsureDoesNotContainOnly<T>(this IEnumerable<T>? value, string fieldName, IBlackboard? blackboard, params T?[] items)
    {
        var isValid = CheckDoesNotContainOnly(value, items);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Items", items),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, $"The collection must not contain only the specified items: [{string.Join(", ", items ?? Array.Empty<T>())}]", null, null, contextList);
        }
    }

    /// <summary>
    /// Ensures that the collection does not contain only the specified items, throwing an exception if validation fails.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should not contain exclusively.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the collection contains only the specified items.</exception>
    public static void EnsureDoesNotContainOnly<T>(this IList<T>? value, string fieldName, IBlackboard? blackboard, params T?[] items)
    {
        EnsureDoesNotContainOnly((IEnumerable<T>?)value, fieldName, blackboard, items);
    }

    /// <summary>
    /// Ensures that the collection does not contain only the specified items, throwing an exception if validation fails.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should not contain exclusively.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the collection contains only the specified items.</exception>
    public static void EnsureDoesNotContainOnly<T>(this List<T>? value, string fieldName, IBlackboard? blackboard, params T?[] items)
    {
        EnsureDoesNotContainOnly((IEnumerable<T>?)value, fieldName, blackboard, items);
    }

    /// <summary>
    /// Ensures that the collection does not contain only the specified items, throwing an exception if validation fails.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should not contain exclusively.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the collection contains only the specified items.</exception>
    public static void EnsureDoesNotContainOnly<T>(this T[]? value, string fieldName, IBlackboard? blackboard, params T?[] items)
    {
        EnsureDoesNotContainOnly((IEnumerable<T>?)value, fieldName, blackboard, items);
    }

    /// <summary>
    /// Ensures that the collection does not contain only the specified items, throwing an exception if validation fails.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should not contain exclusively.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the collection contains only the specified items.</exception>
    public static void EnsureDoesNotContainOnly<T>(this ReadOnlySpan<T> value, string fieldName, IBlackboard? blackboard, params T?[] items)
    {
        EnsureDoesNotContainOnly(value.ToArray(), fieldName, blackboard, items);
    }

    /// <summary>
    /// Ensures that the collection does not contain only the specified items, throwing an exception if validation fails.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should not contain exclusively.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the collection contains only the specified items.</exception>
    public static void EnsureDoesNotContainOnly<T>(this Span<T> value, string fieldName, IBlackboard? blackboard, params T?[] items)
    {
        EnsureDoesNotContainOnly((ReadOnlySpan<T>)value, fieldName, blackboard, items);
    }

    /// <summary>
    /// Ensures that the collection does not contain only the specified items, throwing an exception if validation fails.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should not contain exclusively.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the collection contains only the specified items.</exception>
    public static void EnsureDoesNotContainOnly<T>(this Memory<T> value, string fieldName, IBlackboard? blackboard, params T?[] items)
    {
        EnsureDoesNotContainOnly(value.Span, fieldName, blackboard, items);
    }
}
