# Network

Validators for IP addresses and hostnames.

## Validators

### IsValidIpAddress

**Description:** Validates that a string represents a valid IP address (IPv4 or IPv6).

**Methods:**
- `CheckIsValidIpAddress` → `bool`
- `ValidateIsValidIpAddress` → `ValidationResult`
- `EnsureIsValidIpAddress` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |

**Matching Attribute:** `[ValidateIsValidIpAddress]`

---

### IsValidHostname

**Description:** Validates that a string represents a valid hostname. Rules: length 1–253, each label 1–63 chars, alphanumeric and hyphens only, no leading/trailing hyphens per label.

**Methods:**
- `CheckIsValidHostname` → `bool`
- `ValidateIsValidHostname` → `ValidationResult`
- `EnsureIsValidHostname` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |

**Matching Attribute:** `[ValidateIsValidHostname]`
