namespace Validations.Net.ValidationAttributes.Helpers;

/// <summary>
/// A generic type-safe cache for storing and retrieving predicate functions.
/// This class implements the singleton pattern to ensure only one cache exists per type.
/// </summary>
/// <typeparam name="T">The type parameter for the input of the predicate functions</typeparam>
public sealed class PredicateCache<T>
{
    /// <summary>
    /// Lazy-loaded singleton instance of the predicate cache
    /// </summary>
    private static readonly Lazy<PredicateCache<T>> Instance = new Lazy<PredicateCache<T>>(() => new PredicateCache<T>());

    /// <summary>
    /// Private constructor to prevent direct instantiation. Use the Cache property to access the singleton instance.
    /// </summary>
    private PredicateCache()
    {
        this.GlobalPredicates = new();
        PredicateAssociations = new();
    }

    /// <summary>
    /// Gets the singleton instance of the predicate cache for type T
    /// </summary>
    public static PredicateCache<T> Cache => Instance.Value;

    /// <summary>
    /// Dictionary that stores predicate functions keyed by their unique identifier
    /// </summary>
    public Dictionary<string, Func<T, bool>> GlobalPredicates;

    /// <summary>
    /// A dictionary that maps predicate identifiers to associated sets of types.
    /// This allows predicates to be linked or grouped with specific type information,
    /// facilitating targeted predicate evaluation or retrieval based on type associations.
    /// </summary>
    public Dictionary<string, HashSet<Type>> PredicateAssociations;

    /// <summary>
    /// Clears all cached predicates by creating a new empty dictionary
    /// </summary>
    public void Clear()
    {
        this.GlobalPredicates = new();
        PredicateAssociations = new();
    }

    /// <summary>
    /// Attempts to retrieve a predicate function from the cache using the specified key
    /// </summary>
    /// <param name="key">The unique identifier for the predicate function</param>
    /// <param name="type">The type the predicate is associated with</param>
    /// <returns>The predicate function if found; otherwise, null</returns>
    public Func<T, bool>? GetPredicate(string key, Type type)
    {
        if (this.GlobalPredicates.TryGetValue(key, out var predicate))
        {
            if (PredicateAssociations[key].Contains(type))
            {
                return predicate;
            }
        }

        return null;
    }

    /// <summary>
    /// Adds or updates a predicate function in the cache with the specified key
    /// </summary>
    /// <param name="key">The unique identifier for the predicate function</param>
    /// <param name="type">The type the predicate can be associated with</param>
    /// <param name="predicate">The predicate function to cache</param>
    public void AddPredicate(string key, Type type, Func<T, bool> predicate)
    {
        if (!this.PredicateAssociations.ContainsKey(key))
        {
            this.GlobalPredicates[key] = predicate;
            this.PredicateAssociations[key] = new();
        }
        PredicateAssociations[key].Add(type);
    }
}
