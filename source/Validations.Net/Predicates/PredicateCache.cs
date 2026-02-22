using Singletons.Net;
using System.Collections.Concurrent;

namespace Validations.Net.Predicates;

/// <summary>
/// A cache for predicate functions
/// </summary>
/// <typeparam name="T">The type of the predicate</typeparam>
public sealed class PredicateCache<T> : SingletonBase<PredicateCache<T>>
{
    private static readonly object AddPredicateLock = new();

    /// <summary>
    /// A dictionary that maps predicate keys to the predicate functions
    /// </summary>
    private readonly ConcurrentDictionary<string, Func<T, bool>> GlobalPredicates;

    /// <summary>
    /// A dictionary that maps predicate keys to the types they are associated with
    /// </summary>
    private readonly ConcurrentDictionary<string, HashSet<Type>> PredicateAssociations;

    /// <summary>
    /// Initializes a new instance of the <see cref="PredicateCache{T}"/> class
    /// </summary>
    private PredicateCache()
    {
        this.GlobalPredicates = new ConcurrentDictionary<string, Func<T, bool>>();
        this.PredicateAssociations = new ConcurrentDictionary<string, HashSet<Type>>();
    }

    /// <summary>
    /// Adds a predicate to the cache
    /// </summary>
    /// <param name="key">The unique identifier for the predicate function</param>
    /// <param name="type">The type the predicate is associated with</param>
    /// <param name="predicate">The predicate function to add</param>
    public void AddPredicate(string key, Type type, Func<T, bool> predicate)
    {
        lock (AddPredicateLock)
        {
            this.GlobalPredicates.AddOrUpdate(key, predicate, (_, existing) => existing);
            this.PredicateAssociations.AddOrUpdate(key,
                _ => new HashSet<Type> { type },
                (_, existingSet) =>
                {
                    lock (existingSet)
                    {
                        existingSet.Add(type);
                    }
                    return existingSet;
                });
        }
    }

    /// <summary>
    /// Clears the cache
    /// </summary>
    public void Clear()
    {
        this.GlobalPredicates.Clear();
        this.PredicateAssociations.Clear();
    }

    /// <summary>
    /// Attempts to retrieve a predicate function from the cache using the specified key
    /// </summary>
    /// <param name="key">The unique identifier for the predicate function</param>
    /// <param name="type">The type the predicate is associated with</param>
    /// <returns>The predicate function if found; otherwise, null</returns>
    public Func<T, bool>? GetPredicate(string key, Type type)
    {
        if (this.GlobalPredicates.TryGetValue(key, out Func<T, bool>? predicate))
        {
            if (this.PredicateAssociations.TryGetValue(key, out HashSet<Type>? associations))
            {
                lock (associations)
                {
                    if (associations.Contains(type))
                    {
                        return predicate;
                    }
                }
            }
        }

        return null;
    }
}
