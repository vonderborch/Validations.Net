# Validations.Net -- LLM API Reference

This document is a complete reference for the Validations.Net library, written for AI coding assistants. It covers every pattern, type, validator, attribute, and integration point so you can generate correct code on the first try.

## Quick Facts

- **Package**: `Validations.Net` (NuGet)
- **Target**: `net9.0`
- **Nullable**: enabled
- **Dependencies**: `SimpleBlackboard.Net`, `Singletons.Net`
- **Root namespace**: `Validations.Net`

## Architecture Overview

The library has four layers. Every validator follows the same three-method pattern.

```
Layer 1: Validators (extension methods on base types)
  └─ Check* → bool
  └─ Validate* → ValidationResult
  └─ Ensure* → value or throws ValidationException

Layer 2: ValidationAttributes (declarative, on classes/properties/fields)
  └─ Run via CheckIsValid / ValidateIsValid / EnsureIsValid

Layer 3: ValidationSets (fluent builder for validation pipelines)
  └─ Build → Execute / Check / Ensure

Layer 4: Predicates (registered named predicates for AgainstPredicate)
  └─ [PredicateRegistration("name")] on methods/fields/properties
```

## Namespaces

| Namespace | Contains |
|-----------|----------|
| `Validations.Net` | Core types: `ValidationResult`, `AggregateValidationResult`, `ValidationFailure`, `ValidationException`, `ValidationContext`, `ValidationAttribute`, `PredicateRegistrationAttribute` |
| `Validations.Net.Validators` | General validators (null, equality, numeric, string, collection, containment, sequence, type-level) |
| `Validations.Net.Validators.Age` | `IsValidAge`, `IsAdult`, `IsMinor`, `IsWithinAgeRange` |
| `Validations.Net.Validators.DataFormat` | `IsValidJson`, `IsValidXml`, `IsValidBase64`, `IsValidEmail` + negations |
| `Validations.Net.Validators.DateTime` | `IsBusinessDay`, `IsWeekend`, `IsWithinDateRange`, `IsInPast`, `IsInFuture`, `IsDateOnly`, `IsTimeOnly`, `IsUtc`, `IsLocal`, `IsLeapYear` |
| `Validations.Net.Validators.FileSystem` | `DoesFileExist`, `DoesDirectoryExist` + negations |
| `Validations.Net.Validators.Finance` | `IsValidIban`, `IsValidBicSwift` |
| `Validations.Net.Validators.Geography` | `IsValidLatitude`, `IsValidLongitude` |
| `Validations.Net.Validators.Identifiers` | `IsValidGuid`, `IsNotValidGuid`, `IsValidId` |
| `Validations.Net.Validators.Network` | `IsValidIpAddress`, `IsValidHostname` |
| `Validations.Net.Validators.Phone` | `IsValidPhoneNumber` |
| `Validations.Net.Validators.Security` | `IsSecurePassword` |
| `Validations.Net.Validators.Streams` | `CanRead`, `CanWrite`, `CanSeek` |
| `Validations.Net.Validators.Version` | `IsValidSemanticVersion`, `IsCompatibleVersion` |
| `Validations.Net.Validators` | General validation attributes |
| `Validations.Net.Validators.{Category}` | Specialized attributes (mirrors `Validators` structure) |
| `Validations.Net.ValidationSets` | `ValidationSet`, `ValidationSetBuilder<T>` |
| `Validations.Net.Predicates` | `PredicateManager`, `PredicateInfo`, `TypePredicates` |

## Core Types

### ValidationResult

A `record struct` returned by all `Validate*` methods (except type-level which returns `AggregateValidationResult`).

```csharp
public record struct ValidationResult
{
    public readonly bool IsValid;
    public readonly ValidationException? ValidationException;
    public readonly PredicateException? PredicateException;
    public readonly string? ExceptionMessage;
}
```

**Usage pattern:**
```csharp
var result = value.ValidateIsNotNull();
if (!result.IsValid)
{
    string msg = result.ExceptionMessage;          // human-readable error
    string validator = result.ValidationException.Validator;  // e.g. "IsNotNull"
    string? param = result.ValidationException.ParameterName; // auto-captured via CallerArgumentExpression
}
```

### AggregateValidationResult

Returned by `ValidateIsValid`, `ValidationSet.Execute`, and any multi-failure validation.

