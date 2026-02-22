using System.Reflection;

namespace Validations.Net.Predicates;

/// <summary>
/// Represents information about a type and its associated validation predicates.
/// </summary>
public record struct TypePredicatesInfo
{
    /// <summary>
    /// Gets the type associated with the validation predicates.
    /// </summary>
    public Type Type { get; }

    /// <summary>
    /// Represents a collection of predicate definitions used for validation.
    /// </summary>
    public IReadOnlyList<PredicateInfo> Predicates { get; }

    /// <summary>
    /// Represents information about a type and its associated predicates.
    /// </summary>
    public TypePredicatesInfo(Type type)
    {
        this.Type = type;
        this.Predicates = GetPredicates(type);
    }

    /// <summary>
    ///     Analyzes the specified type and extracts information about all valid predicates.
    /// </summary>
    /// <param name="type">The type to analyze.</param>
    /// <returns>A list of <see cref="PredicateInfo" /> objects containing information about valid predicates.</returns>
    private List<PredicateInfo> GetPredicates(Type type)
    {
        List<PredicateInfo> output = new();
        IEnumerable<(FieldInfo field, MethodInfo method, PredicateRegistrationAttribute attribute)> fieldsToAdd =
            GetValidFieldsInType(type);
        foreach ((FieldInfo field, MethodInfo method, PredicateRegistrationAttribute attribute) field in fieldsToAdd)
        {
            PredicateType predicateType = field.field.IsStatic ? PredicateType.StaticField : PredicateType.Field;
            PredicateInfo info = new(field.attribute.Name, field.attribute.Group, field.field.IsPublic, predicateType,
                field.method, type);
            output.Add(info);
        }

        IEnumerable<(MethodInfo method, PredicateRegistrationAttribute attribute)> propertiesToAdd =
            GetValidPropertiesInType(type);
        foreach ((MethodInfo method, PredicateRegistrationAttribute attribute) property in propertiesToAdd)
        {
            PredicateType predicateType =
                property.method.IsStatic ? PredicateType.StaticProperty : PredicateType.Property;
            PredicateInfo info = new(property.attribute.Name, property.attribute.Group, property.method.IsPublic,
                predicateType, property.method, type);
            output.Add(info);
        }

        IEnumerable<(MethodInfo method, PredicateRegistrationAttribute attribute)> methodsToAdd =
            GetValidMethodsInType(type);
        foreach ((MethodInfo method, PredicateRegistrationAttribute attribute) method in methodsToAdd)
        {
            PredicateType predicateType = method.method.IsStatic ? PredicateType.StaticMethod : PredicateType.Method;
            PredicateInfo info = new(method.attribute.Name, method.attribute.Group, method.method.IsPublic,
                predicateType, method.method, type);
            output.Add(info);
        }

        return output;
    }

