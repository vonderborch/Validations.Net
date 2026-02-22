# Numeric

Validators for numeric properties: sign, zero, parity, divisibility, floating-point special values, and credit card numbers.

## Validators

### IsPositive

**Description:** Validates that a numeric value is positive.

**Type constraints:** `T : INumber<T>`

**Methods:**
- `CheckIsPositive` → `bool`
- `ValidateIsPositive` → `ValidationResult`
- `EnsureIsPositive` → `T` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | T | The value to validate |

---

### IsNegative

**Description:** Validates that a numeric value is negative.

**Type constraints:** `T : INumber<T>`

**Methods:**
- `CheckIsNegative` → `bool`
- `ValidateIsNegative` → `ValidationResult`
- `EnsureIsNegative` → `T` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | T | The value to validate |

---

### IsZero

**Description:** Validates that a numeric value is zero.

**Type constraints:** `T : INumber<T>`

**Methods:**
- `CheckIsZero` → `bool`
- `ValidateIsZero` → `ValidationResult`
- `EnsureIsZero` → `T` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | T | The value to validate |

---

### IsNonZero

**Description:** Validates that a numeric value is not zero.

**Type constraints:** `T : INumber<T>`

**Methods:**
- `CheckIsNonZero` → `bool`
- `ValidateIsNonZero` → `ValidationResult`
- `EnsureIsNonZero` → `T` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | T | The value to validate |

---

### IsEven

**Description:** Validates that an integer value is even.

**Type constraints:** `T : IBinaryInteger<T>`

**Methods:**
- `CheckIsEven` → `bool`
- `ValidateIsEven` → `ValidationResult`
- `EnsureIsEven` → `T` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | T | The value to validate |

**Matching Attribute:** `[ValidateIsEven]`

---

### IsOdd

**Description:** Validates that an integer value is odd.

**Type constraints:** `T : IBinaryInteger<T>`

**Methods:**
- `CheckIsOdd` → `bool`
- `ValidateIsOdd` → `ValidationResult`
- `EnsureIsOdd` → `T` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | T | The value to validate |

**Matching Attribute:** `[ValidateIsOdd]`

---

### IsDivisibleBy

**Description:** Validates that an integer value is divisible by a given divisor.

**Type constraints:** `T : IBinaryInteger<T>`

**Methods:**
- `CheckIsDivisibleBy` → `bool`
- `ValidateIsDivisibleBy` → `ValidationResult`
- `EnsureIsDivisibleBy` → `T` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | T | The value to validate |
| divisor | T | The divisor (must not be zero) |

**Matching Attribute:** `[ValidateIsDivisibleBy(divisor)]`

---

### IsFinite

**Description:** Validates that a floating-point value is finite (not NaN, not ±infinity).

**Type constraints:** `T : IFloatingPoint<T>`

**Methods:**
- `CheckIsFinite` → `bool`
- `ValidateIsFinite` → `ValidationResult`
- `EnsureIsFinite` → `T` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | T | The value to validate |

**Matching Attribute:** `[ValidateIsFinite]`

---

### IsNaN

**Description:** Validates that a floating-point value is NaN.

**Type constraints:** `T : IFloatingPoint<T>`

**Methods:**
- `CheckIsNaN` → `bool`
- `ValidateIsNaN` → `ValidationResult`
- `EnsureIsNaN` → `T` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | T | The value to validate |

**Matching Attribute:** `[ValidateIsNaN]`

---

### IsCreditCard

**Description:** Validates that a string represents a valid credit card number using the Luhn algorithm. Strips non-digits, validates length 13–19, and runs Luhn checksum.

**Type constraints:** `string?`

**Methods:**
- `CheckIsCreditCard` → `bool`
- `ValidateIsCreditCard` → `ValidationResult`
- `EnsureIsCreditCard` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |

**Matching Attribute:** `[ValidateIsCreditCard]`
