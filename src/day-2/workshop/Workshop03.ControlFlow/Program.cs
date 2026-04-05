// ============================================================================
// Workshop 03: Control Flow
// ============================================================================
// Topics covered:
//   - if / else if / else
//   - switch statement and switch expression
//   - for, foreach, while, do-while loops
//   - Pattern matching
//   - Ternary operator
//   - Logical operators
// ============================================================================

Console.WriteLine("========================================");
Console.WriteLine("  Workshop 03 \u2013 Control Flow");
Console.WriteLine("========================================");
Console.WriteLine();

// --- Part 1: if / else if / else ---
Console.WriteLine("--- Part 1: if / else if / else ---");

int score = 85;

if (score >= 90)
{
    Console.WriteLine($"Score {score}: Grade A \u2013 Excellent!");
}
else if (score >= 80)
{
    Console.WriteLine($"Score {score}: Grade B \u2013 Good job!");
}
else if (score >= 70)
{
    Console.WriteLine($"Score {score}: Grade C \u2013 Satisfactory.");
}
else if (score >= 60)
{
    Console.WriteLine($"Score {score}: Grade D \u2013 Needs improvement.");
}
else
{
    Console.WriteLine($"Score {score}: Grade F \u2013 Failing.");
}
Console.WriteLine();

// --- Part 2: Logical operators ---
Console.WriteLine("--- Part 2: Logical Operators ---");

int age = 25;
bool hasLicense = true;
bool hasInsurance = true;

if (age >= 18 && hasLicense && hasInsurance)
{
    Console.WriteLine("You can drive.");
}

bool isWeekend = true;
bool isHoliday = false;

if (isWeekend || isHoliday)
{
    Console.WriteLine("It's a day off!");
}

if (!isHoliday)
{
    Console.WriteLine("It's not a holiday.");
}
Console.WriteLine();

// --- Part 3: Ternary operator ---
Console.WriteLine("--- Part 3: Ternary Operator ---");

int temperature = 30;
string weather = temperature > 25 ? "Hot" : "Cool";
Console.WriteLine($"Temperature {temperature} degrees is {weather}.");

string status = score >= 60 ? "Passed" : "Failed";
Console.WriteLine($"Score {score}: {status}");
Console.WriteLine();

// --- Part 4: switch statement ---
Console.WriteLine("--- Part 4: Switch Statement ---");

string dayOfWeek = "Wednesday";

switch (dayOfWeek)
{
    case "Monday":
        Console.WriteLine("Start of the work week.");
        break;
    case "Tuesday":
    case "Wednesday":
    case "Thursday":
        Console.WriteLine("Midweek \u2013 keep pushing!");
        break;
    case "Friday":
        Console.WriteLine("Almost weekend!");
        break;
    case "Saturday":
    case "Sunday":
        Console.WriteLine("Weekend \u2013 rest up!");
        break;
    default:
        Console.WriteLine("Unknown day.");
        break;
}
Console.WriteLine();

// --- Part 5: Switch expression (modern C#) ---
Console.WriteLine("--- Part 5: Switch Expression ---");

string taskStatus = "InProgress";

string statusDescription = taskStatus switch
{
    "New" => "Task has not been started yet.",
    "InProgress" => "Task is currently being worked on.",
    "Completed" => "Task is done!",
    "Cancelled" => "Task was cancelled.",
    _ => "Unknown status."
};

Console.WriteLine($"Status '{taskStatus}': {statusDescription}");
Console.WriteLine();

// --- Part 6: for loop ---
Console.WriteLine("--- Part 6: for Loop ---");

Console.WriteLine("Counting from 1 to 5:");
for (int i = 1; i <= 5; i++)
{
    Console.WriteLine($"  i = {i}");
}

Console.WriteLine("Multiplication table of 7:");
for (int i = 1; i <= 10; i++)
{
    Console.WriteLine($"  7 x {i} = {7 * i}");
}
Console.WriteLine();

// --- Part 7: foreach loop ---
Console.WriteLine("--- Part 7: foreach Loop ---");

string[] languages = ["C#", "JavaScript", "TypeScript", "Python", "Go"];

Console.WriteLine("Languages we can use with .NET ecosystem:");
foreach (string lang in languages)
{
    Console.WriteLine($"  - {lang}");
}
Console.WriteLine();

// --- Part 8: while loop ---
Console.WriteLine("--- Part 8: while Loop ---");

int countdown = 5;
Console.WriteLine("Countdown:");
while (countdown > 0)
{
    Console.WriteLine($"  {countdown}...");
    countdown--;
}
Console.WriteLine("  Liftoff!");
Console.WriteLine();

// --- Part 9: do-while loop ---
Console.WriteLine("--- Part 9: do-while Loop ---");

int attempt = 0;
int maxAttempts = 3;

do
{
    attempt++;
    Console.WriteLine($"  Attempt {attempt} of {maxAttempts}");
} while (attempt < maxAttempts);
Console.WriteLine();

// --- Part 10: Pattern matching ---
Console.WriteLine("--- Part 10: Pattern Matching ---");

object[] items = [42, "hello", 3.14, true, null!, new int[] { 1, 2, 3 }];

foreach (object item in items)
{
    string description = item switch
    {
        int n when n > 0 => $"Positive integer: {n}",
        int n => $"Non-positive integer: {n}",
        string s => $"String of length {s.Length}: \"{s}\"",
        double d => $"Double value: {d}",
        bool b => $"Boolean: {b}",
        int[] arr => $"Int array with {arr.Length} elements",
        null => "Null value",
        _ => $"Something else: {item}"
    };

    Console.WriteLine($"  {description}");
}
Console.WriteLine();

// --- Part 11: Practical example \u2013 Order Validation ---
Console.WriteLine("--- Part 11: Practical Example \u2013 Order Validation ---");

string[] orders = ["ORD-001", "ORD-002", "", "ORD-004", "ORD-005"];
decimal[] amounts = [150.00m, 0m, 75.50m, -10m, 250.00m];

for (int i = 0; i < orders.Length; i++)
{
    string orderId = orders[i];
    decimal amount = amounts[i];

    if (string.IsNullOrEmpty(orderId))
    {
        Console.WriteLine($"  Order {i + 1}: SKIPPED \u2013 empty order ID");
        continue;
    }

    if (amount <= 0)
    {
        Console.WriteLine($"  Order {i + 1} ({orderId}): INVALID \u2013 amount must be positive (was {amount})");
        continue;
    }

    string priority = amount switch
    {
        >= 200m => "HIGH",
        >= 100m => "MEDIUM",
        _ => "LOW"
    };

    Console.WriteLine($"  Order {i + 1} ({orderId}): VALID \u2013 ${amount} \u2013 Priority: {priority}");
}
Console.WriteLine();

Console.WriteLine("Workshop 03 complete!");
