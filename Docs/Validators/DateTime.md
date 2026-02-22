# DateTime

Validators for date/time values: business day, weekend, date range, UTC/local kind, and leap year.

## Validators

### IsBusinessDay

**Description:** Validates that a date falls on a business day (Monday through Friday).

**Target types:** `DateTime`, `DateTimeOffset`

**Methods:**
- `CheckIsBusinessDay` → `bool`
- `ValidateIsBusinessDay` → `ValidationResult`
- `EnsureIsBusinessDay` → same type (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | DateTime / DateTimeOffset | The date to validate |

**Matching Attribute:** `[ValidateIsBusinessDay]`

---

### IsWeekend

**Description:** Validates that a date falls on a weekend (Saturday or Sunday).

**Target types:** `DateTime`, `DateTimeOffset`

**Methods:**
- `CheckIsWeekend` → `bool`
- `ValidateIsWeekend` → `ValidationResult`
- `EnsureIsWeekend` → same type (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | DateTime / DateTimeOffset | The date to validate |

**Matching Attribute:** `[ValidateIsWeekend]`

---

### IsWithinDateRange

**Description:** Validates that a date/time value falls within a specified range (inclusive).

**Target types:** `DateTime`, `DateTimeOffset`

**Methods:**
- `CheckIsWithinDateRange` → `bool`
- `ValidateIsWithinDateRange` → `ValidationResult`
- `EnsureIsWithinDateRange` → same type (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | DateTime / DateTimeOffset | The date to validate |
| min | DateTime / DateTimeOffset | Minimum bound |
| max | DateTime / DateTimeOffset | Maximum bound |

**Matching Attribute:** `[ValidateIsWithinDateRange(min, max)]`

---

### IsUtc

**Description:** Validates that a DateTime has `Kind == Utc`, or that a DateTimeOffset has zero offset.

**Target types:** `DateTime`, `DateTimeOffset`

**Methods:**
- `CheckIsUtc` → `bool`
- `ValidateIsUtc` → `ValidationResult`
- `EnsureIsUtc` → same type (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | DateTime / DateTimeOffset | The value to validate |

**Matching Attribute:** `[ValidateIsUtc]`

---

### IsLocal

**Description:** Validates that a DateTime has `Kind == Local`.

**Target types:** `DateTime`

**Methods:**
- `CheckIsLocal` → `bool`
- `ValidateIsLocal` → `ValidationResult`
- `EnsureIsLocal` → `DateTime` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | DateTime | The value to validate |

**Matching Attribute:** `[ValidateIsLocal]`

---

### IsLeapYear

**Description:** Validates that a year or the year of a date is a leap year.

**Target types:** `int`, `DateTime`, `DateTimeOffset`

**Methods:**
- `CheckIsLeapYear` → `bool`
- `ValidateIsLeapYear` → `ValidationResult`
- `EnsureIsLeapYear` → same type (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| year / value | int / DateTime / DateTimeOffset | The year or date to validate |

**Matching Attribute:** `[ValidateIsLeapYear]`