    /// <summary>
    ///     Finds all valid predicate fields in the specified type.
    /// </summary>
    /// <param name="type">The type to analyze for predicate fields.</param>
    /// <returns>
    ///     An enumerable of tuples containing field info, method info (field invoke method), and its corresponding
    ///     predicate registration attribute.
    /// </returns>
    private static IEnumerable<(FieldInfo field, MethodInfo method, PredicateRegistrationAttribute attribute)>
        GetValidFieldsInType(Type type)
    {
        FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                            BindingFlags.Static);
        foreach (FieldInfo field in fields)
        {
            PredicateRegistrationAttribute? attribute = field.GetCustomAttribute<PredicateRegistrationAttribute>(true);
            if (attribute == null)
            {
                continue;
            }

            if (!IsValidPredicateField(field))
            {
                continue;
            }

            // The field must be of type Func<T, bool>, so get its MethodInfo (the Invoke method of the delegate)
            if (field.FieldType.IsSubclassOf(typeof(Delegate)))
            {
                MethodInfo? methodInfo = field.FieldType.GetMethod("Invoke");
                if (methodInfo is null) continue;
                yield return (field, methodInfo, attribute);
                continue;
            }

            // For static fields, resolve the delegate value at discovery time
            if (field.IsStatic)
            {
                var fieldValue = field.GetValue(null);
                if (fieldValue is Delegate del)
                {
                    MethodInfo methodInfo = del.Method;
                    yield return (field, methodInfo, attribute);
                }
            }
        }
    }

    /// <summary>
    ///     Finds all valid predicate methods in the specified type.
    /// </summary>
    /// <param name="type">The type to analyze for predicate methods.</param>
    /// <returns>An enumerable of tuples containing method info and its corresponding predicate registration attribute.</returns>
    private static IEnumerable<(MethodInfo method, PredicateRegistrationAttribute attribute)>
        GetValidMethodsInType(Type type)
    {
        IEnumerable<(MethodInfo, PredicateRegistrationAttribute)> output = type
            .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
            .Where(method => method.GetCustomAttributes(typeof(PredicateRegistrationAttribute)).Any() &&
                             IsValidPredicateMethod(method))
            .Select(method => (method, method.GetCustomAttribute<PredicateRegistrationAttribute>()!));
        return output;
    }

    /// <summary>
    ///     Finds all valid predicate properties in the specified type.
    /// </summary>
    /// <param name="type">The type to analyze for predicate properties.</param>
    /// <returns>
    ///     An enumerable of tuples containing method info (property getter) and its corresponding predicate registration
    ///     attribute.
    /// </returns>
    private static IEnumerable<(MethodInfo method, PredicateRegistrationAttribute attribute)>
        GetValidPropertiesInType(Type type)
    {
        IEnumerable<(MethodInfo, PredicateRegistrationAttribute)> output = type
            .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
            .Where(property => property.GetCustomAttributes(typeof(PredicateRegistrationAttribute)).Any() &&
                               IsValidPredicateProperty(property))
            .Select(field => (field.GetMethod!, field.GetCustomAttribute<PredicateRegistrationAttribute>()!));
        return output;
    }

    /// <summary>
    ///     Determines whether a field qualifies as a valid predicate field.
    /// </summary>
    /// <param name="field">The field to validate.</param>
    /// <returns>True if the field is a valid predicate field; otherwise, false.</returns>
    private static bool IsValidPredicateField(FieldInfo field)
    {
        // The field must be of type Func<T, bool>
        if (!field.FieldType.IsGenericType)
        {
            return false;
        }

        Type genericType = field.FieldType.GetGenericTypeDefinition();
        if (genericType != typeof(Func<,>))
        {
            return false;
        }

        Type[] genericArgs = field.FieldType.GetGenericArguments();
        if (genericArgs.Length != 2)
        {
            return false;
        }

        // The second generic argument must be bool (i.e. Func<T, bool>)
        if (genericArgs[1] != typeof(bool))
        {
            return false;
        }

        // The field must be static or instance, and should be public or non-public
        // Unlike properties, fields don't have get/set methods, so nothing else to check

        return true;
    }

    /// <summary>
    ///     Determines whether a method qualifies as a valid predicate method.
    /// </summary>
    /// <param name="method">The method to validate.</param>
    /// <returns>True if the method is a valid predicate method; otherwise, false.</returns>
    private static bool IsValidPredicateMethod(MethodInfo method)
    {
        if (method.ReturnType != typeof(bool))
        {
            return false;
        }

        return method.GetParameters().Length == 1;
    }

    /// <summary>
    ///     Determines whether a property qualifies as a valid predicate property.
    /// </summary>
    /// <param name="property">The property to validate.</param>
    /// <returns>True if the property is a valid predicate property; otherwise, false.</returns>
    private static bool IsValidPredicateProperty(PropertyInfo property)
    {
        if (!property.PropertyType.IsGenericType || property.GetMethod is null)
        {
            return false;
        }

        Type genericType = property.PropertyType.GetGenericTypeDefinition();
        if (genericType != typeof(Func<,>))
        {
            return false;
        }

        Type[] genericArgs = property.PropertyType.GetGenericArguments();
        return genericArgs.Length == 2 && genericArgs[1] == typeof(bool);
    }
}
