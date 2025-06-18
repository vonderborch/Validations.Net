using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateDoesNotContainAnyAttribute : ValidationAttribute
{
    public ICollection<string>? Substrings { get; init; } = null;

    public ICollection<char>? Characters { get; init; } = null;
    
    public StringComparison Comparison { get; }

    public ValidateDoesNotContainAnyAttribute(StringComparison comparison, params string[] subStrings) : base("DoesNotContainAny")
    {
        Comparison = comparison;
        Substrings = subStrings;
    }

    public ValidateDoesNotContainAnyAttribute(StringComparison comparison, params char[] characters) : base("DoesNotContainAny")
    {
        Comparison = comparison;
        Characters = characters;
    }


    public override bool Check(object? value)
    {
        string str = GetCorrectType<string>(value, nameof(value));
        if (Characters is null)
        {
            return str.CheckDoesNotContainAny(Substrings!, Comparison);
        }
        else
        {
            return str.CheckDoesNotContainAny(Characters, Comparison);
        }
    }

    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        string str = GetCorrectType<string>(value, nameof(propertyName));
        if (Characters is null)
        { 
            str.ValidateDoesNotContainAny(Substrings!, propertyName, Comparison, blackboard);
        }
        else
        { 
            str.ValidateDoesNotContainAny(Characters, propertyName, Comparison, blackboard);
        }
    }
}
