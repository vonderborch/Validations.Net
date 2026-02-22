# Validations.Net Documentation

## Overview

Validations.Net is a high-performance .NET validation library that provides 100+ validators as extension methods on base types. It supports three usage patterns: inline Check/Validate/Ensure methods, declarative ValidationAttributes for type-level validation, and fluent ValidationSet builders.

## Guides

| Guide | Description |
|-------|-------------|
| [Getting Started](GettingStarted.md) | Quick start guide with usage examples |
| [Validation Attributes](ValidationAttributes.md) | Declarative attribute-based validation |
| [Validation Sets](ValidationSets.md) | Fluent validation builder API |
| [Aggregate Results](AggregateValidationResult.md) | Working with validation results and error formatting |

## General Validators

### Null Checks

| Validator | Description | Docs |
|-----------|-------------|------|
| IsNull | Value is null | [Docs](Validators/NullChecks.md) |
| IsNotNull | Value is not null | [Docs](Validators/NullChecks.md) |

### Equality & Comparison

| Validator | Description | Docs |
|-----------|-------------|------|
| IsEquals | Value equals expected | [Docs](Validators/EqualityComparison.md) |
| IsNotEquals | Value does not equal expected | [Docs](Validators/EqualityComparison.md) |
| IsGreaterThan | Value > comparand | [Docs](Validators/EqualityComparison.md) |
| IsGreaterThanOrEquals | Value >= comparand | [Docs](Validators/EqualityComparison.md) |
| IsLessThan | Value < comparand | [Docs](Validators/EqualityComparison.md) |
| IsLessThanOrEquals | Value <= comparand | [Docs](Validators/EqualityComparison.md) |
| IsInRange | Value within [min, max] | [Docs](Validators/EqualityComparison.md) |
| IsNotInRange | Value outside [min, max] | [Docs](Validators/EqualityComparison.md) |
| IsOneOf | Value is one of allowed values | [Docs](Validators/EqualityComparison.md) |
| IsNotOneOf | Value is not one of the values | [Docs](Validators/EqualityComparison.md) |
| IsDefault | Value equals default(T) | [Docs](Validators/EqualityComparison.md) |
| IsNotDefault | Value is not default(T) | [Docs](Validators/EqualityComparison.md) |
| IsSameAs | Same reference (ReferenceEquals) | [Docs](Validators/EqualityComparison.md) |
| IsNotSameAs | Different reference | [Docs](Validators/EqualityComparison.md) |

### Numeric

| Validator | Description | Docs |
|-----------|-------------|------|
| IsPositive | Value > 0 (INumber&lt;T&gt;) | [Docs](Validators/Numeric.md) |
| IsNegative | Value < 0 | [Docs](Validators/Numeric.md) |
| IsZero | Value == 0 | [Docs](Validators/Numeric.md) |
| IsNonZero | Value != 0 | [Docs](Validators/Numeric.md) |
| IsEven | Even integer | [Docs](Validators/Numeric.md) |
| IsOdd | Odd integer | [Docs](Validators/Numeric.md) |
| IsDivisibleBy | Value % divisor == 0 | [Docs](Validators/Numeric.md) |
| IsFinite | Finite floating point | [Docs](Validators/Numeric.md) |
| IsNaN | NaN floating point | [Docs](Validators/Numeric.md) |
| IsCreditCard | Valid Luhn checksum | [Docs](Validators/Numeric.md) |

### String

| Validator | Description | Docs |
|-----------|-------------|------|
| IsNullOrWhiteSpace | Null, empty, or whitespace | [Docs](Validators/String.md) |
| IsNotNullOrWhiteSpace | Has non-whitespace content | [Docs](Validators/String.md) |
| IsWhiteSpace | All whitespace characters | [Docs](Validators/String.md) |
| IsNotWhiteSpace | Not all whitespace | [Docs](Validators/String.md) |
| IsMatch | Matches regex pattern | [Docs](Validators/String.md) |
| IsNotMatch | Does not match regex | [Docs](Validators/String.md) |
| IsValidUri | Valid URI | [Docs](Validators/String.md) |
| StartsWith | Starts with prefix | [Docs](Validators/String.md) |
| DoesNotStartWith | Does not start with prefix | [Docs](Validators/String.md) |
| EndsWith | Ends with suffix | [Docs](Validators/String.md) |
| DoesNotEndWith | Does not end with suffix | [Docs](Validators/String.md) |
| MinLength | Length >= minimum | [Docs](Validators/String.md) |
| MaxLength | Length <= maximum | [Docs](Validators/String.md) |

### Collection & Sequence

| Validator | Description | Docs |
|-----------|-------------|------|
| IsEmpty | Collection/string is empty | [Docs](Validators/Collection.md) |
| IsNotEmpty | Collection/string is not empty | [Docs](Validators/Collection.md) |
| IsNullOrEmpty | Null or empty | [Docs](Validators/Collection.md) |
| IsNotNullOrEmpty | Not null and not empty | [Docs](Validators/Collection.md) |
| IsLength | Exact length/count | [Docs](Validators/Collection.md) |
| IsNotLength | Not exact length | [Docs](Validators/Collection.md) |
| IsCount | Exact element count | [Docs](Validators/Collection.md) |
| IsNotCount | Not exact count | [Docs](Validators/Collection.md) |
| IsSingle | Exactly one element | [Docs](Validators/Collection.md) |
| IsNotSingle | Not exactly one element | [Docs](Validators/Collection.md) |
| IsSubsetOf | All elements in target | [Docs](Validators/Collection.md) |
| IsNotSubsetOf | Not a subset | [Docs](Validators/Collection.md) |
| IsSupersetOf | Contains all target elements | [Docs](Validators/Collection.md) |
| IsNotSupersetOf | Not a superset | [Docs](Validators/Collection.md) |