```csharp
public sealed class AggregateValidationResult
{
    public bool IsValid { get; }
    public IReadOnlyList<ValidationFailure> Failures { get; }
    public Dictionary<string, string[]> ToDictionary();       // ASP.NET ModelState shape
    public IReadOnlyList<string> GetAllErrorMessages();       // flat list of messages
}
```

### ValidationFailure

```csharp
public readonly record struct ValidationFailure(string MemberPath, ValidationResult Result)
{
    public string? ErrorMessage => Result.ExceptionMessage;
}
```

Member paths use dot notation (`Address.City`) and bracket notation for collections (`Items[0].ProductId`).

### ValidationException

```csharp
public class ValidationException : Exception
{
    public IBlackboard? Blackboard { get; }
    public ValidationContext Context { get; }    // key-value context data
    public string Validator { get; }             // validator name, e.g. "IsNotNull"
    public string? ParameterName { get; }        // auto-captured parameter name
}
```

Access context data via `exception.Context.GetSnapshot()` which returns `ImmutableDictionary<string, object?>`.

### ValidationContext

Implements `IBlackboard`. Provides mutable key-value storage during validation.

```csharp
public class ValidationContext : IBlackboard
{
    public ImmutableDictionary<string, object?> GetSnapshot();
    public bool SetValue<T>(string key, T value);
    public T? GetValue<T>(string key);
    public bool TryGetValue<T>(string key, out T? value);
    public bool HasValue<T>(string key);
    public void ClearBlackboard();
    public T? RemoveValue<T>(string key);
    public bool TryRemoveValue<T>(string key, out T? value);
}
```

## The Three-Method Pattern

Every validator exposes exactly three methods. Use this pattern when generating code:

| Pattern | Prefix | Returns | When to use |
|---------|--------|---------|-------------|
| **Check** | `Check*` | `bool` | Control flow, conditions, simple branching |
| **Validate** | `Validate*` | `ValidationResult` | Need error details without throwing |
| **Ensure** | `Ensure*` | Original value | Guard clauses, want to throw on failure |

### Method Signatures (Template)

For a validator named `IsNotNull`:

```csharp
// Check -- returns bool, no exceptions
public static bool CheckIsNotNull<T>(this T? value);

// Validate -- returns ValidationResult
public static ValidationResult ValidateIsNotNull<T>(this T? value,
    IBlackboard? blackboard = null,
    string validationFailureMessage = "Value must not be null",
    [CallerArgumentExpression(nameof(value))] string? parameterName = null);

// Ensure -- returns value or throws ValidationException
public static T EnsureIsNotNull<T>(this T? value,
    IBlackboard? blackboard = null,
    string validationFailureMessage = "Value must not be null",
    [CallerArgumentExpression(nameof(value))] string? parameterName = null);
```

**Key points:**
- All validators are **extension methods** on the value being validated.
- `blackboard` is optional shared context (rarely needed).
- `validationFailureMessage` overrides the default error message.
- `parameterName` is auto-captured by the compiler -- do not pass it manually.

## Complete Validator Reference

### Null Checks

```csharp
value.CheckIsNull()           // true if value is null
value.CheckIsNotNull()        // true if value is not null
```

### Equality & Comparison

```csharp
value.CheckIsEquals(expected)                        // IEquatable<T>
value.CheckIsEquals(expected, tolerance)              // INumber<T>, approximate equality
value.CheckIsNotEquals(expected)                      // IEquatable<T>
value.CheckIsGreaterThan(comparand)                   // IComparable<T>
value.CheckIsGreaterThanOrEquals(comparand)            // IComparable<T>
value.CheckIsLessThan(comparand)                      // IComparable<T>
value.CheckIsLessThanOrEquals(comparand)               // IComparable<T>
value.CheckIsInRange(min, max, minInclusive, maxInclusive)  // IComparable<T>, defaults: inclusive
value.CheckIsNotInRange(min, max, minInclusive, maxInclusive)
value.CheckIsOneOf(params T[] allowedValues)           // or IEnumerable<T>
value.CheckIsNotOneOf(params T[] disallowedValues)
value.CheckIsDefault()                                 // equals default(T)
value.CheckIsNotDefault()
value.CheckIsSameAs(other)                             // ReferenceEquals
value.CheckIsNotSameAs(other)
```

