# Getting Started with Validations.Net

This guide introduces Validations.Net and walks through the three main usage patterns: inline validators, ValidationAttributes, and ValidationSets.

## Installation

Install the package from NuGet:

```bash
dotnet add package Validations.Net
```

Or add a package reference to your project file:

```xml
<PackageReference Include="Validations.Net" Version="1.0.0" />
```

## The Three Usage Patterns

Every validator in Validations.Net exposes three method variants:

| Pattern | Method | Returns | Behavior |
|---------|--------|---------|----------|
| **Check** | `Check*` | `bool` | Returns `true` if valid, `false` otherwise. No exceptions. |
| **Validate** | `Validate*` | `ValidationResult` or `AggregateValidationResult` | Returns a result object with success/failure details. No exceptions. |
| **Ensure** | `Ensure*` | Original value | Returns the value if valid; throws `ValidationException` if invalid. |

### Check Methods

Use `Check*` when you need a simple boolean for control flow:

```csharp
using Validations.Net;
using Validations.Net.Validators;

string? name = GetUserName();
if (!name.CheckIsNotNullOrWhiteSpace())
{
    return BadRequest("Name is required");
}

int age = GetAge();
if (!age.CheckIsInRange(0, 150))
{
    return BadRequest("Age must be between 0 and 150");
}
```

### Validate Methods

Use `Validate*` when you need detailed failure information without throwing:

```csharp
var result = name.ValidateIsNotNullOrWhiteSpace();
if (!result.IsValid)
{
    // result.ExceptionMessage contains the error message
    // result.ValidationException has full context
    return BadRequest(result.ExceptionMessage ?? "Validation failed");
}

// For type-level validation (ValidationAttributes), returns AggregateValidationResult
var user = new User { Name = "", Age = -1 };
var aggregateResult = user.ValidateIsValid();
if (!aggregateResult.IsValid)
{
    foreach (var failure in aggregateResult.Failures)
    {
        Console.WriteLine($"{failure.MemberPath}: {failure.ErrorMessage}");
    }
}
```

### Ensure Methods

Use `Ensure*` when validation failure should throw an exception:

```csharp
// Throws ValidationException if name is null or whitespace
string validatedName = name.EnsureIsNotNullOrWhiteSpace();

// With custom message
string validatedName = name.EnsureIsNotNullOrWhiteSpace(
    validationFailureMessage: "Customer name is required");

// For type-level validation
User validatedUser = user.EnsureIsValid();
```

## ValidationResult Explained

`ValidationResult` is a record struct returned by `Validate*` methods (except `ValidateIsValid`):

```csharp
public record struct ValidationResult
{
    public readonly bool IsValid;                    // true if validation passed
    public readonly ValidationException? ValidationException;  // exception if failed
    public readonly PredicateException? PredicateException;    // for predicate failures
    public readonly string? ExceptionMessage;        // convenience: exception message
}
```

Example:

```csharp
var result = value.ValidateIsNotNull();
if (result.IsValid)
{
    // Proceed
}
else
{
    string msg = result.ExceptionMessage;  // "Parameter must not be null"
    var ex = result.ValidationException;   // Full exception with Validator, ParameterName, Context
}
```

## Quick Examples by Pattern

### Inline Validators

```csharp
// Null check
bool ok = value.CheckIsNotNull();
var result = value.ValidateIsNotNull();
var ensured = value.EnsureIsNotNull();

// String validation
bool ok = email.CheckIsNotNullOrWhiteSpace();
var result = email.ValidateIsNotNullOrWhiteSpace();

// Numeric range
bool ok = count.CheckIsInRange(1, 100);
var result = count.ValidateIsInRange(1, 100);

// Collection
bool ok = items.CheckIsNotEmpty();
var result = items.ValidateIsNotEmpty();
```

### ValidationAttributes with IsValid / IsNotValid

Decorate your types with attributes, then use `ValidateIsValid` or `CheckIsValid`:

```csharp
public class User
{
    [ValidateIsNotNull]
    [ValidateIsNotNullOrWhiteSpace]
    public string Name { get; set; } = "";

    [ValidateIsInRange(0, 150)]
    public int Age { get; set; }
}

// Usage
var user = new User { Name = "Alice", Age = 25 };
bool valid = user.CheckIsValid();

var result = user.ValidateIsValid();
if (!result.IsValid)
{
    foreach (var f in result.Failures)
        Console.WriteLine($"{f.MemberPath}: {f.ErrorMessage}");
}

User ensured = user.EnsureIsValid();  // Throws if invalid
```

### ValidationSets

Build a fluent validation pipeline:

```csharp
var set = ValidationSet
    .For<User>()
    .AddIsNotNull()
    .AddIsNotNullOrWhiteSpace(u => u.Name, message: "Name is required")
    .AddIsInRange(u => u.Age, 0, 150)
    .AddFromType()   // Also run ValidationAttributes
    .Build();

// Execute and collect all failures
var result = set.Execute(user);
if (!result.IsValid)
{
    var errors = result.ToDictionary();  // For ModelState
}

// Or check/ensure
bool ok = set.Check(user);
User validated = set.Ensure(user);
```

## Custom Error Messages

Most validators accept an optional `validationFailureMessage` parameter:

```csharp
value.EnsureIsNotNull(validationFailureMessage: "Customer ID is required");
email.ValidateIsNotNullOrWhiteSpace(validationFailureMessage: "Email address is required");
```

For ValidationAttributes, set the `Message` property:

```csharp
[ValidateIsNotNull(Message = "Customer ID is required")]
public string? CustomerId { get; set; }

[ValidateIsInRange(0, 150, Message = "Age must be between 0 and 150")]
public int Age { get; set; }
```

## Nested Validation

Use `[ValidateNested]` to recursively validate child objects:

```csharp
public class Address
{
    [ValidateIsNotNullOrWhiteSpace]
    public string Street { get; set; } = "";

    [ValidateIsNotNullOrWhiteSpace]
    public string City { get; set; } = "";
}

public class Customer
{
    [ValidateIsNotNull]
    [ValidateNested]
    public Address? Address { get; set; }
}

var customer = new Customer { Address = new Address { Street = "", City = "" } };
var result = customer.ValidateIsValid();
// Failures include "Address.Street", "Address.City"
```

## Collection Validation

Use `[ValidateEachIsValid]` to validate each element of a collection:

```csharp
public class OrderItem
{
    [ValidateIsNotNullOrWhiteSpace]
    public string ProductId { get; set; } = "";

    [ValidateIsGreaterThan(0)]
    public int Quantity { get; set; }
}

public class Order
{
    [ValidateEachIsValid]
    public List<OrderItem> Items { get; set; } = new();
}

var order = new Order
{
    Items = new List<OrderItem>
    {
        new() { ProductId = "", Quantity = 0 },
        new() { ProductId = "P1", Quantity = 2 }
    }
};
var result = order.ValidateIsValid();
// Failures include "Items[0].ProductId", "Items[0].Quantity"
```

## Next Steps

- [Validation Attributes](ValidationAttributes.md) — Declarative validation in depth
- [Validation Sets](ValidationSets.md) — Fluent builder API
- [Aggregate Validation Result](AggregateValidationResult.md) — Working with validation results and ASP.NET integration