### Containment

| Validator | Description | Docs |
|-----------|-------------|------|
| DoesContain | Contains item/substring | [Docs](Validators/Containment.md) |
| DoesNotContain | Does not contain item | [Docs](Validators/Containment.md) |
| DoesContainAny | Contains at least one | [Docs](Validators/Containment.md) |
| DoesNotContainAny | Contains none | [Docs](Validators/Containment.md) |
| DoesContainAll | Contains all required | [Docs](Validators/Containment.md) |
| DoesNotContainAll | Missing at least one | [Docs](Validators/Containment.md) |

### Sequence Order & Type

| Validator | Description | Docs |
|-----------|-------------|------|
| IsSorted | Elements in order | [Docs](Validators/SequenceType.md) |
| IsNotSorted | Elements not in order | [Docs](Validators/SequenceType.md) |
| IsDistinct | All elements unique | [Docs](Validators/SequenceType.md) |
| IsNotDistinct | Has duplicate elements | [Docs](Validators/SequenceType.md) |
| IsAssignableTo | Assignable to target type | [Docs](Validators/SequenceType.md) |
| IsInstanceOf | Exact type match | [Docs](Validators/SequenceType.md) |

### Type-Level Validation

| Validator | Description | Docs |
|-----------|-------------|------|
| IsValid | All ValidationAttributes pass | [Docs](Validators/IsValid.md) |
| IsNotValid | At least one attribute fails | [Docs](Validators/IsNotValid.md) |
| AgainstPredicate | Tests against a predicate | [Docs](Validators/AgainstPredicate.md) |

## Specialized Validators

### Age

| Validator | Description | Docs |
|-----------|-------------|------|
| IsValidAge | Age 0-150 | [Docs](Validators/Age.md) |
| IsAdult | Age >= adult threshold | [Docs](Validators/Age.md) |
| IsMinor | Age < adult threshold | [Docs](Validators/Age.md) |
| IsWithinAgeRange | Age within range | [Docs](Validators/Age.md) |

### Data Format

| Validator | Description | Docs |
|-----------|-------------|------|
| IsValidJson / IsNotValidJson | JSON string validation | [Docs](Validators/DataFormat.md) |
| IsValidXml / IsNotValidXml | XML string validation | [Docs](Validators/DataFormat.md) |
| IsValidBase64 / IsNotValidBase64 | Base64 validation | [Docs](Validators/DataFormat.md) |
| IsValidEmail | Email format validation | [Docs](Validators/DataFormat.md) |

### Date & Time

| Validator | Description | Docs |
|-----------|-------------|------|
| IsBusinessDay / IsWeekend | Weekday/weekend | [Docs](Validators/DateTime.md) |
| IsWithinDateRange | Within date range | [Docs](Validators/DateTime.md) |
| IsUtc / IsLocal | DateTimeKind checks | [Docs](Validators/DateTime.md) |
| IsLeapYear | Leap year check | [Docs](Validators/DateTime.md) |

### File System

| Validator | Description | Docs |
|-----------|-------------|------|
| DoesFileExist / DoesFileNotExist | File existence | [Docs](Validators/FileSystem.md) |
| DoesDirectoryExist / DoesDirectoryNotExist | Directory existence | [Docs](Validators/FileSystem.md) |

### Streams

| Validator | Description | Docs |
|-----------|-------------|------|
| CanRead / CanWrite / CanSeek | Stream capability checks | [Docs](Validators/Streams.md) |

### Identifiers

| Validator | Description | Docs |
|-----------|-------------|------|
| IsValidGuid / IsNotValidGuid | GUID format | [Docs](Validators/Identifiers.md) |

### Network

| Validator | Description | Docs |
|-----------|-------------|------|
| IsValidIpAddress | IPv4/IPv6 address | [Docs](Validators/Network.md) |
| IsValidHostname | Hostname format | [Docs](Validators/Network.md) |

### Finance

| Validator | Description | Docs |
|-----------|-------------|------|
| IsValidIban | IBAN format | [Docs](Validators/Finance.md) |
| IsValidBicSwift | BIC/SWIFT code | [Docs](Validators/Finance.md) |

### Geography

| Validator | Description | Docs |
|-----------|-------------|------|
| IsValidLatitude | -90 to 90 | [Docs](Validators/Geography.md) |
| IsValidLongitude | -180 to 180 | [Docs](Validators/Geography.md) |

### Version

| Validator | Description | Docs |
|-----------|-------------|------|
| IsValidSemanticVersion | SemVer format | [Docs](Validators/Version.md) |

### Security

| Validator | Description | Docs |
|-----------|-------------|------|
| IsSecurePassword | Password complexity | [Docs](Validators/Security.md) |

### Phone

| Validator | Description | Docs |
|-----------|-------------|------|
| IsValidPhoneNumber | Phone number format | [Docs](Validators/Phone.md) |
