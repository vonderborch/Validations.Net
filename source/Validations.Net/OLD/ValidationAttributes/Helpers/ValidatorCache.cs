using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes.Helpers;

/// <summary>
///     Provides caching functionality for validation metadata to improve performance.
/// </summary>
/// <remarks>
///     The <see cref="ValidatorCache" /> class maintains a thread-safe cache of validation metadata for types,
///     allowing the validation system to avoid repetitive reflection operations when validating objects of the same type.
///     The cache uses a least-recently-used (LRU) eviction policy to manage memory usage.
/// </remarks>
public static class ValidatorCache
{
    /// <summary>
    ///     Thread-safe dictionary that stores validation metadata for types while preserving access order.
    /// </summary>
    private static readonly ThreadSafeOrderedDictionary<Type, TypeValidationInfo> CachedTypeValidators = new();

    /// <summary>
    ///     The maximum number of type validation entries to keep in the cache. Default is 32.
    /// </summary>
    private static int _cacheSize = 32;

    /// <summary>
    ///     Clears all cached type validation metadata from the cache.
    /// </summary>
    /// <remarks>
    ///     Use this method when you want to force the validation system to rebuild
    ///     validation metadata for all types, such as after application configuration changes
    ///     that might affect validation behavior.
    /// </remarks>
    public static void ClearCache()
    {
        CachedTypeValidators.Clear();
    }

    /// <summary>
    ///     Retrieves cached validation metadata for the type of the provided instance.
    /// </summary>
    /// <typeparam name="T">The type for which to retrieve validation metadata.</typeparam>
    /// <param name="instance">An instance of the type. Can be null for reference types.</param>
    /// <returns>A <see cref="TypeValidationInfo" /> containing validation metadata for the type.</returns>
    /// <remarks>
    ///     This method implements a least-recently-used (LRU) caching strategy. When the method is called:
    ///     <list type="bullet">
    ///         <item>
    ///             <description>If the type is already in the cache, it's moved to the most-recently-used position.</description>
    ///         </item>
    ///         <item>
    ///             <description>If the type is not in the cache, validation metadata is created and added to the cache.</description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 If adding a new entry would exceed the cache size limit, the least recently used entry is
    ///                 removed.
    ///             </description>
    ///         </item>
    ///     </list>
    /// </remarks>
    public static TypeValidationInfo GetValidatorsForInstance<T>(T? instance)
    {
        // Get the cached validations for the type of the instance, or create a new one if it doesn't exist
        Type type = typeof(T);
        if (!CachedTypeValidators.TryGetValue(type, out TypeValidationInfo typeValidators))
        {
            // If the type is not cached, create a new CachedValidations instance
            typeValidators = new TypeValidationInfo(type);
            CachedTypeValidators[type] = typeValidators;
        }
        else
        {
            // Update the cache keys to maintain the order of access
            CachedTypeValidators.Remove(type);
            CachedTypeValidators[type] = typeValidators;
        }

        // If the cache size exceeds the limit, remove the oldest entry
        if (CachedTypeValidators.Count > _cacheSize)
        {
            Type oldestKey = CachedTypeValidators.GetOldestKey()!;
            CachedTypeValidators.Remove(oldestKey);
        }

        return typeValidators;
    }

    /// <summary>
    ///     Sets the maximum number of type validation entries to keep in the cache.
    /// </summary>
    /// <param name="newCacheSize">The new maximum cache size. Must be greater than zero.</param>
    /// <remarks>
    ///     Adjust this value based on your application's memory constraints and the number of
    ///     different types you expect to validate frequently. Larger cache sizes improve performance
    ///     for applications that validate many different types, at the cost of increased memory usage.
    /// </remarks>
    /// <exception cref="ValidationException">Thrown if <paramref name="newCacheSize" /> is less than or equal to zero.</exception>
    public static void SetCacheSize(int newCacheSize)
    {
        newCacheSize.ValidateIsGreaterThan(0, nameof(newCacheSize));
        _cacheSize = newCacheSize;
    }
}
