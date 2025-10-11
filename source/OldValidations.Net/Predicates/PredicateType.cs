namespace Validations.Net.Predicates;

/// <summary>
///     Defines the type of member that can be used as a predicate in validation attributes.
/// </summary>
public enum PredicateType
{
    /// <summary>
    ///     Indicates that the predicate is stored in an instance property.
    ///     The property must be accessible and return a boolean value.
    /// </summary>
    Property,

    /// <summary>
    ///     Indicates that the predicate is stored in a static property.
    ///     The property must be accessible and return a boolean value.
    /// </summary>
    StaticProperty,

    /// <summary>
    ///     Indicates that the predicate is stored in an instance field.
    ///     The field must be accessible and contain a boolean value.
    /// </summary>
    Field,

    /// <summary>
    ///     Indicates that the predicate is stored in a static field.
    ///     The field must be accessible and contain a boolean value.
    /// </summary>
    StaticField,

    /// <summary>
    ///     Indicates that the predicate is implemented as an instance method.
    ///     The method must be accessible and return a boolean value.
    /// </summary>
    Method,

    /// <summary>
    ///     Indicates that the predicate is implemented as a static method.
    ///     The method must be accessible and return a boolean value.
    /// </summary>
    StaticMethod
}
