// ============================================================================
// Workshop 01: Hello World Console App
// ============================================================================
// Topics covered:
//   - Creating and running a .NET console application
//   - Console.WriteLine and string interpolation
//   - Reading user input with Console.ReadLine
//   - Basic program structure
// ============================================================================

Console.WriteLine("========================================");
Console.WriteLine("  Workshop 01 – Hello World Console App");
Console.WriteLine("========================================");
Console.WriteLine();

// --- Part 1: Simple output ---
Console.WriteLine("Hello, World!");
Console.WriteLine("Welcome to .NET 10 and C# Foundations.");
Console.WriteLine();

// --- Part 2: String interpolation ---
string teamName = "Full-Stack Engineering Bootcamp";
int dayNumber = 2;
Console.WriteLine($"Hello from {teamName}, Day {dayNumber}!");
Console.WriteLine();

// --- Part 3: Reading user input ---
Console.Write("What is your name? ");
string? name = Console.ReadLine();
Console.WriteLine($"Hello, {name}! Welcome to the workshop.");
Console.WriteLine();

// --- Part 4: Multiple outputs ---
Console.WriteLine("Things you will learn today:");
Console.WriteLine("  1. Variables and Types");
Console.WriteLine("  2. Control Flow");
Console.WriteLine("  3. Methods and Clean Code");
Console.WriteLine("  4. Classes and Objects");
Console.WriteLine("  5. OOP Principles");
Console.WriteLine("  6. Collections and LINQ");
Console.WriteLine("  7. Async/Await");
Console.WriteLine("  8. Unit Testing");
Console.WriteLine();

// --- Part 5: Simple calculation ---
Console.Write("Enter a number: ");
string? input = Console.ReadLine();
if (int.TryParse(input, out int number))
{
    Console.WriteLine($"Your number doubled is: {number * 2}");
    Console.WriteLine($"Your number squared is: {number * number}");
}
else
{
    Console.WriteLine("That was not a valid number.");
}

Console.WriteLine();
Console.WriteLine("Workshop 01 complete!");
