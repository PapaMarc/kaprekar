# AI Coding Agent Instructions for Kaprekar

## Project Overview

This is a simple C# console application that demonstrates the **Kaprekar Constant** (6174) - a mathematical property where any 4-digit number with at least two distinct digits, when repeatedly subjected to a specific operation (descending digits minus ascending digits), converges to 6174.

**Key insight**: The project is educational/exploratory, not a production system. Quality is in clarity and correctness of the mathematical routine, not in architecture complexity.

## Architecture & Core Logic

The codebase is single-file (`Program.cs`) with a functional style:

- **`RunKaprekarRoutine(int)`** - Core algorithm: repeatedly subtracts ascending digit arrangement from descending until reaching 6174. Logs each iteration showing the math visually (`9531 - 1359 = 8172`).
- **Input validation pipeline**: `TryParseKaprekarInput()` → `IsValidKaprekarInput()` - Must be 4-digit range (0-9999) with ≥2 distinct digits.
- **Fallback behavior**: Invalid user input triggers automatic `RandoNo()` to generate a valid number rather than rejecting.
- **UI helpers**: `PrintMessage()` abstracts console output; `GetDigitUniquenessSummary()` provides digit frequency analysis.

## Critical Developer Workflow

**Build & Run**:
```powershell
dotnet build
dotnet run
# Or directly: Program.exe (after build)
```

**Target Framework**: .NET 10.0 (see `kaprekar.csproj`)

**Nullable reference types enabled** - Code follows strict null-safety patterns; pay attention to `?` and `null` checks.

## Code Patterns & Conventions

1. **Null-safety**: All string inputs are checked with `string.IsNullOrWhiteSpace()` before parsing.
2. **Number formatting**: Numbers are padded to 4 digits using `.PadLeft(4, '0')` to preserve leading zeros during digit manipulation.
3. **Plural handling**: Iteration/duplicate counts use ternary operators to match singular/plural suffixes (e.g., `"iteration" vs "iterations"`).
4. **Digit manipulation**: Heavy use of `OrderBy/OrderByDescending()` on character arrays to sort digits; converted back to int via `int.Parse()`.
5. **Loop entry pattern**: `retryInput = "init"` dummy initialization to enter a while loop checking `!string.IsNullOrWhiteSpace()`.

## Integration Points & External Dependencies

- **System.Linq** - Required for `OrderBy/OrderByDescending()` and `HashSet<char>`.
- **System.Collections.Generic** - For `HashSet` (digit uniqueness check) and `Dictionary` (frequency analysis).
- **No external NuGet packages** - Pure .NET runtime only.

## Key Validation Rules

When modifying input validation or the routine:
- **Kaprekar requires ≥2 distinct digits** - e.g., 1111 is invalid, 1234 is valid.
- **Numbers outside 0-9999 are rejected** - enforced in `TryParseKaprekarInput()`.
- **The constant is always 6174** - hard-coded, not parameterized (appropriate for this mathematical property).
- **All numbers are treated as 4-digit** - padding with zeros is essential for the algorithm.

## Testing Considerations

The app is interactive/manual-test driven. If adding unit tests, focus on:
- `IsValidKaprekarInput()` - boundary cases (0, 1111, 9999, mixed digits).
- `GetDigitUniquenessSummary()` - frequency accuracy.
- `RunKaprekarRoutine()` - convergence to 6174 regardless of valid input.
