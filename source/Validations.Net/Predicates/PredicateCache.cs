using Singletons.Net;

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
    public Dictionary<string, Func<T, bool>> GlobalPredicates;

    /// <summary>
    /// A dictionary that maps predicate keys to the types they are associated with
    /// </summary>
    public Dictionary<string, HashSet<Type>> PredicateAssociations;

    /// <summary>
    /// Initializes a new instance of the <see cref="PredicateCache{T}"/> class
    /// </summary>
    private PredicateCache()
    {
        this.GlobalPredicates = new Dictionary<string, Func<T, bool>>();
        this.PredicateAssociations = new Dictionary<string, HashSet<Type>>();
    }

    /// <summary>
    /// Adds a predicate to the cache
    /// </summary>
    /// <param name="key">The unique identifier for the predicate function</param>
    /// <param name="type">The type the predicate is associated with</param>
    /// <param name="predicate">The predicate function to add</param>
    public void AddPredicate(string key, Type type, Func<T, bool> predicate)
    {
        if (!this.PredicateAssociations.ContainsKey(key))
        {
            this.GlobalPredicates[key] = predicate;
            this.PredicateAssociations[key] = new HashSet<Type>();
        }

        this.PredicateAssociations[key].Add(type);
    }

    /// <summary>
    /// Clears the cache
    /// </summary>
    public void Clear()
    {
        this.GlobalPredicates = new Dictionary<string, Func<T, bool>>();
        this.PredicateAssociations = new Dictionary<string, HashSet<Type>>();
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
            if (this.PredicateAssociations[key].Contains(type))
            {
                return predicate;
            }
        }

        return null;
    }
}
