using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace Validations.Net.Validation;

/// <summary>
/// Describes a member (property or field) that has validation attributes.
/// Includes a compiled accessor delegate for high-performance value retrieval.
/// </summary>
internal sealed class MemberValidationDescriptor
{
    public required string Name { get; init; }
    public required MemberInfo MemberInfo { get; init; }
    public required Func<object, object?> GetValue { get; init; }
    public required ValidationAttribute[] Attributes { get; init; }
    public required bool IsNested { get; init; }
    public required bool IsCollection { get; init; }
}

/// <summary>
/// Cached per-type validation metadata: discovered members with their attributes
/// and compiled accessors, plus class-level attributes.
/// </summary>
internal sealed class TypeValidationInfo
{
    private static readonly ConcurrentDictionary<Type, TypeValidationInfo> Cache = new();

    public MemberValidationDescriptor[] Members { get; }
    public ValidationAttribute[] ClassAttributes { get; }

    private TypeValidationInfo(MemberValidationDescriptor[] members, ValidationAttribute[] classAttributes)
    {
        Members = members;
        ClassAttributes = classAttributes;
    }

    /// <summary>
    /// Gets or creates the TypeValidationInfo for the given type.
    /// Reflection and delegate compilation happen once per type.
    /// </summary>
    public static TypeValidationInfo GetForType(Type type)
    {
        return Cache.GetOrAdd(type, static t => Discover(t));
    }

    private static TypeValidationInfo Discover(Type type)
    {
        var members = new List<MemberValidationDescriptor>();
        const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        // Discover properties
        foreach (var prop in type.GetProperties(flags))
        {
            if (!prop.CanRead) continue;
            var attrs = prop.GetCustomAttributes<ValidationAttribute>(true).ToArray();
            if (attrs.Length == 0) continue;

            var hasNested = Array.Exists(attrs, a => a.GetType().Name == "ValidateNestedAttribute");
            var hasCollection = Array.Exists(attrs, a => a.GetType().Name == "ValidateEachIsValidAttribute");

            members.Add(new MemberValidationDescriptor
            {
                Name = prop.Name,
                MemberInfo = prop,
                GetValue = CompilePropertyAccessor(type, prop),
                Attributes = attrs,
                IsNested = hasNested,
                IsCollection = hasCollection
            });
        }

        // Discover fields
        foreach (var field in type.GetFields(flags))
        {
            var attrs = field.GetCustomAttributes<ValidationAttribute>(true).ToArray();
            if (attrs.Length == 0) continue;

            var hasNested = Array.Exists(attrs, a => a.GetType().Name == "ValidateNestedAttribute");
            var hasCollection = Array.Exists(attrs, a => a.GetType().Name == "ValidateEachIsValidAttribute");

            members.Add(new MemberValidationDescriptor
            {
                Name = field.Name,
                MemberInfo = field,
                GetValue = CompileFieldAccessor(type, field),
                Attributes = attrs,
                IsNested = hasNested,
                IsCollection = hasCollection
            });
        }

        // Class-level attributes
        var classAttrs = type.GetCustomAttributes<ValidationAttribute>(true).ToArray();

        return new TypeValidationInfo(members.ToArray(), classAttrs);
    }

    private static Func<object, object?> CompilePropertyAccessor(Type declaringType, PropertyInfo property)
    {
        var param = Expression.Parameter(typeof(object), "obj");
        var cast = Expression.Convert(param, declaringType);
        var access = Expression.Property(cast, property);
        var box = Expression.Convert(access, typeof(object));
        return Expression.Lambda<Func<object, object?>>(box, param).Compile();
    }

    private static Func<object, object?> CompileFieldAccessor(Type declaringType, FieldInfo field)
    {
        var param = Expression.Parameter(typeof(object), "obj");
        var cast = Expression.Convert(param, declaringType);
        var access = Expression.Field(cast, field);
        var box = Expression.Convert(access, typeof(object));
        return Expression.Lambda<Func<object, object?>>(box, param).Compile();
    }
}
