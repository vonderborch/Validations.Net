# IsValid

Checks for if a class or struct is valid, used with `ValidationAttributes` attached to a class or struct's fields or
properties

## Available Extension Methods

- `CheckIsValid`
- `GetValidationResultForIsValid`
- `ValidateIsValid`

## Valid Types

- `class`
- `struct`

## Check Method Parameters

### Overload 1

n/a

Example:

```csharp

[ValidateIsNotNull]
public class Person
{
    [ValidateIsEquals("John")]
    public string Name { get; set; }
    
    [ValidateGreaterThan(18)]
    public int Age { get; set; }
}

Person person = new("John", 19);
var result = person.CheckIsValid();

```

## GetValidationResultFor and Validate Method Parameters

### Overload 1

| Parameter Name | Type          | IsRequired | Description                                                 |
|----------------|---------------|------------|-------------------------------------------------------------|
| variableName   | string        | Yes        | The name of the variable being tested                       |
| blackboard     | Blackboard?   | No         | An optional blackboard object containing additional context |

Example:

```csharp

[ValidateIsNotNull]
public class Person
{
    [ValidateIsEquals("John")]
    public string Name { get; set; }
    
    [ValidateGreaterThan(18)]
    public int Age { get; set; }
}

Person person = new("John", 19);
var result = person.GetValidationResultForIsValid(nameof(person));
person.ValidateIsValid(nameof(person));

```

## ValidationAttributes

n/a
