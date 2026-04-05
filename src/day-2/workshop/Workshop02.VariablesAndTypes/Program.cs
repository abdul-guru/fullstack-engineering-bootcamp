// ============================================================================
// Workshop 02: Variables and Types
// ============================================================================
// Topics covered:
//   - Value types: int, double, decimal, bool, char
//   - Reference types: string, object
//   - Type inference with var
//   - Constants
//   - Type conversions (implicit, explicit, parsing)
//   - Nullable value types
//   - String operations and interpolation
// ============================================================================

Console.WriteLine("========================================");
Console.WriteLine("  Workshop 02 \u2013 Variables and Types");
Console.WriteLine("========================================");
Console.WriteLine();

// --- Part 1: Value types ---
Console.WriteLine("--- Part 1: Value Types ---");

int age = 25;
double temperature = 36.6;
decimal price = 19.99m;
bool isActive = true;
char grade = 'A';

Console.WriteLine($"int     : age = {age}");
Console.WriteLine($"double  : temperature = {temperature}");
Console.WriteLine($"decimal : price = {price}");
Console.WriteLine($"bool    : isActive = {isActive}");
Console.WriteLine($"char    : grade = {grade}");
Console.WriteLine();

// --- Part 2: Reference types ---
Console.WriteLine("--- Part 2: Reference Types ---");

string greeting = "Hello, C#!";
object anything = 42;

Console.WriteLine($"string : greeting = {greeting}");
Console.WriteLine($"object : anything = {anything} (type: {anything.GetType().Name})");

anything = "Now I'm a string";
Console.WriteLine($"object : anything = {anything} (type: {anything.GetType().Name})");
Console.WriteLine();

// --- Part 3: Type inference with var ---
Console.WriteLine("--- Part 3: Type Inference (var) ---");

var count = 10;           // inferred as int
var message = "Hi there"; // inferred as string
var ratio = 3.14;         // inferred as double
var isReady = false;      // inferred as bool

Console.WriteLine($"var count   -> {count.GetType().Name} = {count}");
Console.WriteLine($"var message -> {message.GetType().Name} = {message}");
Console.WriteLine($"var ratio   -> {ratio.GetType().Name} = {ratio}");
Console.WriteLine($"var isReady -> {isReady.GetType().Name} = {isReady}");
Console.WriteLine();

// --- Part 4: Constants ---
Console.WriteLine("--- Part 4: Constants ---");

const double Pi = 3.14159265358979;
const int MaxRetries = 3;
const string AppName = "TaskFlow";

Console.WriteLine($"Pi = {Pi}");
Console.WriteLine($"MaxRetries = {MaxRetries}");
Console.WriteLine($"AppName = {AppName}");
// Pi = 3.0;  // This would cause a compile error: cannot assign to a constant
Console.WriteLine();

// --- Part 5: Type conversions ---
Console.WriteLine("--- Part 5: Type Conversions ---");

// Implicit conversion (safe, no data loss)
int smallNumber = 100;
long bigNumber = smallNumber;     // int -> long is safe
double doubleValue = smallNumber; // int -> double is safe
Console.WriteLine($"Implicit: int {smallNumber} -> long {bigNumber} -> double {doubleValue}");

// Explicit conversion (cast, possible data loss)
double preciseValue = 9.78;
int truncated = (int)preciseValue;  // loses decimal part
Console.WriteLine($"Explicit: double {preciseValue} -> int {truncated} (truncated)");

// Parsing strings to numbers
string numberText = "42";
int parsed = int.Parse(numberText);
Console.WriteLine($"Parse: \"{numberText}\" -> int {parsed}");

// Safe parsing with TryParse
string badInput = "not-a-number";
bool success = int.TryParse(badInput, out int result);
Console.WriteLine($"TryParse: \"{badInput}\" -> success={success}, result={result}");

// Convert class
string boolText = "true";
bool converted = Convert.ToBoolean(boolText);
Console.WriteLine($"Convert: \"{boolText}\" -> bool {converted}");
Console.WriteLine();

// --- Part 6: Nullable value types ---
Console.WriteLine("--- Part 6: Nullable Value Types ---");

int? nullableAge = null;
Console.WriteLine($"nullableAge = {nullableAge?.ToString() ?? "(null)"}");
Console.WriteLine($"Has value? {nullableAge.HasValue}");

nullableAge = 30;
Console.WriteLine($"nullableAge = {nullableAge}");
Console.WriteLine($"Has value? {nullableAge.HasValue}");

// Null-coalescing operator
int actualAge = nullableAge ?? 0;
Console.WriteLine($"actualAge (with ??) = {actualAge}");
Console.WriteLine();

// --- Part 7: String operations ---
Console.WriteLine("--- Part 7: String Operations ---");

string firstName = "Jane";
string lastName = "Smith";

// Concatenation
string fullName = firstName + " " + lastName;
Console.WriteLine($"Concatenation: {fullName}");

// Interpolation
Console.WriteLine($"Interpolation: {firstName} {lastName}");

// String methods
string text = "  Hello, World!  ";
Console.WriteLine($"Original:  \"{text}\"");
Console.WriteLine($"Trimmed:   \"{text.Trim()}\"");
Console.WriteLine($"Upper:     \"{text.Trim().ToUpper()}\"");
Console.WriteLine($"Lower:     \"{text.Trim().ToLower()}\"");
Console.WriteLine($"Contains:  {text.Contains("World")}");
Console.WriteLine($"Replace:   \"{text.Trim().Replace("World", "C#")}\"");
Console.WriteLine($"Length:    {text.Trim().Length}");
Console.WriteLine($"Substring: \"{text.Trim().Substring(0, 5)}\"");

// Verbatim string
string path = @"C:\Users\Projects\Code";
Console.WriteLine($"Verbatim string: {path}");
Console.WriteLine();

Console.WriteLine("Workshop 02 complete!");
