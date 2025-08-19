namespace Validations.Net;

/// <summary>
///     An attribute used to register a predicate with a specific name and group.
///     This allows for the dynamic retrieval of predicates at runtime using the <see cref="PredicateRegistrar" />.
/// </summary>
/// <param name="name">
///     The name associated with the predicate. The name serves as a unique identifier for the predicate
///     within a specified group.
/// </param>
/// <summary>
///     Gets the group associated with the predicate registration.
///     The group is an optional categorization that helps in organizing predicates
///     and aids in retrieval based on the group context.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Delegate | AttributeTargets.Property | AttributeTargets.Field)]
public class ValidationPredicateAttribute(string name, string? group = null) : Attribute
{
    /// <summary>
    ///     Gets the group associated with the predicate registration.
    ///     The group is an optional categorization that helps in organizing predicates
    ///     and aids in retrieval based on the group context.
    /// </summary>
    public string? Group { get; } = group;

    /// <summary>
    ///     Gets the name associated with the predicate registration.
    ///     The name serves as a unique identifier for the predicate within a specified group.
    /// </summary>
    public string Name { get; } = name;
}
