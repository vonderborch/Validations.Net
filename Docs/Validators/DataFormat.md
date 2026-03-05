# Data Format

Validators for JSON, XML, Base64, and email format validation.

## Validators

### IsValidJson

**Description:** Validates that a string represents valid JSON (parsed via `JsonDocument.Parse`).

**Methods:**
- `CheckIsValidJson` → `bool`
- `ValidateIsValidJson` → `ValidationResult`
- `EnsureIsValidJson` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |

**Matching Attribute:** `[ValidateIsValidJson]`

---

### IsNotValidJson

**Description:** Validates that a string does not represent valid JSON.

**Methods:**
- `CheckIsNotValidJson` → `bool`
- `ValidateIsNotValidJson` → `ValidationResult`
- `EnsureIsNotValidJson` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |

**Matching Attribute:** `[ValidateIsNotValidJson]`

---

### IsValidXml

**Description:** Validates that a string represents valid XML (parsed via `XDocument.Parse`).

**Methods:**
- `CheckIsValidXml` → `bool`
- `ValidateIsValidXml` → `ValidationResult`
- `EnsureIsValidXml` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |

**Matching Attribute:** `[ValidateIsValidXml]`

---

### IsNotValidXml

**Description:** Validates that a string does not represent valid XML.

**Methods:**
- `CheckIsNotValidXml` → `bool`
- `ValidateIsNotValidXml` → `ValidationResult`
- `EnsureIsNotValidXml` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |

**Matching Attribute:** `[ValidateIsNotValidXml]`

---

### IsValidBase64

**Description:** Validates that a string represents valid Base64-encoded data.

**Methods:**
- `CheckIsValidBase64` → `bool`
- `ValidateIsValidBase64` → `ValidationResult`
- `EnsureIsValidBase64` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |

**Matching Attribute:** `[ValidateIsValidBase64]`

---

### IsNotValidBase64

**Description:** Validates that a string does not represent valid Base64-encoded data.

**Methods:**
- `CheckIsNotValidBase64` → `bool`
- `ValidateIsNotValidBase64` → `ValidationResult`
- `EnsureIsNotValidBase64` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |

**Matching Attribute:** `[ValidateIsNotValidBase64]`

---

### IsValidEmail

**Description:** Validates that a string represents a valid email address (basic check: one '@', content before/after, domain has '.', no spaces).

**Methods:**
- `CheckIsValidEmail` → `bool`
- `ValidateIsValidEmail` → `ValidationResult`
- `EnsureIsValidEmail` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |

**Matching Attribute:** `[ValidateIsValidEmail]`
