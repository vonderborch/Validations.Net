using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Validates that a collection contains only the specified items.
/// </summary>
public static class DoesContainOnly
{
    private const string ValidatorName = nameof(DoesContainOnly);

    /// <summary>
    ///     Checks if the collection contains only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should contain exclusively.</param>
    /// <returns>True if the collection contains only the specified items; otherwise, false.</returns>
    public static bool CheckDoesContainOnly<T>(this IEnumerable<T>? value, params T?[] items)
    {
        if (value == null)
        {
            return false;
        }

        if (items == null || items.Length == 0)
        {
            return !value.Any();
        }

        var valueList = value.ToList();
        var itemsList = items.Where(x => x != null).ToList();

        // Check if all items in the collection are in the specified items
        foreach (var item in valueList)
        {
            if (!itemsList.Contains(item))
            {
                return false;
            }
        }

        // Check if all specified items are in the collection
        foreach (var item in itemsList)
        {
            if (!valueList.Contains(item))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if the collection contains only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should contain exclusively.</param>
    /// <returns>True if the collection contains only the specified items; otherwise, false.</returns>
    public static bool CheckDoesContainOnly<T>(this IList<T>? value, params T?[] items)
    {
        if (value == null)
        {
            return false;
        }

        return CheckDoesContainOnly((IEnumerable<T>)value, items);
    }

    /// <summary>
    ///     Checks if the collection contains only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should contain exclusively.</param>
    /// <returns>True if the collection contains only the specified items; otherwise, false.</returns>
    public static bool CheckDoesContainOnly<T>(this List<T>? value, params T?[] items)
    {
        if (value == null)
        {
            return false;
        }

        return CheckDoesContainOnly((IEnumerable<T>)value, items);
    }

    /// <summary>
    ///     Checks if the collection contains only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should contain exclusively.</param>
    /// <returns>True if the collection contains only the specified items; otherwise, false.</returns>
    public static bool CheckDoesContainOnly<T>(this T[]? value, params T?[] items)
    {
        if (value == null)
        {
            return false;
        }

        return CheckDoesContainOnly((IEnumerable<T>)value, items);
    }

    /// <summary>
    ///     Checks if the collection contains only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should contain exclusively.</param>
    /// <returns>True if the collection contains only the specified items; otherwise, false.</returns>
    public static bool CheckDoesContainOnly<T>(this ReadOnlySpan<T> value, params T?[] items)
    {
        return CheckDoesContainOnly(value.ToArray(), items);
    }

    /// <summary>
    ///     Checks if the collection contains only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should contain exclusively.</param>
    /// <returns>True if the collection contains only the specified items; otherwise, false.</returns>
    public static bool CheckDoesContainOnly<T>(this Span<T> value, params T?[] items)
    {
        return CheckDoesContainOnly((ReadOnlySpan<T>)value, items);
    }

    /// <summary>
    ///     Checks if the collection contains only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should contain exclusively.</param>
    /// <returns>True if the collection contains only the specified items; otherwise, false.</returns>
    public static bool CheckDoesContainOnly<T>(this Memory<T> value, params T?[] items)
    {
        return CheckDoesContainOnly(value.Span, items);
    }

    /// <summary>
    ///     Ensures that the collection contains only the specified items, throwing an exception if validation fails.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should contain exclusively.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the collection does not contain only the specified items.</exception>
    public static void EnsureDoesContainOnly<T>(this IEnumerable<T>? value, string fieldName, IBlackboard? blackboard,
        params T?[] items)
    {
        var isValid = CheckDoesContainOnly(value, items);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Items", items),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName,
                $"The collection must contain only the specified items: [{string.Join(", ", items ?? Array.Empty<T>())}]",
                null, null, contextList);
        }
    }

    /// <summary>
    ///     Ensures that the collection contains only the specified items, throwing an exception if validation fails.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should contain exclusively.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the collection does not contain only the specified items.</exception>
    public static void EnsureDoesContainOnly<T>(this IList<T>? value, string fieldName, IBlackboard? blackboard,
        params T?[] items)
    {
        EnsureDoesContainOnly((IEnumerable<T>?)value, fieldName, blackboard, items);
    }

