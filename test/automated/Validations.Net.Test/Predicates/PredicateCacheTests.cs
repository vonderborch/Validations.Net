using System.Collections.Concurrent;
using NUnit.Framework;
using Validations.Net.Predicates;

namespace Validations.Net.Test.Predicates;

[TestFixture]
public class PredicateCacheTests
{
    private PredicateCache<string> _cache;

    [SetUp]
    public void SetUp()
    {
        _cache = PredicateCache<string>.Instance;
        _cache.Clear();
    }

    [TearDown]
    public void TearDown()
    {
        _cache.Clear();
    }

    [Test]
    public void Instance_IsSingleton()
    {
        // Act
        var instance1 = PredicateCache<string>.Instance;
        var instance2 = PredicateCache<string>.Instance;

        // Assert
        Assert.That(instance1, Is.SameAs(instance2));
    }

    [Test]
    public void Instance_DifferentGenericTypes_AreDifferentInstances()
    {
        // Act
        var stringCache = PredicateCache<string>.Instance;
        var intCache = PredicateCache<int>.Instance;

        // Assert - Different generic types should have different instances
        Assert.That(stringCache.GetType(), Is.Not.EqualTo(intCache.GetType()));
        Assert.That(stringCache.GetType().GetGenericArguments()[0], Is.EqualTo(typeof(string)));
        Assert.That(intCache.GetType().GetGenericArguments()[0], Is.EqualTo(typeof(int)));
    }

    [Test]
    public void AddPredicate_WithNewKey_AddsToBothCollections()
    {
        // Arrange
        var key = "testKey";
        var type = typeof(string);
        Func<string, bool> predicate = s => s.Length > 0;

        // Act
        _cache.AddPredicate(key, type, predicate);

        // Assert
        Assert.That(_cache.GlobalPredicates.ContainsKey(key), Is.True);
        Assert.That(_cache.PredicateAssociations.ContainsKey(key), Is.True);
        Assert.That(_cache.GlobalPredicates[key], Is.EqualTo(predicate));
        Assert.That(_cache.PredicateAssociations[key].Contains(type), Is.True);
    }

    [Test]
    public void AddPredicate_WithExistingKey_AddsTypeToAssociations()
    {
        // Arrange
        var key = "testKey";
        var type1 = typeof(string);
        var type2 = typeof(object);
        Func<string, bool> predicate = s => s.Length > 0;

        // Act
        _cache.AddPredicate(key, type1, predicate);
        _cache.AddPredicate(key, type2, predicate);

        // Assert
        Assert.That(_cache.GlobalPredicates.Count, Is.EqualTo(1));
        Assert.That(_cache.PredicateAssociations[key].Count, Is.EqualTo(2));
        Assert.That(_cache.PredicateAssociations[key].Contains(type1), Is.True);
        Assert.That(_cache.PredicateAssociations[key].Contains(type2), Is.True);
    }

    [Test]
    public void AddPredicate_WithSameKeyAndType_DoesNotDuplicate()
    {
        // Arrange
        var key = "testKey";
        var type = typeof(string);
        Func<string, bool> predicate = s => s.Length > 0;

        // Act
        _cache.AddPredicate(key, type, predicate);
        _cache.AddPredicate(key, type, predicate);

        // Assert
        Assert.That(_cache.GlobalPredicates.Count, Is.EqualTo(1));
        Assert.That(_cache.PredicateAssociations[key].Count, Is.EqualTo(1));
        Assert.That(_cache.PredicateAssociations[key].Contains(type), Is.True);
    }

    [Test]
    public void GetPredicate_WithValidKeyAndType_ReturnsPredicate()
    {
        // Arrange
        var key = "testKey";
        var type = typeof(string);
        Func<string, bool> predicate = s => s.Length > 0;
        _cache.AddPredicate(key, type, predicate);

        // Act
        var result = _cache.GetPredicate(key, type);

        // Assert
        Assert.That(result, Is.EqualTo(predicate));
    }

