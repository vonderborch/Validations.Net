using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a collection is not empty.
/// </summary>
public static class CollectionIsNotEmptyValidation
{
    /// <summary>
    /// Determines whether the specified collection is not empty.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <returns><c>true</c> if the collection is not empty; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotEmpty<T>(IEnumerable<T> value)
    {
        value.ValidateIsNotNull("value");
        return value.Count() > 0;
    }

    /// <summary>
    /// Determines whether the specified collection is not empty.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <returns><c>true</c> if the collection is not empty; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotEmpty<T>(ICollection<T> value)
    {
        value.ValidateIsNotNull("value");
        return value.Count > 0;
    }

    /// <summary>
    /// Extension method that determines whether the specified collection is not empty.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <returns><c>true</c> if the collection is not empty; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotEmpty<T>(this IEnumerable<T> value)
    {
        value.ValidateIsNotNull("value");
        return value.Count() > 0;
    }

    /// <summary>
    /// Extension method that determines whether the specified collection is not empty.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <returns><c>true</c> if the collection is not empty; otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotEmpty<T>(this ICollection<T> value)
    {
        value.ValidateIsNotNull("value");
        return value.Count > 0;
    }

    /// <summary>
    /// Validates that the specified collection is not empty. Throws a <see cref="ValidationException"/> if the collection is empty.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original collection if it is not empty.</returns>
    /// <exception cref="ValidationException">Thrown when the collection is empty.</exception>
    [DebuggerStepThrough]
    public static IEnumerable<T> IsNotEmptyValidation<T>(IEnumerable<T> value, string variableName, object? blackboard = null)
    {
        if (!IsNotEmpty(value))
        {
            throw new ValidationException(variableName, $"{variableName} must not be empty.", "IsNotEmpty", blackboard, exceptionContext: new Dictionary<string, object?>() {
                { "value", value }
            });
        }

        return value;
    }

    /// <summary>
    /// Validates that the specified collection is not empty. Throws a <see cref="ValidationException"/> if the collection is empty.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original collection if it is not empty.</returns>
    /// <exception cref="ValidationException">Thrown when the collection is empty.</exception>
    [DebuggerStepThrough]
    public static ICollection<T> IsNotEmptyValidation<T>(ICollection<T> value, string variableName, object? blackboard = null)
    {
        if (!IsNotEmpty(value))
        {
            throw new ValidationException(variableName, $"{variableName} must not be empty.", "IsNotEmpty", blackboard, exceptionContext: new Dictionary<string, object?>() {
                { "value", value }
            });
        }

        return value;
    }

    /// <summary>
    /// Extension method that validates the specified collection is not empty. Throws a <see cref="ValidationException"/> if the collection is empty.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original collection if it is not empty.</returns>
    /// <exception cref="ValidationException">Thrown when the collection is empty.</exception>
    [DebuggerStepThrough]
    public static IEnumerable<T> ValidateIsNotEmpty<T>(this IEnumerable<T> value, string variableName, object? blackboard = null)
    {
        IsNotEmptyValidation(value, variableName, blackboard);
        return value;
    }

    /// <summary>
    /// Extension method that validates the specified collection is not empty. Throws a <see cref="ValidationException"/> if the collection is empty.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original collection if it is not empty.</returns>
    /// <exception cref="ValidationException">Thrown when the collection is empty.</exception>
    [DebuggerStepThrough]
    public static ICollection<T> ValidateIsNotEmpty<T>(this ICollection<T> value, string variableName, object? blackboard = null)
    {
        IsNotEmptyValidation(value, variableName, blackboard);
        return value;
    }
}