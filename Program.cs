using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    // Generates a random 4-digit number between 1000 and 9999
    static int RandoNo()
    {
        Random rnd = new Random();
        return rnd.Next(1000, 10000);
    }

    // Prints a message to the console
    static void PrintMessage(string message)
    {
        Console.WriteLine(message);
    }

    // Validates that a number is a proper Kaprekar input:
    // Must be a 4-digit number with at least two distinct digits
    static bool IsValidKaprekarInput(int number)
    {
        string digits = number.ToString().PadLeft(4, '0');
        var uniqueDigits = new HashSet<char>(digits);
        return uniqueDigits.Count >= 2;
    }

    // Prompts the user to enter a 4-digit number and returns it if valid
    // If invalid, returns -1 to signal fallback is needed
    static int GetUserKaprekarInput()
    {
        PrintMessage("Enter a 4-digit number with at least two different digits:");
        string? input = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(input) &&
            int.TryParse(input, out int userNumber) &&
            userNumber >= 0 && userNumber <= 9999 &&
            IsValidKaprekarInput(userNumber))
        {
            return userNumber;
        }

        return -1;
    }

    // Returns a summary of digit uniqueness: how many are unique vs duplicated
    static string GetDigitUniquenessSummary(int number)
    {
        string digits = number.ToString().PadLeft(4, '0');
        var frequency = new Dictionary<char, int>();

        foreach (char digit in digits)
        {
            if (frequency.ContainsKey(digit))
                frequency[digit]++;
            else
                frequency[digit] = 1;
        }

        int uniqueCount = 0;
        int duplicatedCount = 0;

        foreach (var kvp in frequency)
        {
            if (kvp.Value == 1)
                uniqueCount++;
            else
                duplicatedCount++;
        }

        return $"There are {uniqueCount} unique digits and {duplicatedCount} duplicated digit{(duplicatedCount != 1 ? "s" : "")}.";
    }

    // Performs the Kaprekar routine and logs each iteration
    static void RunKaprekarRoutine(int number)
    {
        const int kaprekarConstant = 6174;
        int iterations = 0;

        while (number != kaprekarConstant)
        {
            string padded = number.ToString().PadLeft(4, '0');

            // Sort digits descending and ascending
            string desc = String.Concat(padded.OrderByDescending(c => c));
            string asc = String.Concat(padded.OrderBy(c => c));

            int high = int.Parse(desc);
            int low = int.Parse(asc);
            number = high - low;
            iterations++;

            PrintMessage($"{desc} - {asc} = {number.ToString().PadLeft(4, '0')}");
        }

        PrintMessage($"Kaprekar's constant reached in {iterations} iteration{(iterations != 1 ? "s" : "")}.");
    }

    // Entry point: prompts user, validates input, falls back to random if needed
    static void Main()
    {
        int kaprekarInput;

        int userInput = GetUserKaprekarInput();
        if (userInput == -1)
        {
            PrintMessage("Invalid Kaprekar input. A random valid number will be used instead.");
            kaprekarInput = RandoNo();
        }
        else
        {
            kaprekarInput = userInput;
        }

        PrintMessage("Kaprekar starting number: " + kaprekarInput);
        PrintMessage(GetDigitUniquenessSummary(kaprekarInput));
        RunKaprekarRoutine(kaprekarInput);
    }
}