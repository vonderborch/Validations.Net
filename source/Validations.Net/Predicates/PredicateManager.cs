using System.Collections.Concurrent;
using System.Threading;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Predicates;

public static class PredicateManager
{
    private static readonly ConcurrentDictionary<string, PredicateInfo> GlobalPredicates = new();

    private static readonly ConcurrentDictionary<Type, ConcurrentDictionary<string, PredicateInfo>> InstancePredicates =
        new();
        
    private static readonly Lock SyncLock = new();

    private static bool _predicatesInitialized;

    public static int InitializationThreadCount = 4;

    static PredicateManager() {}

    /// <summary>
    /// Clears all registered predicates.
    /// </summary>
    public static void Clear()
    {
        lock (SyncLock)
        {
            _predicatesInitialized = false;
            GlobalPredicates.Clear();
        }
    }

    public static Func<T, bool>? GetPredicate<T>(string name, string? group, object? instance, bool refresh = false)
    {
        instance.EnsureIsNotNull(nameof(instance));
        var key = PredicateInfo.GetKey(name, group);
        Type type = instance!.GetType();

        // First, check if the predicate is already in the typed cache
        Func<T, bool>? cachedPredicate = PredicateCache<T>.Instance.GetPredicate(key, type);
        if (cachedPredicate is not null)
        {
            return cachedPredicate;
        }
        
        // Next, load the predicates (as needed)
    }

    private static void InitializePredicates(bool refresh = false) {
        lock (SyncLock)
        {
            if (_predicatesInitialized && !refresh)
            {
                return;
            }

            Clear();
            
            // Get types from all loaded assemblies for scanning
            IEnumerable<Type> assemblyTypes = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => !a.IsDynamic && !a.FullName!.StartsWith("System.") && !a.FullName.StartsWith("Microsoft."))
                .SelectMany(a =>
                {
                    try
                    {
                        return a.GetTypes();
                    }
                    catch
                    {
                        return Array.Empty<Type>();
                    }
                });

            // Process each type in parallel for better performance
            Parallel.ForEach(assemblyTypes, new ParallelOptions { MaxDegreeOfParallelism = InitializationThreadCount }, type =>
            {
                try
                {
                    ProcessTypePredicates(type);
                }
                catch
                {
                    // Log or handle exception if needed
                }
            });

            _predicatesInitialized = true;
        }
    }

    private static void ProcessTypePredicates(Type type)
    {
        TypePredicatesInfo predicatesInfo = new(type);
        if (predicatesInfo.Predicates.Count == 0)
        {
            return;
        }

        foreach (PredicateInfo predicate in predicatesInfo.Predicates)
        {
            if (predicate is { IsGlobalPredicate: true })
            {
                // Register global predicate
                GlobalPredicates[predicate.Key] = predicate;
            }
            else
            {
                // Register instance predicate
                ConcurrentDictionary<string, PredicateInfo> typePredicates =
                    InstancePredicates.GetOrAdd(type, _ => new ConcurrentDictionary<string, PredicateInfo>());
                typePredicates[predicate.Key] = predicate;
            }
        }
    }
}