    /// <summary>
    ///     Ensures that the collection contains only the specified items, throwing an exception if validation fails.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should contain exclusively.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the collection does not contain only the specified items.</exception>
    public static void EnsureDoesContainOnly<T>(this List<T>? value, string fieldName, IBlackboard? blackboard,
        params T?[] items)
    {
        EnsureDoesContainOnly((IEnumerable<T>?)value, fieldName, blackboard, items);
    }

    /// <summary>
    ///     Ensures that the collection contains only the specified items, throwing an exception if validation fails.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should contain exclusively.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the collection does not contain only the specified items.</exception>
    public static void EnsureDoesContainOnly<T>(this T[]? value, string fieldName, IBlackboard? blackboard,
        params T?[] items)
    {
        EnsureDoesContainOnly((IEnumerable<T>?)value, fieldName, blackboard, items);
    }

    /// <summary>
    ///     Ensures that the collection contains only the specified items, throwing an exception if validation fails.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should contain exclusively.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the collection does not contain only the specified items.</exception>
    public static void EnsureDoesContainOnly<T>(this ReadOnlySpan<T> value, string fieldName, IBlackboard? blackboard,
        params T?[] items)
    {
        EnsureDoesContainOnly(value.ToArray(), fieldName, blackboard, items);
    }

    /// <summary>
    ///     Ensures that the collection contains only the specified items, throwing an exception if validation fails.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should contain exclusively.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the collection does not contain only the specified items.</exception>
    public static void EnsureDoesContainOnly<T>(this Span<T> value, string fieldName, IBlackboard? blackboard,
        params T?[] items)
    {
        EnsureDoesContainOnly((ReadOnlySpan<T>)value, fieldName, blackboard, items);
    }

    /// <summary>
    ///     Ensures that the collection contains only the specified items, throwing an exception if validation fails.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should contain exclusively.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the collection does not contain only the specified items.</exception>
    public static void EnsureDoesContainOnly<T>(this Memory<T> value, string fieldName, IBlackboard? blackboard,
        params T?[] items)
    {
        EnsureDoesContainOnly(value.Span, fieldName, blackboard, items);
    }

    /// <summary>
    ///     Validates that the collection contains only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should contain exclusively.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateDoesContainOnly<T>(this IEnumerable<T>? value, string fieldName,
        IBlackboard? blackboard, params T?[] items)
    {
        var isValid = CheckDoesContainOnly(value, items);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Items", items),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName,
                $"The collection must contain only the specified items: [{string.Join(", ", items ?? Array.Empty<T>())}]",
                fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that the collection contains only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should contain exclusively.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateDoesContainOnly<T>(this IList<T>? value, string fieldName,
        IBlackboard? blackboard, params T?[] items)
    {
        return ValidateDoesContainOnly((IEnumerable<T>?)value, fieldName, blackboard, items);
    }

    /// <summary>
    ///     Validates that the collection contains only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should contain exclusively.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateDoesContainOnly<T>(this List<T>? value, string fieldName,
        IBlackboard? blackboard, params T?[] items)
    {
        return ValidateDoesContainOnly((IEnumerable<T>?)value, fieldName, blackboard, items);
    }

    /// <summary>
    ///     Validates that the collection contains only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should contain exclusively.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateDoesContainOnly<T>(this T[]? value, string fieldName,
        IBlackboard? blackboard, params T?[] items)
    {
        return ValidateDoesContainOnly((IEnumerable<T>?)value, fieldName, blackboard, items);
    }

    /// <summary>
    ///     Validates that the collection contains only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should contain exclusively.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateDoesContainOnly<T>(this ReadOnlySpan<T> value, string fieldName,
        IBlackboard? blackboard, params T?[] items)
    {
        return ValidateDoesContainOnly(value.ToArray(), fieldName, blackboard, items);
    }

    /// <summary>
    ///     Validates that the collection contains only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should contain exclusively.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateDoesContainOnly<T>(this Span<T> value, string fieldName,
        IBlackboard? blackboard, params T?[] items)
    {
        return ValidateDoesContainOnly((ReadOnlySpan<T>)value, fieldName, blackboard, items);
    }

    /// <summary>
    ///     Validates that the collection contains only the specified items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="value">The collection to validate.</param>
    /// <param name="items">The items that the collection should contain exclusively.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateDoesContainOnly<T>(this Memory<T> value, string fieldName,
        IBlackboard? blackboard, params T?[] items)
    {
        return ValidateDoesContainOnly(value.Span, fieldName, blackboard, items);
    }
}
