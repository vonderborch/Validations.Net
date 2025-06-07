using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Validations.Net.Validators.Collections;

/// <summary>
/// Provides validation methods to check if a collection is empty.
/// These methods can be used to validate that collections contain no elements,
/// which is useful in scenarios where empty collections are required or expected.
/// </summary>
public static class EmptyValidations
{
    /// <summary>
    /// Extension method that determines whether the specified collection is empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <returns><c>true</c> if the collection is empty (contains no elements); otherwise, <c>false</c>.</returns>
    /// <exception cref="ValidationException">Thrown when the collection is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEmpty<T>(this ICollection<T>? collection)
    {
        collection = collection.ValidateIsNotNull("collection");
        return collection.Count == 0;
    }

    /// <summary>
    /// Extension method that determines whether the specified enumerable is empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the enumerable.</typeparam>
    /// <param name="collection">The enumerable to check.</param>
    /// <returns><c>true</c> if the enumerable is empty (contains no elements); otherwise, <c>false</c>.</returns>
    /// <exception cref="ValidationException">Thrown when the enumerable is null.</exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsEmpty<T>(this IEnumerable<T>? collection)
    {
        collection = collection.ValidateIsNotNull("collection");
        return !collection.Any();
    }
    
    /// <summary>
    /// Extension method that determines whether the specified collection is not empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <returns><c>true</c> if the collection is not empty (contains at least one element); otherwise, <c>false</c>.</returns>
    /// <exception cref="ValidationException">Thrown when the collection is null.</exception>
    /// <remarks>
    /// This method is the logical inverse of <see cref="CheckIsEmpty{T}(ICollection{T})"/>.
    /// It first validates that the collection is not null before checking if it contains elements.
    /// </remarks>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotEmpty<T>(this ICollection<T>? collection)
    {
        return !collection.CheckIsEmpty();
    }

    /// <summary>
    /// Extension method that determines whether the specified enumerable is not empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the enumerable.</typeparam>
    /// <param name="collection">The enumerable to check.</param>
    /// <returns><c>true</c> if the enumerable is not empty (contains at least one element); otherwise, <c>false</c>.</returns>
    /// <exception cref="ValidationException">Thrown when the enumerable is null.</exception>
    /// <remarks>
    /// This method is the logical inverse of <see cref="CheckIsEmpty{T}(IEnumerable{T})"/>.
    /// It first validates that the enumerable is not null before checking if it contains elements.
    /// </remarks>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotEmpty<T>(this IEnumerable<T>? collection)
    {
        return !collection.CheckIsEmpty();
    }

    /// <summary>
    /// Extension method that validates the specified collection is empty. 
    /// Throws a <see cref="ValidationException"/> if the collection is not empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original collection if it is empty.</returns>
    /// <exception cref="ValidationException">Thrown when the collection is not empty or is null.</exception>
    [DebuggerStepThrough]
    public static ICollection<T> ValidateIsEmpty<T>(this ICollection<T>? collection, string variableName, object? blackboard = null)
    {
        if (!collection.CheckIsEmpty())
        {
            throw new ValidationException(variableName, $"{variableName} must be empty.", "IsEmpty", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", collection }
            });
        }

        return collection;
    }

    /// <summary>
    /// Extension method that validates the specified enumerable is empty. 
    /// Throws a <see cref="ValidationException"/> if the enumerable is not empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the enumerable.</typeparam>
    /// <param name="collection">The enumerable to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original enumerable if it is empty.</returns>
    /// <exception cref="ValidationException">Thrown when the enumerable is not empty or is null.</exception>
    [DebuggerStepThrough]
    public static IEnumerable<T> ValidateIsEmpty<T>(this IEnumerable<T>? collection, string variableName, object? blackboard = null)
    {
        if (!collection.CheckIsEmpty())
        {
            throw new ValidationException(variableName, $"{variableName} must be empty.", "IsEmpty", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", collection }
            });
        }

        return collection!;
    }
    
    /// <summary>
    /// Extension method that validates the specified collection is not empty. 
    /// Throws a <see cref="ValidationException"/> if the collection is empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original collection if it is not empty.</returns>
    /// <exception cref="ValidationException">Thrown when the collection is empty or is null.</exception>
    /// <remarks>
    /// This method is useful in scenarios where a collection must contain at least one element to proceed with an operation.
    /// The exception includes the variable name in the error message for easier debugging.
    /// </remarks>
    [DebuggerStepThrough]
    public static ICollection<T> ValidateIsNotEmpty<T>(this ICollection<T>? collection, string variableName, object? blackboard = null)
    {
        if (!collection.CheckIsNotEmpty())
        {
            throw new ValidationException(variableName, $"{variableName} must not be empty.", "IsNotEmpty", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", collection }
            });
        }

        return collection!;
    }

    /// <summary>
    /// Extension method that validates the specified enumerable is not empty. 
    /// Throws a <see cref="ValidationException"/> if the enumerable is empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the enumerable.</typeparam>
    /// <param name="collection">The enumerable to check.</param>
    /// <param name="variableName">The name of the variable being checked, used in the exception message.</param>
    /// <param name="blackboard">Optional contextual data to include with the validation exception.</param>
    /// <returns>The original enumerable if it is not empty.</returns>
    /// <exception cref="ValidationException">Thrown when the enumerable is empty or is null.</exception>
    /// <remarks>
    /// This method is useful in scenarios where an enumerable must contain at least one element to proceed with an operation.
    /// The exception includes the variable name in the error message for easier debugging.
    /// Note that this method may enumerate the collection twice: once to check if it's not empty and once when returning it.
    /// </remarks>
    [DebuggerStepThrough]
    public static IEnumerable<T> ValidateIsNotEmpty<T>(this IEnumerable<T>? collection, string variableName, object? blackboard = null)
    {
        if (!collection.CheckIsNotEmpty())
        {
            throw new ValidationException(variableName, $"{variableName} must not be empty.", "IsNotEmpty", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", collection }
            });
        }

        return collection!;
    }
}
