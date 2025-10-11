namespace Validations.Net.OLD.ValidationAttributes.Helpers;

/// <summary>
///     A generic type-safe cache for storing and retrieving predicate functions.
///     This class implements the singleton pattern to ensure only one cache exists per type.
/// </summary>
/// <typeparam name="T">The type parameter for the input of the predicate functions</typeparam>
public sealed class PredicateCache<T>
{
    /// <summary>
    ///     Lazy-loaded singleton instance of the predicate cache
    /// </summary>
    private static readonly Lazy<PredicateCache<T>> Instance = new(() => new PredicateCache<T>());

    /// <summary>
    ///     Dictionary that stores predicate functions keyed by their unique identifier
    /// </summary>
    public Dictionary<string, Func<T, bool>> GlobalPredicates;

    /// <summary>
    ///     A dictionary that maps predicate identifiers to associated sets of types.
    ///     This allows predicates to be linked or grouped with specific type information,
    ///     facilitating targeted predicate evaluation or retrieval based on type associations.
    /// </summary>
    public Dictionary<string, HashSet<Type>> PredicateAssociations;

    /// <summary>
    ///     Private constructor to prevent direct instantiation. Use the Cache property to access the singleton instance.
    /// </summary>
    private PredicateCache()
    {
        this.GlobalPredicates = new Dictionary<string, Func<T, bool>>();
        this.PredicateAssociations = new Dictionary<string, HashSet<Type>>();
    }

    /// <summary>
    ///     Gets the singleton instance of the predicate cache for type T
    /// </summary>
    public static PredicateCache<T> Cache => Instance.Value;

    /// <summary>
    ///     Adds or updates a predicate function in the cache with the specified key
    /// </summary>
    /// <param name="key">The unique identifier for the predicate function</param>
    /// <param name="type">The type the predicate can be associated with</param>
    /// <param name="predicate">The predicate function to cache</param>
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
    ///     Clears all cached predicates by creating a new empty dictionary
    /// </summary>
    public void Clear()
    {
        this.GlobalPredicates = new Dictionary<string, Func<T, bool>>();
        this.PredicateAssociations = new Dictionary<string, HashSet<Type>>();
    }

    /// <summary>
    ///     Attempts to retrieve a predicate function from the cache using the specified key
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
