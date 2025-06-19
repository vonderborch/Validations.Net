namespace Validations.Net;

/// <summary>
///     An attribute used to register a predicate with a specific name and group.
///     This allows for the dynamic retrieval of predicates at runtime using the <see cref="PredicateRegistrar" />.
/// </summary>
/// <param name="name">
///     The name associated with the predicate. The name serves as a unique identifier for the predicate
///     within a specified group.
/// </param>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Delegate | AttributeTargets.Property)]
public class PredicateRegistrationAttribute(string name) : Attribute
{
    /// <summary>
    ///     Gets the name associated with the predicate registration.
    ///     The name serves as a unique identifier for the predicate within a specified group.
    /// </summary>
    public string Name { get; } = name;
}