### Numeric (`T : INumber<T>` unless noted)

```csharp
value.CheckIsPositive()          // > 0
value.CheckIsNegative()          // < 0
value.CheckIsZero()              // == 0
value.CheckIsNonZero()           // != 0
value.CheckIsEven()              // IBinaryInteger<T>
value.CheckIsOdd()               // IBinaryInteger<T>
value.CheckIsDivisibleBy(divisor) // IBinaryInteger<T>
value.CheckIsFinite()            // INumber<T>, not NaN/infinity
value.CheckIsNaN()               // INumber<T>
value.CheckIsCreditCard()        // string, Luhn algorithm
```

### String

```csharp
value.CheckIsNullOrWhiteSpace()
value.CheckIsNotNullOrWhiteSpace()
value.CheckIsWhiteSpace()          // non-null, all whitespace
value.CheckIsNotWhiteSpace()
value.CheckIsNullOrEmpty()
value.CheckIsNotNullOrEmpty()
value.CheckIsMatch(pattern)        // Regex pattern (string or Regex)
value.CheckIsNotMatch(pattern)
value.CheckIsValidUri(uriKind)     // default: UriKind.Absolute
value.CheckStartsWith(prefix, comparison)
value.CheckDoesNotStartWith(prefix, comparison)
value.CheckEndsWith(suffix, comparison)
value.CheckDoesNotEndWith(suffix, comparison)
value.CheckMinLength(minLength)
value.CheckMaxLength(maxLength)
```

### Collection & Sequence

```csharp
collection.CheckIsEmpty()              // string, ICollection<T>, IEnumerable
collection.CheckIsNotEmpty()
collection.CheckIsLength(length)       // string or ICollection<T>, exact length
collection.CheckIsLength(length, mode) // LengthCheckMode: ExactLength, LessThan, etc.
collection.CheckIsLength(min, max)     // range overload
collection.CheckIsNotLength(length)
enumerable.CheckIsCount(count)         // IEnumerable<T>, short-circuits
enumerable.CheckIsNotCount(count)
collection.CheckIsSingle()             // exactly one element
collection.CheckIsNotSingle()
collection.CheckIsSubsetOf(target)
collection.CheckIsNotSubsetOf(target)
collection.CheckIsSupersetOf(target)
collection.CheckIsNotSupersetOf(target)
collection.CheckIsSorted()             // IComparable elements
collection.CheckIsNotSorted()
collection.CheckIsDistinct()
collection.CheckIsNotDistinct()
```

### Containment

```csharp
// Collections
collection.CheckDoesContain(item)
collection.CheckDoesNotContain(item)
collection.CheckDoesContainAny(candidates)
collection.CheckDoesNotContainAny(candidates)
collection.CheckDoesContainAll(required)
collection.CheckDoesNotContainAll(required)

// Strings (substring checks)
str.CheckDoesContain(substring, comparison)
str.CheckDoesNotContain(substring, comparison)
str.CheckDoesContainAny(substrings, comparison)
str.CheckDoesNotContainAny(substrings, comparison)
str.CheckDoesContainAll(substrings, comparison)
str.CheckDoesNotContainAll(substrings, comparison)
```

### Type Checks

```csharp
value.CheckIsAssignableTo<TTarget>()
value.CheckIsInstanceOf<TTarget>()
value.CheckIsElementOf(collection)     // value is in collection
value.CheckIsNotElementOf(collection)
```

### Type-Level Validation

```csharp
instance.CheckIsValid()       // bool -- all ValidationAttributes pass
instance.ValidateIsValid()    // AggregateValidationResult
instance.EnsureIsValid()      // returns instance or throws

instance.CheckIsNotValid()    // bool -- at least one attribute fails
instance.ValidateIsNotValid() // AggregateValidationResult
```

### Predicate-Based

```csharp
// Inline predicate (lambda)
value.CheckAgainstPredicate(v => v > 0 && v < 100)
value.ValidateAgainstPredicate(v => v > 0 && v < 100)
value.EnsureAgainstPredicate(v => v > 0 && v < 100)

// Named predicate (registered via [PredicateRegistration])
value.CheckAgainstPredicate("predicateName", predicateGroup: null, predicateInstance: null)
value.ValidateAgainstPredicate("predicateName")
value.EnsureAgainstPredicate("predicateName")
```

