# AgainstPredicate

Tests the value against a specified predicate

## Available Extension Methods

- `CheckAgainstPredicate`
- `GetValidationResultForAgainstPredicate`
- `ValidateAgainstPredicate`

## Valid Types

Any

## Check Method Parameters

### Overload 1

| Parameter Name | Type          | IsRequired | Description                             |
|----------------|---------------|------------|-----------------------------------------|
| predicate      | Func<T, bool> | Yes        | The predicate to test the value against |

Example:

```csharp

var result = 32.CheckAgainstPredicate(x => x > 31);

```

## GetValidationResultFor and Validate Method Parameters

### Overload 1

| Parameter Name | Type          | IsRequired | Description                                                 |
|----------------|---------------|------------|-------------------------------------------------------------|
| predicate      | Func<T, bool> | Yes        | The predicate to test the value against                     |
| variableName   | string        | Yes        | The name of the variable being tested                       |
| blackboard     | Blackboard?   | No         | An optional blackboard object containing additional context |

Example:

```csharp

var result = 32.GetValidationResultForIsValid(x => x > 31, "my variable");
32.ValidateIsValid(x => x > 31, "my variable");

```

## ValidationAttributes

### ValidateAgainstPredicateAttribute

NOTE: Predicates must be a method, field, or property with a
[PredicateRegistrationAttribute](../PredicateRegistrationAttribute.md) attribute.

#### Constructor 1

| Parameter Name | Type        | IsRequired | Description                                                                          |
|----------------|-------------|------------|--------------------------------------------------------------------------------------|
| predicateName  | string      | Yes        | The name of the predicate                                                            |
| predicateGroup | string?     | No         | The optional name of the group for the predicate. Can be used to organize predicates |

Example:

```csharp

public class Mock
{
    [ValidateAgainstPredicate<string>("NameIsJohn")]
    public string Name { get; set; }
    
    [PredicateRegistrationAttribute("NameIsJohn")]
    public static bool NameIsJohn(string name) {
        return name == "John"
    }
}

```
