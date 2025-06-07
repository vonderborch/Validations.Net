using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validations.Net.Validators.Collections;

/// <summary>
/// Provides validation methods to check if a collection is null or empty.
/// These methods can be used to validate that collections are either null or contain no elements,
/// which is useful in scenarios where null or empty collections are required or expected.
/// </summary>
public static class NullOrEmptyValidations
{
    /// <summary>
    /// Extension method that determines whether the specified collection is null or empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <returns><c>true</c> if the collection is null or empty (contains no elements); otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNullOrEmpty<T>(this ICollection<T>? collection)
    {
        return collection is null || collection.Count == 0;
    }

    /// <summary>
    /// Extension method that determines whether the specified enumerable is null or empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the enumerable.</typeparam>
    /// <param name="collection">The enumerable to check.</param>
    /// <returns><c>true</c> if the enumerable is null or empty (contains no elements); otherwise, <c>false</c>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNullOrEmpty<T>(this IEnumerable<T>? collection)
    {
        return collection is null || !collection.Any();
    }
    
    /// <summary>
    /// Extension method that determines whether the specified collection is not null or empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <returns><c>true</c> if the collection is not null and contains at least one element; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// This method is the logical inverse of <see cref="CheckIsNullOrEmpty{T}(ICollection{T})"/>.
    /// It checks that the collection is neither null nor empty (contains at least one element).
    /// </remarks>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotNullOrEmpty<T>(this ICollection<T>? collection)
    {
        return !collection.CheckIsNullOrEmpty();
    }

    /// <summary>
    /// Extension method that determines whether the specified enumerable is not null or empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the enumerable.</typeparam>
    /// <param name="collection">The enumerable to check.</param>
    /// <returns><c>true</c> if the enumerable is not null and contains at least one element; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// This method is the logical inverse of <see cref="CheckIsNullOrEmpty{T}(IEnumerable{T})"/>.
    /// It checks that the enumerable is neither null nor empty (contains at least one element).
    /// </remarks>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotNullOrEmpty<T>(this IEnumerable<T>? collection)
    {
        return !collection.CheckIsNullOrEmpty();
    }

    /// <summary>
    /// Extension method that validates the specified collection is null or empty. 
    /// Throws a <see cref="ValidationException"/> if the collection is neither null nor empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original collection if it is null or empty.</returns>
    /// <exception cref="ValidationException">Thrown when the collection is neither null nor empty.</exception>
    [DebuggerStepThrough]
    public static ICollection<T> ValidateIsNullOrEmpty<T>(this ICollection<T>? collection, string variableName, object? blackboard = null)
    {
        if (!collection.CheckIsNullOrEmpty())
        {
            throw new ValidationException(variableName, $"{variableName} must be null or empty.", "IsNullOrEmpty", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", collection }
            });
        }

        return collection!;
    }

    /// <summary>
    /// Extension method that validates the specified enumerable is null or empty. 
    /// Throws a <see cref="ValidationException"/> if the enumerable is neither null nor empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the enumerable.</typeparam>
    /// <param name="collection">The enumerable to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original enumerable if it is null or empty.</returns>
    /// <exception cref="ValidationException">Thrown when the enumerable is neither null nor empty.</exception>
    [DebuggerStepThrough]
    public static IEnumerable<T> ValidateIsNullOrEmpty<T>(this IEnumerable<T>? collection, string variableName, object? blackboard = null)
    {
        if (!collection.CheckIsNullOrEmpty())
        {
            throw new ValidationException(variableName, $"{variableName} must be null or empty.", "IsNullOrEmpty", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", collection }
            });
        }

        return collection!;
    }
    
    /// <summary>
    /// Extension method that validates the specified collection is neither null nor empty. 
    /// Throws a <see cref="ValidationException"/> if the collection is null or empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original collection if it is not null and not empty.</returns>
    /// <exception cref="ValidationException">Thrown when the collection is null or empty.</exception>
    /// <remarks>
    /// This method is useful in scenarios where a collection must contain at least one element to proceed with an operation.
    /// The exception includes the variable name in the error message for easier debugging.
    /// </remarks>
    [DebuggerStepThrough]
    public static ICollection<T> ValidateIsNotNullOrEmpty<T>(this ICollection<T>? collection, string variableName, object? blackboard = null)
    {
        if (collection.CheckIsNullOrEmpty())
        {
            throw new ValidationException(variableName, $"{variableName} must not be null or empty.", "IsNotNullOrEmpty", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", collection }
            });
        }

        return collection!;
    }

    /// <summary>
    /// Extension method that validates the specified enumerable is neither null nor empty. 
    /// Throws a <see cref="ValidationException"/> if the enumerable is null or empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the enumerable.</typeparam>
    /// <param name="collection">The enumerable to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original enumerable if it is not null and not empty.</returns>
    /// <exception cref="ValidationException">Thrown when the enumerable is null or empty.</exception>
    /// <remarks>
    /// This method is useful in scenarios where an enumerable must contain at least one element to proceed with an operation.
    /// The exception includes the variable name in the error message for easier debugging.
    /// Note that this method may enumerate the collection twice if it's not null or empty: once to check if it's empty 
    /// and once when returning it.
    /// </remarks>
    [DebuggerStepThrough]
    public static IEnumerable<T> ValidateIsNotNullOrEmpty<T>(this IEnumerable<T>? collection, string variableName, object? blackboard = null)
    {
        if (collection.CheckIsNullOrEmpty())
        {
            throw new ValidationException(variableName, $"{variableName} must not be null or empty.", "IsNotNullOrEmpty", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", collection }
            });
        }

        return collection!;
    }
}
