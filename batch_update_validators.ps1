# Comprehensive script to add validationFailureMessage parameter to all validators

$validatorsConfig = @{
    "IsNotZero.cs" = "Parameter must not be zero"
    "IsEven.cs" = "Parameter must be even"
    "IsOdd.cs" = "Parameter must be odd"
    "IsPositive.cs" = "Parameter must be positive"
    "IsNegative.cs" = "Parameter must be negative"
    "IsDivisibleBy.cs" = "Parameter must be divisible by the specified value"
    "IsNotDivisibleBy.cs" = "Parameter must not be divisible by the specified value"
    "IsInRange.cs" = "Parameter must be within the specified range"
    "IsNotInRange.cs" = "Parameter must not be within the specified range"
    "IsPowerOf.cs" = "Parameter must be a power of the specified value"
    "IsNotPowerOf.cs" = "Parameter must not be a power of the specified value"
    "IsPerfectSquare.cs" = "Parameter must be a perfect square"
    "IsNotPerfectSquare.cs" = "Parameter must not be a perfect square"
    "IsOfType.cs" = "Parameter must be of the specified type"
    "IsNotOfType.cs" = "Parameter must not be of the specified type"
    "IsAssignableTo.cs" = "Parameter must be assignable to the specified type"
    "IsNotAssignableTo.cs" = "Parameter must not be assignable to the specified type"
    "IsInterfaceOf.cs" = "Parameter must implement the specified interface"
    "IsLength.cs" = "Parameter must have the specified length"
    "IsNotLength.cs" = "Parameter must not have the specified length"
    "IsSorted.cs" = "Parameter must be sorted"
    "IsNotSorted.cs" = "Parameter must not be sorted"
    "DoesContainAll.cs" = "Parameter must contain all specified items"
    "DoesContainAny.cs" = "Parameter must contain any of the specified items"
    "DoesNotContain.cs" = "Parameter must not contain the specified item"
    "DoesNotContainAll.cs" = "Parameter must not contain all specified items"
    "DoesNotContainAny.cs" = "Parameter must not contain any of the specified items"
}

$basePath = "source/Validations.Net/Validators"

foreach ($file in $validatorsConfig.Keys) {
    $filePath = Join-Path $basePath $file
    $message = $validatorsConfig[$file]
    $validatorName = $file -replace '\.cs$', ''
    
    Write-Host "Updating $file..." -ForegroundColor Yellow
    
    $content = Get-Content $filePath -Raw
    
    # Step 1: Add DefaultValidationFailureMessage constant if not present
    if ($content -notmatch 'DefaultValidationFailureMessage') {
        # Find the ValidatorName constant and add the default message after it
        $pattern = "(private const string ValidatorName = [^;]+;)"
        $replacement = "/// <summary>`n    /// Represents the unique identifier name for the validator.`n    /// </summary>`n    public const string ValidatorName = `"$validatorName`";`n`n    /// <summary>`n    /// Represents the default failure message used when the validator fails validation.`n    /// </summary>`n    public const string DefaultValidationFailureMessage = `"$message`";"
        
        $content = $content -replace $pattern, $replacement
    }
    
    # Step 2: Add validationFailureMessage parameter to all Ensure methods
    # Pattern: (public static [^{]+Ensure[^\(]+\([^)]+IBlackboard\? blackboard = null,)\s+(\[CallerArgumentExpression)
    $content = $content -replace '(\bpublic static [^{]+Ensure[^\(]+\([^)]+IBlackboard\? blackboard = null,)\s+(\[CallerArgumentExpression)', '$1`n        string validationFailureMessage = DefaultValidationFailureMessage,`n        $2'
    
    # Step 3: Add validationFailureMessage parameter to all Validate methods
    $content = $content -replace '(\bpublic static ValidationResult Validate[^\(]+\([^)]+IBlackboard\? blackboard = null,)\s+(\[CallerArgumentExpression)', '$1`n        string validationFailureMessage = DefaultValidationFailureMessage,`n        $2'
    
    # Step 4: Update Validate calls in Ensure methods to include validationFailureMessage
    # Pattern: var result = value.Validate[^(]+\(([^,]+, )?blackboard, parameterName\);
    $content = $content -replace '(var result = [^.]+\.Validate[^\(]+\([^,]*,?\s*blackboard,)\s+parameterName\)', '$1 validationFailureMessage, parameterName)'
    $content = $content -replace '(var result = Validate[^\(]+\([^,]*,?\s*blackboard,)\s+parameterName\)', '$1 validationFailureMessage, parameterName)'
    
    # Step 5: Replace hardcoded error messages with validationFailureMessage in CreateFromValidationFailure
    $content = $content -replace '(ValidationResult\.CreateFromValidationFailure\(\s*ValidatorName,\s*)("[^"]*"|[^\r\n,]+),', '$1validationFailureMessage,'
    
    Set-Content $filePath -Value $content -NoNewline
    Write-Host "✓ Updated $file" -ForegroundColor Green
}

Write-Host "`n✓ All validators updated successfully!" -ForegroundColor Green





