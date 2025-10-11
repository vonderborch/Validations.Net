using Singletons.Net;
using System.Collections.Concurrent;

namespace Validations.Net.Predicates;

/// <summary>
/// A cache for predicate functions
/// </summary>
/// <typeparam name="T">The type of the predicate</typeparam>
public sealed class PredicateCache<T> : SingletonBase<PredicateCache<T>>
{
    /// <summary>
    /// A dictionary that maps predicate keys to the predicate functions
    /// </summary>
    public ConcurrentDictionary<string, Func<T, bool>> GlobalPredicates;

    /// <summary>
    /// A dictionary that maps predicate keys to the types they are associated with
    /// </summary>
    public ConcurrentDictionary<string, HashSet<Type>> PredicateAssociations;

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
        this.GlobalPredicates.TryAdd(key, predicate);
        this.PredicateAssociations.AddOrUpdate(key, 
            new HashSet<Type> { type }, 
            (existingKey, existingSet) => 
            {
                existingSet.Add(type);
                return existingSet;
            });
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
            if (this.PredicateAssociations.TryGetValue(key, out HashSet<Type>? associations) && associations.Contains(type))
            {
                return predicate;
            }
        }

        return null;
    }
}
