using System.Collections.Concurrent;
using Singletons.Net;

namespace Validations.Net.OLD.Predicates;

/// <summary>
/// A cache for predicate functions, keyed by (predicateKey, declaringType) to avoid
/// collisions when different types register the same predicate name.
/// </summary>
/// <typeparam name="T">The type of the predicate input</typeparam>
public sealed class PredicateCache<T> : SingletonBase<PredicateCache<T>>
{
    private readonly ConcurrentDictionary<(string Key, Type DeclaringType), Func<T, bool>> _predicates = new();

    /// <summary>
    /// Private constructor to enforce singleton pattern.
    /// </summary>
    private PredicateCache() { }

    /// <summary>
    /// Adds a predicate to the cache.
    /// </summary>
    public void AddPredicate(string key, Type type, Func<T, bool> predicate)
    {
        this._predicates.TryAdd((key, type), predicate);
    }

    /// <summary>
    /// Clears the cache.
    /// </summary>
    public void Clear()
    {
        this._predicates.Clear();
    }

    /// <summary>
    /// Attempts to retrieve a predicate function from the cache.
    /// </summary>
    public Func<T, bool>? GetPredicate(string key, Type type)
    {
        return this._predicates.TryGetValue((key, type), out var predicate) ? predicate : null;
    }
}
