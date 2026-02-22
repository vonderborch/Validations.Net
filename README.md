# Validations.Net

![Logo](logo.png)

A library enabling validations for various types of objects in .NET as extensions to base types.

## Installation

### Nuget

[![NuGet version (Validations.Net)](https://img.shields.io/nuget/v/Validations.Net.svg?style=flat-square)](https://www.nuget.org/packages/Validations.Net/)

The recommended installation approach is to use the available nuget
package: [Validations.Net](https://www.nuget.org/packages/Validations.Net/)

### Clone

Alternatively, you can clone this repo and reference the Validations.Net project in your project.

## How to Use

There are three approaches to using the validation functionality in this library (and they can, of course, be mixed):

- **Check** extension methods: return a `bool` representing the success of the validation (e.g., `value.CheckIsNotNull()`).
- **Validate** extension methods: return a `ValidationResult` (or `AggregateValidationResult` for type-level validation) with success/failure details (e.g., `value.ValidateIsNotNull()`).
- **Ensure** extension methods: return the value if valid, or throw `ValidationException` if invalid (e.g., `value.EnsureIsNotNull()`).
- **Validation Attributes**: attributes that can be added to the members of a class or struct. Validation against types with these attributes is handled via `CheckIsValid`, `ValidateIsValid`, or `EnsureIsValid`.

**Full documentation is available in the [Docs](Docs/index.md) folder.**

### Check / Validate / Ensure Extension Methods

Every validator exposes three method variants:

| Pattern | Returns | Example |
|--------|---------|---------|
| `Check*` | `bool` | `value.CheckIsNotNull()` |
| `Validate*` | `ValidationResult` or `AggregateValidationResult` | `value.ValidateIsNotNull()` |
| `Ensure*` | Original value (or throws) | `value.EnsureIsNotNull()` |

```csharp
using Validations.Net;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

public class Program
{
    public static void Main()
    {
        var person = new Person { Name = "John", Age = 30 };

        // Check: returns bool
        if (!person.CheckIsNotNull())
        {
            Console.WriteLine("Person is null");
        }

        // Validate: returns ValidationResult
        var nameResult = person.Name.ValidateIsEquals("John");
        var ageResult = person.Age.ValidateIsGreaterThan(18);

        // Ensure: returns value or throws ValidationException
        person.EnsureIsNotNull();
        person.Name.EnsureIsEquals("John");
        person.Age.EnsureIsGreaterThan(18);
    }
}
```

### Type-Level Validation (IsValid)

For types decorated with ValidationAttributes, use `CheckIsValid`, `ValidateIsValid`, or `EnsureIsValid`:

- `CheckIsValid()` → `bool`
- `ValidateIsValid()` → `AggregateValidationResult` (with `IsValid`, `Failures`, `ToDictionary()`)
- `EnsureIsValid()` → returns value or throws `ValidationException`

`AggregateValidationResult` collects all validation failures. Use `ToDictionary()` to get a dictionary mapping member paths to error messages (ASP.NET ModelState shape).

```csharp
using Validations.Net;

[ValidateIsNotNull]
public class Person
{
    [ValidateIsEquals("John")]
    public string Name { get; set; }

    [ValidateIsGreaterThan(18)]
    public int Age { get; set; }
}

public class Program
{
    public static void Main()
    {
        var person = new Person { Name = "John", Age = 30 };

        if (!person.CheckIsValid())
        {
            Console.WriteLine("Person is not valid");
        }

        var result = person.ValidateIsValid();
        if (!result.IsValid)
        {
            foreach (var failure in result.Failures)
            {
                Console.WriteLine($"{failure.MemberPath}: {failure.ErrorMessage}");
            }
            var errorsByPath = result.ToDictionary();  // For ModelState
        }

        person.EnsureIsValid(parameterName: nameof(person));  // Throws if invalid
    }
}
```

### ValidationSets

Use the fluent `ValidationSet` builder to define validation pipelines:

```csharp
using Validations.Net.ValidationSets;

var set = ValidationSet
    .For<Person>()
    .AddIsNotNull()
    .AddIsNotNullOrWhiteSpace(p => p.Name, message: "Name is required")
    .AddIsInRange(p => p.Age, 0, 150)
    .AddFromType()   // Also run ValidationAttributes
    .Build();

var result = set.Execute(person);
if (!result.IsValid)
{
    var errors = result.ToDictionary();
}

// Or: set.Check(person) for bool, set.Ensure(person) to throw on failure
```

### Namespaces

- `Validations.Net` — core types (`ValidationResult`, `AggregateValidationResult`, `ValidationException`)
- `Validations.Net.Validators` — general validators
- `Validations.Net.Validators.{Age,DataFormat,DateTime,...}` — specialized validators
- `Validations.Net.ValidationAttributes` — general attributes
- `Validations.Net.ValidationAttributes.{Age,DataFormat,...}` — specialized attributes
- `Validations.Net.ValidationSets` — fluent builder

## Development

1. Clone or fork the repo
2. Create a new branch
3. Code!
4. Push your changes and open a PR
5. Once approved, they'll be merged in
6. Profit!

## Future Plans

See list of issues under the Milestones: https://github.com/vonderborch/Validations.Net/milestones