    [Test]
    public void GetPredicate_WithValidKeyButWrongType_ReturnsNull()
    {
        // Arrange
        var key = "testKey";
        var type1 = typeof(string);
        var type2 = typeof(int);
        Func<string, bool> predicate = s => s.Length > 0;
        _cache.AddPredicate(key, type1, predicate);

        // Act
        var result = _cache.GetPredicate(key, type2);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void GetPredicate_WithInvalidKey_ReturnsNull()
    {
        // Arrange
        var key = "testKey";
        var invalidKey = "invalidKey";
        var type = typeof(string);
        Func<string, bool> predicate = s => s.Length > 0;
        _cache.AddPredicate(key, type, predicate);

        // Act
        var result = _cache.GetPredicate(invalidKey, type);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Clear_RemovesAllPredicatesAndAssociations()
    {
        // Arrange
        var key1 = "testKey1";
        var key2 = "testKey2";
        var type = typeof(string);
        Func<string, bool> predicate1 = s => s.Length > 0;
        Func<string, bool> predicate2 = s => s.Length > 5;
        _cache.AddPredicate(key1, type, predicate1);
        _cache.AddPredicate(key2, type, predicate2);

        // Act
        _cache.Clear();

        // Assert
        Assert.That(_cache.GlobalPredicates.Count, Is.EqualTo(0));
        Assert.That(_cache.PredicateAssociations.Count, Is.EqualTo(0));
    }

    [Test]
    public void AddPredicate_WithMultipleTypes_MaintainsCorrectAssociations()
    {
        // Arrange
        var key = "testKey";
        var types = new[] { typeof(string), typeof(object), typeof(int), typeof(double) };
        Func<string, bool> predicate = s => s.Length > 0;

        // Act
        foreach (var type in types)
        {
            _cache.AddPredicate(key, type, predicate);
        }

        // Assert
        Assert.That(_cache.GlobalPredicates.Count, Is.EqualTo(1));
        Assert.That(_cache.PredicateAssociations[key].Count, Is.EqualTo(4));
        foreach (var type in types)
        {
            Assert.That(_cache.PredicateAssociations[key].Contains(type), Is.True);
            Assert.That(_cache.GetPredicate(key, type), Is.EqualTo(predicate));
        }
    }

    [Test]
    public void AddPredicate_WithDifferentPredicatesForSameKey_UsesLastPredicate()
    {
        // Arrange
        var key = "testKey";
        var type = typeof(string);
        Func<string, bool> predicate1 = s => s.Length > 0;
        Func<string, bool> predicate2 = s => s.Length > 5;

        // Act
        _cache.AddPredicate(key, type, predicate1);
        _cache.AddPredicate(key, type, predicate2);

        // Assert
        Assert.That(_cache.GlobalPredicates[key], Is.EqualTo(predicate1)); // First predicate is kept
        Assert.That(_cache.GetPredicate(key, type), Is.EqualTo(predicate1));
    }

    [Test]
    public void GlobalPredicates_IsPublicProperty()
    {
        // Act & Assert
        Assert.That(_cache.GlobalPredicates, Is.Not.Null);
        Assert.That(_cache.GlobalPredicates, Is.InstanceOf<ConcurrentDictionary<string, Func<string, bool>>>());
    }

    [Test]
    public void PredicateAssociations_IsPublicProperty()
    {
        // Act & Assert
        Assert.That(_cache.PredicateAssociations, Is.Not.Null);
        Assert.That(_cache.PredicateAssociations, Is.InstanceOf<ConcurrentDictionary<string, HashSet<Type>>>());
    }

    [Test]
    public void AddPredicate_WithNullPredicate_DoesNotThrow()
    {
        // Arrange
        var key = "testKey";
        var type = typeof(string);
        Func<string, bool>? predicate = null;

        // Act & Assert
        Assert.DoesNotThrow(() => _cache.AddPredicate(key, type, predicate!));
    }

    [Test]
    public void GetPredicate_AfterClear_ReturnsNull()
    {
        // Arrange
        var key = "testKey";
        var type = typeof(string);
        Func<string, bool> predicate = s => s.Length > 0;
        _cache.AddPredicate(key, type, predicate);
        _cache.Clear();

        // Act
        var result = _cache.GetPredicate(key, type);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void AddPredicate_WithEmptyKey_WorksCorrectly()
    {
        // Arrange
        var key = "";
        var type = typeof(string);
        Func<string, bool> predicate = s => s.Length > 0;

        // Act
        _cache.AddPredicate(key, type, predicate);

        // Assert
        Assert.That(_cache.GetPredicate(key, type), Is.EqualTo(predicate));
    }

    [Test]
    public void AddPredicate_WithSpecialCharactersInKey_WorksCorrectly()
    {
        // Arrange
        var key = "test-key_with.special@chars#123";
        var type = typeof(string);
        Func<string, bool> predicate = s => s.Length > 0;

        // Act
        _cache.AddPredicate(key, type, predicate);

        // Assert
        Assert.That(_cache.GetPredicate(key, type), Is.EqualTo(predicate));
    }

    [Test]
    public void ThreadSafety_MultipleAddOperations_DoNotCauseExceptions()
    {
        // Arrange
        var tasks = new List<Task>();
        var numberOfTasks = 10;

        // Act
        for (int i = 0; i < numberOfTasks; i++)
        {
            var taskIndex = i;
            tasks.Add(Task.Run(() =>
            {
                var key = $"key{taskIndex}";
                var type = typeof(string);
                Func<string, bool> predicate = s => s.Length > taskIndex;
                _cache.AddPredicate(key, type, predicate);
            }));
        }

        // Assert
        Assert.DoesNotThrow(() => Task.WaitAll(tasks.ToArray()));
        Assert.That(_cache.GlobalPredicates.Count, Is.EqualTo(numberOfTasks));
    }
}