### Age Validators

```csharp
age.CheckIsValidAge()                                // int, 0-150
age.CheckIsAdult(adultAge: 18)                       // int, DateTime, DateTimeOffset
dateOfBirth.CheckIsAdult(adultAge: 18)
age.CheckIsMinor(adultAge: 18)                       // int, DateTime, DateTimeOffset
age.CheckIsWithinAgeRange(minAge: 13, maxAge: 65)    // int, DateTime, DateTimeOffset
```

### Data Format Validators

```csharp
str.CheckIsValidJson()          str.CheckIsNotValidJson()
str.CheckIsValidXml()           str.CheckIsNotValidXml()
str.CheckIsValidBase64()        str.CheckIsNotValidBase64()
str.CheckIsValidEmail()         // basic format check, not RFC 5322
```

### DateTime Validators

```csharp
dt.CheckIsBusinessDay()         // Monday-Friday
dt.CheckIsWeekend()             // Saturday-Sunday
dt.CheckIsWithinDateRange(min, max)
dt.CheckIsInPast()              // before UTC now
dt.CheckIsInFuture()            // after UTC now
dt.CheckIsDateOnly()            // time component is midnight
dt.CheckIsTimeOnly()            // date component is DateTime.MinValue.Date
dt.CheckIsUtc()                 // DateTime.Kind == Utc, or DateTimeOffset.Offset == Zero
dt.CheckIsLocal()               // DateTime.Kind == Local
year.CheckIsLeapYear()          // int, DateTime, DateTimeOffset
```

### File System Validators

```csharp
path.CheckDoesFileExist()         path.CheckDoesFileNotExist()
path.CheckDoesDirectoryExist()    path.CheckDoesDirectoryNotExist()
```

### Finance Validators

```csharp
str.CheckIsValidIban()          // basic IBAN format (15-34 chars, country+check+account)
str.CheckIsValidBicSwift()      // BIC/SWIFT code (8 or 11 chars)
```

### Geography Validators

```csharp
value.CheckIsValidLatitude()    // double, -90 to 90
value.CheckIsValidLongitude()   // double, -180 to 180
```

### Identifier Validators

```csharp
str.CheckIsValidGuid()          str.CheckIsNotValidGuid()
str.CheckIsValidId(minLength: 1, maxLength: 255, allowedPattern: @"^[a-zA-Z0-9_-]+$")
```

### Network Validators

```csharp
str.CheckIsValidIpAddress()     // IPv4 or IPv6
str.CheckIsValidHostname()      // RFC 952/1123
```

### Phone Validators

```csharp
str.CheckIsValidPhoneNumber()   // basic format, 7-15 digits, optional +prefix
```

### Security Validators

```csharp
str.CheckIsSecurePassword(
    minLength: 8,
    requireUppercase: true,
    requireLowercase: true,
    requireDigit: true,
    requireSpecialChar: true)
```

### Stream Validators

```csharp
stream.CheckCanRead()
stream.CheckCanWrite()
stream.CheckCanSeek()
```

### Version Validators

```csharp
str.CheckIsValidSemanticVersion()   // SemVer 2.0 (major.minor.patch[-pre][+build])
str.CheckIsCompatibleVersion(baseVersion)
```

## ValidationAttributes

Attributes decorate classes, properties, and fields. They are evaluated by `CheckIsValid` / `ValidateIsValid` / `EnsureIsValid`.

### Usage Pattern

```csharp
using Validations.Net;
using Validations.Net.Validators;

public class CreateUserRequest
{
    [ValidateIsNotNullOrWhiteSpace(Message = "Username is required")]
    [ValidateMinLength(3)]
    public string Username { get; set; } = "";

    [ValidateIsValidEmail(Message = "Invalid email")]
    public string Email { get; set; } = "";

    [ValidateIsSecurePassword(minLength: 8)]
    public string Password { get; set; } = "";

    [ValidateIsInRange(13, 120)]
    public int Age { get; set; }
}

// Validate
var result = request.ValidateIsValid();
if (!result.IsValid)
{
    Dictionary<string, string[]> errors = result.ToDictionary();
    return ValidationProblem(new ValidationProblemDetails(errors));
}
```

### Naming Convention

