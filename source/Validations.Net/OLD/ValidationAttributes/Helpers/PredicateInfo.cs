using System.Reflection;
using Validations.Net.OLD.Validators;

namespace Validations.Net.OLD.ValidationAttributes.Helpers;

/// <summary>
///     Represents information on a predicate used for validation logic within the validation system.
/// </summary>
/// <remarks>
///     A predicate is a function or property that evaluates to a boolean value and is used to validate
///     whether a given condition is met. This struct stores metadata about where and how to find
///     such predicate logic within types.
/// </remarks>
/// <param name="Name">The name of the predicate, used to identify it in validation attributes.</param>
/// <param name="Group">
///     Optional group name that can categorize related predicates. May be null if the predicate is not
///     grouped.
/// </param>
/// <param name="IsPublic">Whether the predicate is public or not.</param>
/// <param name="Type">The type of member that contains the predicate logic (property, field, method, etc.).</param>
/// <param name="MethodInfo">Reflection information about the method, property accessor, or field accessor to invoke.</param>
/// <param name="DeclaringType">The type that declares the member containing the predicate logic.</param>
public record struct PredicateInfo(
    string Name,
    string? Group,
    bool IsPublic,
    PredicateType Type,
    MethodInfo MethodInfo,
    Type DeclaringType)
{
    /// <summary>
    ///     Gets a value indicating whether the predicate is a global predicate.
    /// </summary>
    /// <remarks>
    ///     This property evaluates whether the predicate is both static in nature and publicly accessible.
    ///     A predicate is considered global if it belongs to one of the static types
    ///     (e.g., static field, static property, static method) and has public visibility.
    /// </remarks>
    public bool IsGlobalPredicate { get; } = Type.CheckIsOneOf(PredicateType.StaticField, PredicateType.StaticProperty,
        PredicateType.StaticMethod) && IsPublic;

    /// <summary>
    ///     Indicates whether the predicate represents a static member.
    /// </summary>
    /// <remarks>
    ///     This property determines if the predicate pertains to a static field, static property, or static method.
    ///     It uses the <see cref="PredicateType" /> to evaluate whether the type of the predicate corresponds to any
    ///     of the static member types, such as <see cref="PredicateType.StaticField" />,
    ///     <see cref="PredicateType.StaticProperty" />,
    ///     or <see cref="PredicateType.StaticMethod" />.
    /// </remarks>
    public bool IsStatic { get; } = Type.CheckIsOneOf(PredicateType.StaticField, PredicateType.StaticProperty,
        PredicateType.StaticMethod);

    /// <summary>
    ///     Gets the unique key for the predicate based on its name and group.
    /// </summary>
    /// <remarks>
    ///     This property combines the name of the predicate and its optional group to form a unique key
    ///     that can be used for identification or lookup purposes. If no group is specified, the key will
    ///     consist of just the predicate's name. The key is generated using the <see cref="GetKey" /> method.
    /// </remarks>
    public string Key { get; } = GetKey(Name, Group);

    /// <summary>
    ///     Generates a unique key for identifying a predicate based on its name and optional group.
    /// </summary>
    /// <param name="name">The name of the predicate. This is a required parameter and must not be null.</param>
    /// <param name="group">
    ///     An optional group name used to categorize the predicate. If null, the key will be based only on the
    ///     name.
    /// </param>
    /// <returns>
    ///     A string representing the unique key for the predicate, which combines the group and name if a group is
    ///     provided.
    /// </returns>
    public static string GetKey(string name, string? group)
    {
        var key = group is null ? name : $"{group}-{name}";
        return key;
    }
}
