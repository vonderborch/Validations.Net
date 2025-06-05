using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a collection is empty.
/// </summary>
public static class CollectionIsEmptyValidation
{
    /// <summary>
    /// Determines whether the specified collection is empty.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <returns><c>true</c> if the collection is empty; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEmpty<T>(IEnumerable<T> value)
    {
        value.ValidateIsNotNull("value");
        return value.Count() == 0;
    }

    /// <summary>
    /// Determines whether the specified collection is empty.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <returns><c>true</c> if the collection is empty; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEmpty<T>(ICollection<T> value)
    {
        value.ValidateIsNotNull("value");
        return value.Count == 0;
    }

    /// <summary>
    /// Extension method that determines whether the specified collection is empty.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <returns><c>true</c> if the collection is empty; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEmpty<T>(this IEnumerable<T> value)
    {
        value.ValidateIsNotNull("value");
        return value.Count() == 0;
    }

    /// <summary>
    /// Extension method that determines whether the specified collection is empty.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <returns><c>true</c> if the collection is empty; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEmpty<T>(this ICollection<T> value)
    {
        value.ValidateIsNotNull("value");
        return value.Count == 0;
    }

    /// <summary>
    /// Validates that the specified collection is empty. Throws a <see cref="ValidationException"/> if the collection is not empty.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original collection if it is empty.</returns>
    /// <exception cref="ValidationException">Thrown when the collection is not empty.</exception>
    [DebuggerStepThrough]
    public static IEnumerable<T> IsEmptyValidation<T>(IEnumerable<T> value, string variableName, object? blackboard = null)
    {
        if (!IsEmpty(value))
        {
            throw new ValidationException(variableName, $"{variableName} must be empty.", "IsEmpty", blackboard, exceptionContext: new Dictionary<string, object?>() {
                { "value", value }
            });
        }

        return value;
    }

    /// <summary>
    /// Validates that the specified collection is empty. Throws a <see cref="ValidationException"/> if the collection is not empty.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original collection if it is empty.</returns>
    /// <exception cref="ValidationException">Thrown when the collection is not empty.</exception>
    [DebuggerStepThrough]
    public static ICollection<T> IsEmptyValidation<T>(ICollection<T> value, string variableName, object? blackboard = null)
    {
        if (!IsEmpty(value))
        {
            throw new ValidationException(variableName, $"{variableName} must be empty.", "IsEmpty", blackboard, exceptionContext: new Dictionary<string, object?>() {
                { "value", value }
            });
        }

        return value;
    }

    /// <summary>
    /// Extension method that validates the specified collection is empty. Throws a <see cref="ValidationException"/> if the collection is not empty.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original collection if it is empty.</returns>
    /// <exception cref="ValidationException">Thrown when the collection is not empty.</exception>
    [DebuggerStepThrough]
    public static IEnumerable<T> ValidateIsEmpty<T>(this IEnumerable<T> value, string variableName, object? blackboard = null)
    {
        IsEmptyValidation(value, variableName, blackboard);
        return value;
    }

    /// <summary>
    /// Extension method that validates the specified collection is empty. Throws a <see cref="ValidationException"/> if the collection is not empty.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original collection if it is empty.</returns>
    /// <exception cref="ValidationException">Thrown when the collection is not empty.</exception>
    [DebuggerStepThrough]
    public static ICollection<T> ValidateIsEmpty<T>(this ICollection<T> value, string variableName, object? blackboard = null)
    {
        IsEmptyValidation(value, variableName, blackboard);
        return value;
    }
}