using System.Reflection;

namespace Validations.Net.OLD.ValidationAttributes.Helpers;

/// <summary>
///     Provides information about predicates defined within a specific type.
/// </summary>
/// <remarks>
///     This struct is responsible for analyzing a type and discovering all valid predicate methods, properties, and fields
///     that have been marked with <see cref="ValidationPredicateAttribute" />. These predicates are used in the
///     validation
///     system for custom validation logic.
/// </remarks>
public record struct TypePredicatesInfo
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="TypePredicatesInfo" /> struct.
    /// </summary>
    /// <param name="type">The type to analyze for predicates.</param>
    /// <remarks>
    ///     When instantiated, this struct automatically analyzes the specified type to discover
    ///     all valid predicates marked with <see cref="ValidationPredicateAttribute" />.
    /// </remarks>
    public TypePredicatesInfo(Type type)
    {
        this.Type = type;
        this.Predicates = GetPredicates(type);
    }

    /// <summary>
    ///     Gets a read-only list of predicate information objects found in the analyzed type.
    /// </summary>
    /// <remarks>
    ///     Contains information about all valid predicates (methods, properties, and fields) that were
    ///     discovered in the type and marked with <see cref="ValidationPredicateAttribute" />.
    /// </remarks>
    public IReadOnlyList<PredicateInfo> Predicates { get; }

    /// <summary>
    ///     Gets the type that is being analyzed for predicates.
    /// </summary>
    public Type Type { get; }

    /// <summary>
    ///     Analyzes the specified type and extracts information about all valid predicates.
    /// </summary>
    /// <param name="type">The type to analyze.</param>
    /// <returns>A list of <see cref="PredicateInfo" /> objects containing information about valid predicates.</returns>
    /// <remarks>
    ///     This method collects predicate information from fields, properties, and methods that are
    ///     marked with <see cref="ValidationPredicateAttribute" /> and meet the validation criteria.
    /// </remarks>
    private List<PredicateInfo> GetPredicates(Type type)
    {
        List<PredicateInfo> output = new();
        IEnumerable<(FieldInfo field, MethodInfo method, ValidationPredicateAttribute attribute)> fieldsToAdd =
            GetValidFieldsInType(type);
        foreach ((FieldInfo field, MethodInfo method, ValidationPredicateAttribute attribute) field in fieldsToAdd)
        {
            PredicateType predicateType = field.field.IsStatic ? PredicateType.StaticField : PredicateType.Field;
            PredicateInfo info = new(field.attribute.Name, field.attribute.Group, field.field.IsPublic, predicateType,
                field.method, type);
            output.Add(info);
        }

        IEnumerable<(MethodInfo method, ValidationPredicateAttribute attribute)> propertiesToAdd =
            GetValidPropertiesInType(type);
        foreach ((MethodInfo method, ValidationPredicateAttribute attribute) property in propertiesToAdd)
        {
            PredicateType predicateType =
                property.method.IsStatic ? PredicateType.StaticProperty : PredicateType.Property;
            PredicateInfo info = new(property.attribute.Name, property.attribute.Group, property.method.IsPublic,
                predicateType, property.method, type);
            output.Add(info);
        }

        IEnumerable<(MethodInfo method, ValidationPredicateAttribute attribute)> methodsToAdd =
            GetValidMethodsInType(type);
        foreach ((MethodInfo method, ValidationPredicateAttribute attribute) method in methodsToAdd)
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
    /// <remarks>
    ///     A valid predicate field must be marked with <see cref="ValidationPredicateAttribute" />,
    ///     be of type <see cref="Func{T, TResult}" /> where TResult is bool, and follow the predicate field validation rules.
    /// </remarks>
    private static IEnumerable<(FieldInfo field, MethodInfo method, ValidationPredicateAttribute attribute)>
        GetValidFieldsInType(Type type)
    {
        FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                            BindingFlags.Static);
        foreach (FieldInfo field in fields)
        {
            ValidationPredicateAttribute? attribute = field.GetCustomAttribute<ValidationPredicateAttribute>(true);
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
                yield return (field, methodInfo!, attribute);
                continue;
            }

            // Otherwise, try getting value and then its MethodInfo
            var fieldValue = field.GetValue(null); // Will work for static fields (most likely case)
            if (fieldValue is Delegate del)
            {
                MethodInfo methodInfo = del.Method;
                yield return (field, methodInfo, attribute);
            }
        }
    }

    /// <summary>
    ///     Finds all valid predicate methods in the specified type.
    /// </summary>
    /// <param name="type">The type to analyze for predicate methods.</param>
    /// <returns>An enumerable of tuples containing method info and its corresponding predicate registration attribute.</returns>
    /// <remarks>
    ///     A valid predicate method must be marked with <see cref="ValidationPredicateAttribute" />,
    ///     return a boolean value, and accept exactly one parameter.
    /// </remarks>
    private static IEnumerable<(MethodInfo method, ValidationPredicateAttribute attribute)>
        GetValidMethodsInType(Type type)
    {
        IEnumerable<(MethodInfo, ValidationPredicateAttribute)> output = type
            .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
            .Where(method => method.GetCustomAttributes(typeof(ValidationPredicateAttribute)).Any() &&
                             IsValidPredicateMethod(method))
            .Select(method => (method, method.GetCustomAttribute<ValidationPredicateAttribute>()!));
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
    /// <remarks>
    ///     A valid predicate property must be marked with <see cref="ValidationPredicateAttribute" />,
    ///     be of type <see cref="Func{T, TResult}" /> where TResult is bool, and have a public getter.
    /// </remarks>
    private static IEnumerable<(MethodInfo method, ValidationPredicateAttribute attribute)>
        GetValidPropertiesInType(Type type)
    {
        IEnumerable<(MethodInfo, ValidationPredicateAttribute)> output = type
            .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
            .Where(property => property.GetCustomAttributes(typeof(ValidationPredicateAttribute)).Any() &&
                               IsValidPredicateProperty(property))
            .Select(field => (field.GetMethod!, field.GetCustomAttribute<ValidationPredicateAttribute>()!));
        return output;
    }

    /// <summary>
    ///     Determines whether a field qualifies as a valid predicate field.
    /// </summary>
    /// <param name="field">The field to validate.</param>
    /// <returns>True if the field is a valid predicate field; otherwise, false.</returns>
    /// <remarks>
    ///     A valid predicate field must be a generic type of <see cref="Func{T, TResult}" /> where TResult is bool,
    ///     and the generic type must have exactly 2 arguments with the second argument being of type bool.
    /// </remarks>
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
    /// <remarks>
    ///     A valid predicate method must return a boolean value and accept exactly one parameter.
    /// </remarks>
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
    /// <remarks>
    ///     A valid predicate property must be a generic type of <see cref="Func{T, TResult}" /> where TResult is bool,
    ///     have a public getter, and the generic type must have exactly 2 arguments.
    /// </remarks>
    private static bool IsValidPredicateProperty(PropertyInfo property)
    {
        if (!property.PropertyType.IsGenericType || property.GetMethod is null || !property.GetMethod.IsPublic)
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
