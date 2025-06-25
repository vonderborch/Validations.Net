using System.Collections.Immutable;
using SimpleBlackboard.Net;

namespace Validations.Net;

/// <summary>
///     Provides a specialized context container for validation exceptions.
///     This class extends the Blackboard pattern to store key-value pairs related to validation failures,
///     allowing additional context to be passed with validation exceptions.
/// </summary>
/// <remarks>
///     ValidationExceptionContext can store arbitrary data that provides context about a validation failure,
///     such as specific validation parameters, field values, or other relevant information.
///     This context can help with constructing more detailed error messages or handling specific validation scenarios.
/// </remarks>
/// <param name="context">
///     An optional dictionary containing initial context data. If null, an empty dictionary will be
///     created.
/// </param>
public class ValidationExceptionContext(Dictionary<string, object?>? context = null) : IBlackboard
{
    /// <summary>
    ///     The internal dictionary that stores the context data for the validation exception.
    /// </summary>
    private readonly Dictionary<string, object?> context = context ?? new Dictionary<string, object?>();

    /// <summary>
    ///     The context of the validation exception, represented as an immutable dictionary.
    /// </summary>
    public ImmutableDictionary<string, object?> Context => this.context.ToImmutableDictionary();

    /// <summary>
    ///     Clears all entries from the blackboard.
    /// </summary>
    /// <exception cref="NotImplementedException">This method is not implemented in the current version.</exception>
    /// <remarks>
    ///     This method is not implemented as the validation exception context is designed to be immutable after creation.
    /// </remarks>
    public override void ClearBlackboard()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Retrieves a value from the validation exception context.
    /// </summary>
    /// <typeparam name="T">The expected type of the value.</typeparam>
    /// <param name="key">The key for the value to retrieve.</param>
    /// <returns>The value associated with the specified key, cast to type T.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the key is not found in the context.</exception>
    /// <exception cref="InvalidCastException">Thrown implicitly when the value cannot be cast to type T.</exception>
    public override T GetValue<T>(string key)
    {
        if (this.context.TryGetValue(key, out var value) && value is T typedValue)
        {
            return typedValue;
        }

        throw new KeyNotFoundException($"Key '{key}' not found in the context.");
    }

    /// <summary>
    ///     Determines whether the context contains a value with the specified key.
    /// </summary>
    /// <typeparam name="T">The expected type of the value.</typeparam>
    /// <param name="key">The key to check.</param>
    /// <returns>True if the context contains the key; otherwise, false.</returns>
    /// <remarks>
    ///     This method only checks for the existence of the key and does not verify the type of the associated value.
    /// </remarks>
    public override bool HasValue<T>(string key)
    {
        return this.context.ContainsKey(key);
    }

    /// <summary>
    ///     Sets a value in the validation exception context.
    /// </summary>
    /// <typeparam name="T">The type of the value to set.</typeparam>
    /// <param name="key">The key for the value.</param>
    /// <param name="value">The value to set.</param>
    /// <returns>Always throws <see cref="NotImplementedException" /> as this operation is not supported.</returns>
    /// <exception cref="NotImplementedException">This method is not implemented in the current version.</exception>
    /// <remarks>
    ///     This method is not implemented as the validation exception context is designed to be immutable after creation.
    /// </remarks>
    public override bool SetValue<T>(string key, T value)
    {
        throw new NotImplementedException();
    }
}
