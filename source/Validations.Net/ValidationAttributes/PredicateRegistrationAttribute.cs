namespace Validations.Net.ValidationAttributes;

/// <summary>
/// An attribute used to register a predicate with a specific name and group.
/// This allows for the dynamic retrieval of predicates at runtime using the <see cref="PredicateRegistrar"/>.
/// </summary>
/// <param name="name">The name associated with the predicate. The name serves as a unique identifier for the predicate within a specified group.</param>
/// <param name="group">The group associated with the predicate. The group is used to organize predicates into distinct logical collections, providing a means to differentiate and manage predicates based on their context or purpose. Defaults to `default`.</param>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Delegate)]
public class PredicateRegistrationAttribute(string name, string group = "default") : Attribute
{
    /// <summary>
    /// Gets the name associated with the predicate registration.
    /// The name serves as a unique identifier for the predicate within a specified group.
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Gets the group associated with the predicate registration.
    /// The group is used to organize predicates into distinct logical collections,
    /// providing a means to differentiate and manage predicates based on their context or purpose.
    /// </summary>
    public string Group { get; } = group;
}
