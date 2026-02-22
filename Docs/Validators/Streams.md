# Streams

Validators for stream capabilities: read, write, and seek.

## Validators

### CanRead

**Description:** Validates that a stream supports reading.

**Methods:**
- `CheckCanRead` → `bool`
- `ValidateCanRead` → `ValidationResult`
- `EnsureCanRead` → `Stream?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| stream | Stream? | The stream to validate |

**Matching Attribute:** `[ValidateCanRead]`

---

### CanWrite

**Description:** Validates that a stream supports writing.

**Methods:**
- `CheckCanWrite` → `bool`
- `ValidateCanWrite` → `ValidationResult`
- `EnsureCanWrite` → `Stream?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| stream | Stream? | The stream to validate |

**Matching Attribute:** `[ValidateCanWrite]`

---

### CanSeek

**Description:** Validates that a stream supports seeking.

**Methods:**
- `CheckCanSeek` → `bool`
- `ValidateCanSeek` → `ValidationResult`
- `EnsureCanSeek` → `Stream?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| stream | Stream? | The stream to validate |

**Matching Attribute:** `[ValidateCanSeek]`
