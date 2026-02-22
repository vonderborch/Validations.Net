# Age

Validators for age values and date-of-birth–based age checks.

## Validators

### IsValidAge

**Description:** Validates that an age is within a reasonable range (0–150 inclusive).

**Target types:** `int`

**Methods:**
- `CheckIsValidAge` → `bool`
- `ValidateIsValidAge` → `ValidationResult`
- `EnsureIsValidAge` → `int` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| age | int | The age to validate |

**Matching Attribute:** `[ValidateIsValidAge]`

---

### IsAdult

**Description:** Validates that an age or date of birth indicates adulthood (age ≥ adultAge). For dates, age is calculated from UTC now.

**Target types:** `int`, `DateTime`, `DateTimeOffset`

**Methods:**
- `CheckIsAdult` → `bool`
- `ValidateIsAdult` → `ValidationResult`
- `EnsureIsAdult` → same type (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| age / dateOfBirth | int / DateTime / DateTimeOffset | The age or date of birth |
| adultAge | int | Age threshold for adulthood (default: 18) |

**Matching Attribute:** `[ValidateIsAdult]`

---

### IsMinor

**Description:** Validates that an age or date of birth indicates minority (age < adultAge). For dates, age is calculated from UTC now.

**Target types:** `int`, `DateTime`, `DateTimeOffset`

**Methods:**
- `CheckIsMinor` → `bool`
- `ValidateIsMinor` → `ValidationResult`
- `EnsureIsMinor` → same type (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| age / dateOfBirth | int / DateTime / DateTimeOffset | The age or date of birth |
| adultAge | int | Age threshold (default: 18) |

**Matching Attribute:** `[ValidateIsMinor]`

---

### IsWithinAgeRange

**Description:** Validates that an age or date-of-birth–derived age falls within a specified range.

**Target types:** `int`, `DateTime`, `DateTimeOffset`

**Methods:**
- `CheckIsWithinAgeRange` → `bool`
- `ValidateIsWithinAgeRange` → `ValidationResult`
- `EnsureIsWithinAgeRange` → same type (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| age / dateOfBirth | int / DateTime / DateTimeOffset | The age or date of birth |
| minAge | int | Minimum age (inclusive) |
| maxAge | int | Maximum age (inclusive) |

**Matching Attribute:** `[ValidateIsWithinAgeRange(minAge, maxAge)]`
