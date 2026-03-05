# Validation Attributes

ValidationAttributes provide declarative, type-level validation. Decorate your classes, properties, and fields with attributes, then run validation via `ValidateIsValid`, `CheckIsValid`, or `EnsureIsValid`.

## Base Class

All validation attributes inherit from `ValidationAttribute`:

```csharp
public abstract class ValidationAttribute(string name) : Attribute
{
    public string Name { get; }           // Validator identifier
    public string? Message { get; set; }  // Optional custom failure message
    public abstract ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null);
}
```

- **Name**: Identifies the validator (e.g., `"IsNotNull"`).
- **Message**: When set, overrides the validator's default failure message.
- **Validate**: Performs the validation and returns a `ValidationResult`.

## Decorating Members

Attributes can be applied to:

- **Classes** — class-level validation (e.g., cross-property rules)
- **Properties** — property-level validation
- **Fields** — field-level validation

```csharp
[ValidateIsNotNull]
public string? Id { get; set; }

[ValidateIsNotNullOrWhiteSpace]
[ValidateMinLength(3)]
public string Name { get; set; } = "";

[ValidateIsInRange(0, 150)]
public int Age { get; set; }
```

Multiple attributes on the same member are all evaluated. Validation does not short-circuit; all failures are collected.

## Custom Error Messages

Set the `Message` property to override the default failure message:

```csharp
[ValidateIsNotNull(Message = "Customer ID is required")]
public string? CustomerId { get; set; }

[ValidateIsInRange(0, 150, Message = "Age must be between 0 and 150")]
public int Age { get; set; }

[ValidateIsNotNullOrWhiteSpace(Message = "Email address cannot be empty")]
public string Email { get; set; } = "";
```

## Structural Attributes

Two attributes control validation flow; they do not validate values themselves.

### ValidateNested

Recursively validates a nested object using its own ValidationAttributes:

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
```

When `Customer` is validated, `Address` is validated and failures are reported with paths like `Address.Street`, `Address.City`.

### ValidateEachIsValid

Validates each element of a collection using the element type's ValidationAttributes:

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
```

Failures are reported with indexed paths like `Items[0].ProductId`, `Items[1].Quantity`.

## Complete Attribute Reference

### Null Checks

| Attribute | Description |
|-----------|-------------|
| `ValidateIsNull` | Value must be null |
| `ValidateIsNotNull` | Value must not be null |

### Equality & Comparison

| Attribute | Description |
|-----------|-------------|
| `ValidateIsEquals(object expected)` | Value equals expected |
| `ValidateIsNotEquals(object expected)` | Value does not equal expected |
| `ValidateIsGreaterThan(object comparand)` | Value > comparand |
| `ValidateIsGreaterThanOrEquals(object comparand)` | Value >= comparand |
| `ValidateIsLessThan(object comparand)` | Value < comparand |
| `ValidateIsLessThanOrEquals(object comparand)` | Value <= comparand |
| `ValidateIsInRange(object min, object max, bool minInclusive, bool maxInclusive)` | Value in [min, max] |
| `ValidateIsNotInRange(object min, object max, ...)` | Value outside range |
| `ValidateIsOneOf(params object[] values)` | Value is one of allowed |
| `ValidateIsNotOneOf(params object[] values)` | Value is not one of |
| `ValidateIsDefault` | Value equals default(T) |
| `ValidateIsNotDefault` | Value is not default |
| `ValidateIsSameAs(object? other)` | Same reference |
| `ValidateIsNotSameAs(object? other)` | Different reference |

### Numeric

| Attribute | Description |
|-----------|-------------|
| `ValidateIsEven` | Even integer |
| `ValidateIsOdd` | Odd integer |
| `ValidateIsDivisibleBy(long divisor)` | Value % divisor == 0 |
| `ValidateIsFinite` | Finite floating point |
| `ValidateIsNaN` | NaN floating point |
| `ValidateIsCreditCard` | Valid Luhn checksum |

### String

| Attribute | Description |
|-----------|-------------|
| `ValidateIsNullOrWhiteSpace` | Null, empty, or whitespace |
| `ValidateIsNotNullOrWhiteSpace` | Has non-whitespace content |
| `ValidateIsWhiteSpace` | All whitespace |
| `ValidateIsNotWhiteSpace` | Not all whitespace |
| `ValidateIsMatch(string pattern)` | Matches regex |
| `ValidateIsNotMatch(string pattern)` | Does not match regex |
| `ValidateIsValidUri(UriKind kind)` | Valid URI |
| `ValidateStartsWith(string prefix)` | Starts with prefix |
| `ValidateDoesNotStartWith(string prefix)` | Does not start with |
| `ValidateEndsWith(string suffix)` | Ends with suffix |
| `ValidateDoesNotEndWith(string suffix)` | Does not end with |
| `ValidateMinLength(int minLength)` | Length >= min |
| `ValidateMaxLength(int maxLength)` | Length <= max |

### Collection & Sequence

