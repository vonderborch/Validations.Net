namespace Validations.Net.Helpers;

/// <summary>
///     Provides a thread-safe wrapper around an OrderedDictionary collection that maintains insertion order of elements.
///     This implementation ensures all operations are protected by a lock for thread safety.
/// </summary>
/// <typeparam name="TKey">The type of keys in the dictionary. Must be non-null.</typeparam>
/// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
internal class ThreadSafeOrderedDictionary<TKey, TValue> where TKey : notnull
{
    /// <summary>
    ///     The underlying ordered dictionary that stores the key-value pairs.
    /// </summary>
    private readonly OrderedDictionary<TKey, TValue> _dictionary = new();

    /// <summary>
    ///     Object used for locking to ensure thread safety.
    /// </summary>
    private readonly Lock _lock = new();

    /// <summary>
    ///     Gets the number of key/value pairs contained in the dictionary.
    /// </summary>
    /// <returns>The number of key/value pairs in the dictionary.</returns>
    public int Count
    {
        get
        {
            int count;
            lock (this._lock)
            {
                count = this._dictionary.Count;
            }

            return count;
        }
    }

    /// <summary>
    ///     Gets a collection containing the keys in the dictionary in a thread-safe manner.
    ///     Returns a copy of the keys to avoid concurrent modification issues.
    /// </summary>
    /// <returns>A collection containing the keys in the dictionary.</returns>
    public ICollection<TKey> Keys
    {
        get
        {
            lock (this._lock)
            {
                return new List<TKey>(this._dictionary.Keys);
            }
        }
    }

    /// <summary>
    ///     Gets a collection containing the values in the dictionary in a thread-safe manner.
    ///     Returns a copy of the values to avoid concurrent modification issues.
    /// </summary>
    /// <returns>A collection containing the values in the dictionary.</returns>
    public ICollection<TValue> Values
    {
        get
        {
            lock (this._lock)
            {
                return new List<TValue>(this._dictionary.Values);
            }
        }
    }

    /// <summary>
    ///     Gets or sets the value associated with the specified key in a thread-safe manner.
    /// </summary>
    /// <param name="key">The key of the value to get or set.</param>
    /// <returns>The value associated with the specified key.</returns>
    /// <exception cref="System.Collections.Generic.KeyNotFoundException">The key does not exist in the dictionary.</exception>
    /// <exception cref="System.ArgumentNullException">The key is null.</exception>
    public TValue this[TKey key]
    {
        get
        {
            lock (this._lock)
            {
                return this._dictionary[key];
            }
        }
        set
        {
            lock (this._lock)
            {
                this._dictionary[key] = value;
            }
        }
    }

    /// <summary>
    ///     Adds an element with the provided key and value to the dictionary in a thread-safe manner.
    /// </summary>
    /// <param name="key">The key of the element to add.</param>
    /// <param name="value">The value of the element to add.</param>
    /// <exception cref="System.ArgumentException">An element with the same key already exists in the dictionary.</exception>
    /// <exception cref="System.ArgumentNullException">The key is null.</exception>
    public void Add(TKey key, TValue value)
    {
        lock (this._lock)
        {
            this._dictionary.Add(key, value);
        }
    }

    /// <summary>
    ///     Removes all keys and values from the dictionary in a thread-safe manner.
    /// </summary>
    public void Clear()
    {
        lock (this._lock)
        {
            this._dictionary.Clear();
        }
    }

    /// <summary>
    ///     Gets the oldest key in the dictionary (the first one added) in a thread-safe manner.
    /// </summary>
    /// <returns>The oldest key in the dictionary, or default value if the dictionary is empty.</returns>
    public TKey? GetOldestKey()
    {
        lock (this._lock)
        {
            if (this._dictionary.Count == 0)
            {
                return default;
            }

            // Get the first (oldest) key from the ordered dictionary
            TKey firstKey = this._dictionary.Keys.First();
            return firstKey;
        }
    }

    /// <summary>
    ///     Removes the element with the specified key from the dictionary in a thread-safe manner.
    /// </summary>
    /// <param name="key">The key of the element to remove.</param>
    /// <returns>True if the element is successfully removed; otherwise, false.</returns>
    /// <exception cref="System.ArgumentNullException">The key is null.</exception>
    public void Remove(TKey key)
    {
        lock (this._lock)
        {
            this._dictionary.Remove(key);
        }
    }

    /// <summary>
    ///     Gets the value associated with the specified key in a thread-safe manner.
    /// </summary>
    /// <param name="key">The key whose value to get.</param>
    /// <param name="value">
    ///     When this method returns, the value associated with the specified key, if the key is found;
    ///     otherwise, the default value for the type of the value parameter.
    /// </param>
    /// <returns>true if the dictionary contains an element with the specified key; otherwise, false.</returns>
    /// <exception cref="System.ArgumentNullException">The key is null.</exception>
    public bool TryGetValue(TKey key, out TValue? value)
    {
        lock (this._lock)
        {
            return this._dictionary.TryGetValue(key, out value);
        }
    }
}