Every attribute follows: `Validate{ValidatorName}Attribute`. The `Attribute` suffix can be omitted in usage:

| Validator | Attribute usage |
|-----------|----------------|
| `IsNotNull` | `[ValidateIsNotNull]` |
| `IsInRange` | `[ValidateIsInRange(min, max)]` |
| `IsValidEmail` | `[ValidateIsValidEmail]` |
| `IsSecurePassword` | `[ValidateIsSecurePassword(minLength: 8)]` |

### Structural Attributes

These control validation flow, not value validation:

```csharp
// Recursively validate nested objects
[ValidateNested]
public Address? Address { get; set; }

// Validate each element in a collection
[ValidateEachIsValid]
public List<OrderItem> Items { get; set; } = new();
```

### Custom Messages

```csharp
[ValidateIsNotNull(Message = "Customer ID is required")]
public string? CustomerId { get; set; }
```

### Multiple Attributes

Multiple attributes on the same member are all evaluated (no short-circuit):

```csharp
[ValidateIsNotNullOrWhiteSpace]
[ValidateMinLength(3)]
[ValidateMaxLength(50)]
public string Name { get; set; } = "";
```

## ValidationSets (Fluent Builder)

Build reusable validation pipelines that collect all failures.

```csharp
using Validations.Net.ValidationSets;

var set = ValidationSet
    .For<User>()
    .AddIsNotNull()
    .AddIsNotNullOrWhiteSpace(u => u.Name, message: "Name is required")
    .AddIsNotNullOrEmpty(u => u.Email, message: "Email is required")
    .AddIsInRange(u => u.Age, 0, 150, minInclusive: true, maxInclusive: true)
    .AddFromType()     // also run ValidationAttributes on the type
    .Build();          // returns immutable ValidationSet<User>
```

### Builder Methods

```csharp
// Custom step (full control)
builder.Add((value, blackboard) => {
    // return ValidationResult.CreateFromValidationSuccess() or
    // return ValidationResult.CreateFromValidationFailure(...)
});

// Convenience methods
builder.AddIsNotNull(message?)
builder.AddIsNotNull<TMember>(selector, message?)
builder.AddIsInRange<TValue>(selector, min, max, minInclusive?, maxInclusive?, message?)
builder.AddIsNotNullOrEmpty(selector, message?)
builder.AddIsNotNullOrWhiteSpace(selector, message?)
builder.AddFromType()    // run all ValidationAttributes on the type

// Conditional steps
builder.When(predicate)  // subsequent steps only run if predicate returns true
builder.EndWhen()        // clear condition
```

### Execution

```csharp
AggregateValidationResult result = set.Execute(value);
bool ok = set.Check(value);
T validated = set.Ensure(value);  // throws ValidationException on failure
```

### Conditional Validation

```csharp
var set = ValidationSet
    .For<Order>()
    .AddIsNotNull()
    .When(o => o.RequiresShipping)
        .AddIsNotNull(o => o.ShippingAddress, "Shipping address required")
        .AddIsNotNullOrWhiteSpace(o => o.ShippingAddress!.Street)
    .EndWhen()
    .AddFromType()
    .Build();
```

## Predicate System

Register named predicates with `[PredicateRegistration]`, then validate against them.

### Registering Predicates

```csharp
using Validations.Net;

public class OrderValidation
{
    [PredicateRegistration("HasItems")]
    public static bool HasItems(Order order) => order.Items.Count > 0;

    [PredicateRegistration("TotalInRange", "Financial")]
    public static bool TotalInRange(Order order) => order.Total >= 0 && order.Total <= 1_000_000;
}
```

Predicates can be methods, fields (`Func<T, bool>`), or properties (`Func<T, bool>`). They can be static (global) or instance-scoped.

### Using Predicates

```csharp
// Extension method
bool ok = order.CheckAgainstPredicate("HasItems");
var result = order.ValidateAgainstPredicate("HasItems");

// With group
var result = order.ValidateAgainstPredicate("TotalInRange", predicateGroup: "Financial");

// As attribute
public class Order
{
    [ValidateAgainstPredicate("HasItems")]
    public List<OrderItem> Items { get; set; } = new();
}
```

## Common Patterns

### Guard Clauses

