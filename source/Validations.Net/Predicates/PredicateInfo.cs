using System.Reflection;

namespace Validations.Net.Predicates;

/// <summary>
///     Represents information on a predicate used for validation logic within the validation system.
/// </summary>
/// <param name="Name">The name of the predicate, used to identify it in validation attributes.</param>
/// <param name="Group">
///     Optional group name that can categorize related predicates. May be null if the predicate is not
///     grouped.
/// </param>
/// <param name="IsPublic">Whether the predicate is public or not.</param>
/// <param name="Type">The type of member that contains the predicate logic (property, field, method, etc.).</param>
/// <param name="MethodInfo">Reflection information about the method, property accessor, or field accessor to invoke.</param>
/// <param name="DeclaringType">The type that declares the member containing the predicate logic.</param>
public readonly record struct PredicateInfo(
    string Name,
    string? Group,
    bool IsPublic,
    PredicateType Type,
    MethodInfo MethodInfo,
    Type DeclaringType)
{
    /// <summary>
    ///     Indicates whether the predicate represents a static member.
    /// </summary>
    public bool IsStatic { get; } = Type is PredicateType.StaticField or PredicateType.StaticProperty or PredicateType.StaticMethod;

    /// <summary>
    ///     Gets a value indicating whether the predicate is a global predicate (static and public).
    /// </summary>
    public bool IsGlobalPredicate => IsStatic && IsPublic;

    /// <summary>
    ///     Gets the unique key for the predicate based on its name and group.
    /// </summary>
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
