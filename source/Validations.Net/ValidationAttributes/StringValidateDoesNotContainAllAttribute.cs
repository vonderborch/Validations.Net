using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateDoesNotContainAllAttribute : ValidationAttribute
{
    public ICollection<string>? Substrings { get; init; } = null;

    public ICollection<char>? Characters { get; init; } = null;

    public StringComparison Comparison { get; }

    public ValidateDoesNotContainAllAttribute(StringComparison comparison, params string[] subStrings) : base("DoesNotContainAll")
    {
        Comparison = comparison;
        Substrings = subStrings;
    }

    public ValidateDoesNotContainAllAttribute(StringComparison comparison, params char[] characters) : base("DoesNotContainAll")
    {
        Comparison = comparison;
        Characters = characters;
    }


    public override bool Check(object? value)
    {
        string str = GetCorrectType<string>(value, nameof(value));
        if (Characters is null)
        {
            return str.CheckDoesNotContainAll(Substrings!, Comparison);
        }
        else
        {
            return str.CheckDoesNotContainAll(Characters, Comparison);
        }
    }

    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        string str = GetCorrectType<string>(value, nameof(propertyName));
        if (Characters is null)
        { 
            str.ValidateDoesNotContainAll(Substrings!, propertyName, Comparison, blackboard);
        }
        else
        { 
            str.ValidateDoesNotContainAll(Characters, propertyName, Comparison, blackboard);
        }
    }
}