```csharp
public void ProcessOrder(Order? order)
{
    order.EnsureIsNotNull();
    order.CustomerId.EnsureIsNotNullOrWhiteSpace(
        validationFailureMessage: "Customer ID is required");
    order.Total.EnsureIsInRange(0m, 1_000_000m);
}
```

### ASP.NET Controller Validation

```csharp
[HttpPost]
public IActionResult Create([FromBody] CreateRequest request)
{
    var result = request.ValidateIsValid();
    if (!result.IsValid)
        return ValidationProblem(new ValidationProblemDetails(result.ToDictionary()));
    
    // proceed with valid request
    return Ok();
}
```

### Combining Attributes with ValidationSets

```csharp
public class User
{
    [ValidateIsNotNullOrWhiteSpace]
    public string Name { get; set; } = "";

    [ValidateIsInRange(0, 150)]
    public int Age { get; set; }
}

var set = ValidationSet
    .For<User>()
    .AddIsNotNull()
    .Add((user, bb) => {
        if (user.Age < 18 && string.IsNullOrEmpty(user.ParentConsent))
            return ValidationResult.CreateFromValidationFailure(
                "ParentConsent", "Minors must have parent consent",
                null, bb, new List<(string, object?)> { ("value", user) });
        return ValidationResult.CreateFromValidationSuccess();
    })
    .AddFromType()   // runs [ValidateIsNotNullOrWhiteSpace] and [ValidateIsInRange]
    .Build();
```

### Nested + Collection Validation

```csharp
public class Address
{
    [ValidateIsNotNullOrWhiteSpace]
    public string Street { get; set; } = "";

    [ValidateIsNotNullOrWhiteSpace]
    public string City { get; set; } = "";
}

public class OrderItem
{
    [ValidateIsNotNullOrWhiteSpace]
    public string ProductId { get; set; } = "";

    [ValidateIsGreaterThan(0)]
    public int Quantity { get; set; }
}

public class Order
{
    [ValidateIsNotNull]
    [ValidateNested]
    public Address? ShippingAddress { get; set; }

    [ValidateIsNotEmpty]
    [ValidateEachIsValid]
    public List<OrderItem> Items { get; set; } = new();
}

var result = order.ValidateIsValid();
// Possible failure paths: "ShippingAddress.Street", "Items[0].ProductId", "Items[2].Quantity"
```

## LengthCheckMode Enum

Used with `IsLength` overloads:

```csharp
public enum LengthCheckMode
{
    ExactLength,
    LessThan,
    LessThanOrEqual,
    GreaterThan,
    GreaterThanOrEqual
}

// Usage
str.CheckIsLength(10, LengthCheckMode.LessThanOrEqual);  // length <= 10
```

## Error Message Customization

Every `Validate*` and `Ensure*` method accepts `validationFailureMessage`:

```csharp
value.EnsureIsNotNull(validationFailureMessage: "Order ID cannot be null");
value.ValidateIsInRange(1, 100, validationFailureMessage: "Quantity must be between 1 and 100");
```

For attributes, set the `Message` property:

```csharp
[ValidateIsNotNull(Message = "Order ID cannot be null")]
```

## IBlackboard Integration

All `Validate*` and `Ensure*` methods accept an optional `IBlackboard? blackboard` parameter for passing shared context through a validation pipeline. The blackboard is from the `SimpleBlackboard.Net` package and provides typed key-value storage.

```csharp
var bb = new Blackboard();
bb.SetValue("requestId", "abc-123");

var result = value.ValidateIsNotNull(blackboard: bb);
// The blackboard is available on result.ValidationException.Blackboard
```

## Key Behavioral Notes

1. **No short-circuit**: All validators in an attribute chain or ValidationSet run; failures are collected.
2. **Null handling**: Most `Check*` methods return `false` for null inputs (not an exception). Notable exceptions: `CheckIsNull` returns `true` for null.
3. **Thread safety**: `ValidationContext` is NOT thread-safe. `PredicateManager` uses internal locking. `TypeValidationInfo` uses `ConcurrentDictionary` for caching.
4. **Performance**: Compiled expression trees for member access, `AggressiveInlining` on hot paths, lazy allocation in `AggregateValidationResult`.
5. **CallerArgumentExpression**: `parameterName` is auto-captured at call sites. Do not pass it manually unless you want to override it.
6. **IsInRange**: Throws `ArgumentException` if `min > max`.