| Attribute | Description |
|-----------|-------------|
| `ValidateIsEmpty` | Empty |
| `ValidateIsNotEmpty` | Not empty |
| `ValidateIsNullOrEmpty` | Null or empty |
| `ValidateIsNotNullOrEmpty` | Not null and not empty |
| `ValidateIsLength(int length)` | Exact length |
| `ValidateIsNotLength(int length)` | Not exact length |
| `ValidateIsCount(int count)` | Exact count |
| `ValidateIsNotCount(int count)` | Not exact count |
| `ValidateIsSingle` | Exactly one element |
| `ValidateIsNotSingle` | Not exactly one |

### Containment

| Attribute | Description |
|-----------|-------------|
| `ValidateDoesContain(string substring)` | Contains substring |
| `ValidateDoesNotContain(string substring)` | Does not contain |
| `ValidateDoesContainAny(params string[] values)` | Contains at least one |
| `ValidateDoesNotContainAny(params string[] values)` | Contains none |
| `ValidateDoesContainAll(params string[] values)` | Contains all |
| `ValidateDoesNotContainAll(params string[] values)` | Missing at least one |

### Sequence Order & Type

| Attribute | Description |
|-----------|-------------|
| `ValidateIsSorted` | Elements in order |
| `ValidateIsNotSorted` | Elements not in order |
| `ValidateIsDistinct` | All elements unique |
| `ValidateIsNotDistinct` | Has duplicates |

### Type-Level & Predicates

| Attribute | Description |
|-----------|-------------|
| `ValidateAgainstPredicate(string predicateName)` | Tests against registered predicate |
| `ValidateNested` | Recursively validate nested object |
| `ValidateEachIsValid` | Validate each collection element |

### Specialized

| Attribute | Description |
|-----------|-------------|
| `ValidateIsValidAge` | Age 0–150 |
| `ValidateIsAdult(int adultAge)` | Age >= threshold |
| `ValidateIsMinor(int adultAge)` | Age < threshold |
| `ValidateIsValidJson` / `ValidateIsNotValidJson` | JSON format |
| `ValidateIsValidXml` / `ValidateIsNotValidXml` | XML format |
| `ValidateIsValidBase64` / `ValidateIsNotValidBase64` | Base64 format |
| `ValidateIsValidEmail` | Email format |
| `ValidateIsWithinDateRange(object min, object max)` | Date in range |
| `ValidateIsBusinessDay` / `ValidateIsWeekend` | Weekday/weekend |
| `ValidateIsUtc` / `ValidateIsLocal` | DateTimeKind |
| `ValidateIsLeapYear` | Leap year |
| `ValidateDoesFileExist` / `ValidateDoesFileNotExist` | File existence |
| `ValidateDoesDirectoryExist` / `ValidateDoesDirectoryNotExist` | Directory existence |
| `ValidateCanRead` / `ValidateCanWrite` / `ValidateCanSeek` | Stream capabilities |
| `ValidateIsValidGuid` / `ValidateIsNotValidGuid` | GUID format |
| `ValidateIsValidIpAddress` | IP address |
| `ValidateIsValidHostname` | Hostname |
| `ValidateIsValidIban` | IBAN |
| `ValidateIsValidBicSwift` | BIC/SWIFT |
| `ValidateIsValidLatitude` / `ValidateIsValidLongitude` | Coordinates |
| `ValidateIsValidSemanticVersion` | SemVer |
| `ValidateIsSecurePassword(...)` | Password complexity |
| `ValidateIsValidPhoneNumber` | Phone format |

## Member Path Tracking

When validation runs via `ValidateIsValid`, failures include a **member path** indicating where the failure occurred:

- Property: `"Name"`, `"Age"`
- Nested: `"Address.City"`
- Collection: `"Items[0].ProductId"`, `"Items[2].Quantity"`

This enables mapping errors to form fields (e.g., ASP.NET ModelState).

## How IsValid / IsNotValid Runs Attributes

`ValidateIsValid` and `CheckIsValid` use the internal `ValidationRunner` to:

1. Run class-level attributes on the instance
2. For each property/field, run member-level attributes (excluding structural ones)
3. For members with `[ValidateNested]`, recursively validate the nested object
4. For members with `[ValidateEachIsValid]`, validate each collection element
5. Collect all failures with member paths

The runner avoids infinite recursion by tracking visited object references.

## Example

```csharp
public class CreateUserRequest
{
    [ValidateIsNotNullOrWhiteSpace(Message = "Username is required")]
    [ValidateMinLength(3, Message = "Username must be at least 3 characters")]
    public string Username { get; set; } = "";

    [ValidateIsValidEmail(Message = "Invalid email format")]
    public string Email { get; set; } = "";

    [ValidateIsSecurePassword(minLength: 8, Message = "Password does not meet requirements")]
    public string Password { get; set; } = "";

    [ValidateIsInRange(18, 120, Message = "Age must be between 18 and 120")]
    public int Age { get; set; }
}

var request = new CreateUserRequest { Username = "ab", Email = "invalid", Password = "weak", Age = 10 };
var result = request.ValidateIsValid();
foreach (var f in result.Failures)
    Console.WriteLine($"{f.MemberPath}: {f.ErrorMessage}");
```
