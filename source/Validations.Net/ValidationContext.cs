using System.Collections.Immutable;
using SimpleBlackboard.Net;

namespace Validations.Net;

/// <summary>
/// Represents a context for maintaining and managing validation-related data.
/// Provides functionality to store, retrieve, and manipulate key-value pairs of contextual information
/// for validation operations. The context is backed by a mutable dictionary for internal storage.
/// </summary>
public class ValidationContext(Dictionary<string, object?>? context = null) : IBlackboard
{
    /// <summary>
    /// Internal storage for validation context data, represented as a mutable dictionary.
    /// </summary>
    private readonly Dictionary<string, object?> _context = context ?? new();
    
    /// <summary>
    ///     The context of the validation exception, represented as an immutable dictionary.
    /// </summary>
    public ImmutableDictionary<string, object?> Context => this._context.ToImmutableDictionary();

    /// <summary>
    /// Sets a value in the validation context for a specified key. overwriting the value if the key already exists.
    /// </summary>
    /// <param name="key">The key associated with the value to be set.</param>
    /// <param name="value">The value to associate with the specified key.</param>
    /// <typeparam name="T">The type of the value being set.</typeparam>
    /// <returns>
    /// Returns <c>true</c> if the value was successfully set or updated; otherwise returns <c>false</c>.
    /// </returns>
    /// <exception cref="NotImplementedException">Thrown if the method is not implemented.</exception>
    public bool SetValue<T>(string key, T value)
    {
        _context[key] = value;
        return true;
    }

    /// <summary>
    /// Retrieves a value of a specified type from the validation context for a given key.
    /// </summary>
    /// <param name="key">The key associated with the value to be retrieved.</param>
    /// <typeparam name="T">The type of the value being retrieved.</typeparam>
    /// <returns>
    /// The value associated with the specified key, cast to the specified type <typeparamref name="T"/>.
    /// </returns>
    /// <exception cref="KeyNotFoundException">
    /// Thrown when the specified key does not exist in the validation context.
    /// </exception>
    public T? GetValue<T>(string key)
    {
        if (this._context.TryGetValue(key, out var value) && value is T typedValue)
        {
            return typedValue;
        }

        throw new KeyNotFoundException($"Key '{key}' not found in the context.");
    }

    /// <summary>
    /// Attempts to retrieve a value of the specified type associated with the given key
    /// from the validation context.
    /// </summary>
    /// <param name="key">The key associated with the value to be retrieved.</param>
    /// <param name="value">
    /// When this method returns, contains the value associated with the specified key
    /// if the key is found and the value is of the specified type;
    /// otherwise, the default value for the type of the value parameter.
    /// </param>
    /// <typeparam name="T">The expected type of the value associated with the specified key.</typeparam>
    /// <returns>
    /// Returns <c>true</c> if the key exists in the context and the value is of the specified type;
    /// otherwise, returns <c>false</c>.
    /// </returns>
    public bool TryGetValue<T>(string key, out T? value)
    {
        if (this._context.TryGetValue(key, out var fetchedValue) && fetchedValue is T typedValue)
        {
            value = typedValue;
            return true;
        }

        value = default;
        return false;
    }

    /// <summary>
    /// Determines whether the validation context contains a value for the specified key and that value is of the specified type.
    /// </summary>
    /// <param name="key">The key to check for existence in the validation context.</param>
    /// <typeparam name="T">The expected type of the value associated with the specified key.</typeparam>
    /// <returns>
    /// Returns <c>true</c> if the context contains a value for the specified key and it is of the specified type; otherwise, returns <c>false</c>.
    /// </returns>
    public bool HasValue<T>(string key)
    {
        bool hasValue = this._context.TryGetValue(key, out var value) && value is T;
        return hasValue;
    }

    /// <summary>
    /// Clears all key-value pairs from the validation context, removing any stored contextual data.
    /// </summary>
    /// <exception cref="NotImplementedException">Thrown if the method is not implemented.</exception>
    public void ClearBlackboard()
    {
        _context.Clear();
    }

    /// <summary>
    /// Removes the value associated with the specified key from the validation context.
    /// </summary>
    /// <param name="key">The key associated with the value to be removed.</param>
    /// <typeparam name="T">The expected type of the value to be removed.</typeparam>
    /// <returns>
    /// Returns the value that was associated with the specified key if it existed; otherwise, returns the default value of type <typeparamref name="T"/>.
    /// </returns>
    /// <exception cref="KeyNotFoundException">Thrown if the specified key is not present in the context.</exception>
    /// <exception cref="InvalidCastException">Thrown if the value associated with the key cannot be cast to type <typeparamref name="T"/>.</exception>
    /// <exception cref="NotImplementedException">Thrown if the method is not implemented.</exception>
    public T? RemoveValue<T>(string key)
    {
        if (_context.Remove(key, out var value) && value is T typedValue)
            return typedValue;
        return default;
    }

    /// <summary>
    /// Attempts to remove the value associated with the specified key from the validation context.
    /// </summary>
    /// <param name="key">The key identifying the value to be removed.</param>
    /// <param name="value">When this method returns, contains the value that was associated with the specified key, if the key is found; otherwise, it contains the default value for the type of the value parameter.</param>
    /// <typeparam name="T">The type of the value being removed.</typeparam>
    /// <returns>
    /// Returns <c>true</c> if the value was successfully removed; otherwise, returns <c>false</c>.
    /// </returns>
    /// <exception cref="NotImplementedException">Thrown if the method is not implemented.</exception>
    public bool TryRemoveValue<T>(string key, out T? value)
    {
        if (_context.Remove(key, out var rawValue) && rawValue is T typedValue)
        {
            value = typedValue;
            return true;
        }
        value = default;
        return false;
    }
}
