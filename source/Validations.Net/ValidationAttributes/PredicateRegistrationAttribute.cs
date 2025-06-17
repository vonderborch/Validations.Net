namespace Validations.Net.ValidationAttributes;

public class PredicateRegistrationAttribute(string name, string group = "default") : Attribute
{
    public string Name { get; } = name;

    public string Group { get; } = group;
}
