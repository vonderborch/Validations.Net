using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateDoesContainAllAttribute : ValidationAttribute
{
    public ICollection<string>? Substrings { get; init; } = null;

    public ICollection<char>? Characters { get; init; } = null;

    public StringComparison Comparison { get; }

    public ValidateDoesContainAllAttribute(StringComparison comparison, params string[] subStrings) : base("DoesContainAll")
    {
        Comparison = comparison;
        Substrings = subStrings;
    }

    public ValidateDoesContainAllAttribute(StringComparison comparison, params char[] characters) : base("DoesContainAll")
    {
        Comparison = comparison;
        Characters = characters;
    }


    public override bool Check(object? value)
    {
        string str = GetCorrectType<string>(value, nameof(value));
        if (Characters is null)
        {
            return str.CheckDoesContainAll(Substrings!, Comparison);
        }
        else
        {
            return str.CheckDoesContainAll(Characters, Comparison);
        }
    }

    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        string str = GetCorrectType<string>(value, nameof(propertyName));
        if (Characters is null)
        { 
            str.ValidateDoesContainAll(Substrings!, propertyName, Comparison, blackboard);
        }
        else
        { 
            str.ValidateDoesContainAll(Characters, propertyName, Comparison, blackboard);
        }
    }
}
