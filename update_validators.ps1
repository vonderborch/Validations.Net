# Script to add DefaultValidationFailureMessage constant to validators that are missing it

$validatorsToUpdate = @(
    @{ File = "DoesContainAll.cs"; Message = "Parameter must contain all specified items" },
    @{ File = "DoesContainAny.cs"; Message = "Parameter must contain any of the specified items" },
    @{ File = "DoesNotContain.cs"; Message = "Parameter must not contain the specified item" },
    @{ File = "DoesNotContainAll.cs"; Message = "Parameter must not contain all specified items" },
    @{ File = "DoesNotContainAny.cs"; Message = "Parameter must not contain any of the specified items" },
    @{ File = "IsAssignableTo.cs"; Message = "Parameter must be assignable to the specified type" },
    @{ File = "IsDivisibleBy.cs"; Message = "Parameter must be divisible by the specified value" },
    @{ File = "IsEven.cs"; Message = "Parameter must be even" },
    @{ File = "IsInRange.cs"; Message = "Parameter must be within the specified range" },
    @{ File = "IsInterfaceOf.cs"; Message = "Parameter must implement the specified interface" },
    @{ File = "IsLength.cs"; Message = "Parameter must have the specified length" },
    @{ File = "IsNegative.cs"; Message = "Parameter must be negative" },
    @{ File = "IsNotAssignableTo.cs"; Message = "Parameter must not be assignable to the specified type" },
    @{ File = "IsNotDivisibleBy.cs"; Message = "Parameter must not be divisible by the specified value" },
    @{ File = "IsNotInRange.cs"; Message = "Parameter must not be within the specified range" },
    @{ File = "IsNotLength.cs"; Message = "Parameter must not have the specified length" },
    @{ File = "IsNotOfType.cs"; Message = "Parameter must not be of the specified type" },
    @{ File = "IsNotPerfectSquare.cs"; Message = "Parameter must not be a perfect square" },
    @{ File = "IsNotPowerOf.cs"; Message = "Parameter must not be a power of the specified value" },
    @{ File = "IsNotSorted.cs"; Message = "Parameter must not be sorted" },
    @{ File = "IsNotZero.cs"; Message = "Parameter must not be zero" },
    @{ File = "IsOdd.cs"; Message = "Parameter must be odd" },
    @{ File = "IsOfType.cs"; Message = "Parameter must be of the specified type" },
    @{ File = "IsPerfectSquare.cs"; Message = "Parameter must be a perfect square" },
    @{ File = "IsPositive.cs"; Message = "Parameter must be positive" },
    @{ File = "IsPowerOf.cs"; Message = "Parameter must be a power of the specified value" },
    @{ File = "IsSorted.cs"; Message = "Parameter must be sorted" },
    @{ File = "IsStruct.cs"; Message = "Parameter must be a struct" },
    @{ File = "IsZero.cs"; Message = "Parameter must be zero" }
)

foreach ($validator in $validatorsToUpdate) {
    $filePath = "source/Validations.Net/Validators/$($validator.File)"
    $content = Get-Content $filePath -Raw
    
    # Extract the validator name from the file name (without .cs)
    $validatorName = $validator.File -replace '\.cs$', ''
    
    # Check if DefaultValidationFailureMessage already exists
    if ($content -notmatch 'DefaultValidationFailureMessage') {
        # Find where to insert the constant (after ValidatorName)
        $pattern = "(private const string ValidatorName = [^;]+;)"
        $replacement = "`$1`n`n    /// <summary>`n    /// Represents the default failure message used when the validator fails validation.`n    /// </summary>`n    public const string DefaultValidationFailureMessage = `"$($validator.Message)`";"
        
        $content = $content -replace $pattern, $replacement
        
        Set-Content $filePath -Value $content -NoNewline
        Write-Host "Added DefaultValidationFailureMessage to $($validator.File)"
    }
}

Write-Host "Done updating validator constants!"



