// ============================================================================
// Workshop 04: Methods and Clean Code
// ============================================================================
// Topics covered:
//   - Method declaration (parameters, return types)
//   - Single responsibility principle for methods
//   - Clean naming conventions
//   - Refactoring from messy to clean code
//   - Expression-bodied methods
//   - Optional and default parameters
//   - Method overloading
// ============================================================================

Console.WriteLine("========================================");
Console.WriteLine("  Workshop 04 \u2013 Methods and Clean Code");
Console.WriteLine("========================================");
Console.WriteLine();

// --- Part 1: Basic methods ---
Console.WriteLine("--- Part 1: Basic Methods ---");

GreetUser("Alice");
GreetUser("Bob");
Console.WriteLine();

// --- Part 2: Methods with return values ---
Console.WriteLine("--- Part 2: Methods with Return Values ---");

int sum = Add(10, 25);
Console.WriteLine($"Add(10, 25) = {sum}");

double avg = CalculateAverage(85, 92, 78, 95, 88);
Console.WriteLine($"Average of scores = {avg:F1}");
Console.WriteLine();

// --- Part 3: Expression-bodied methods ---
Console.WriteLine("--- Part 3: Expression-Bodied Methods ---");

Console.WriteLine($"Square(7) = {Square(7)}");
Console.WriteLine($"IsEven(4) = {IsEven(4)}");
Console.WriteLine($"IsEven(7) = {IsEven(7)}");
Console.WriteLine($"FormatCurrency(1234.5) = {FormatCurrency(1234.5m)}");
Console.WriteLine();

// --- Part 4: Default and optional parameters ---
Console.WriteLine("--- Part 4: Default and Optional Parameters ---");

PrintLog("Application started");
PrintLog("User logged in", "INFO");
PrintLog("Disk space low", "WARNING");
PrintLog("Connection failed", "ERROR");
Console.WriteLine();

// --- Part 5: Method overloading ---
Console.WriteLine("--- Part 5: Method Overloading ---");

Console.WriteLine($"Area of square (5): {CalculateSquareArea(5)}");
Console.WriteLine($"Area of rectangle (5, 8): {CalculateRectangleArea(5, 8)}");
Console.WriteLine($"Area of circle (radius 3): {CalculateCircleArea(3.0):F2}");
Console.WriteLine();

// --- Part 6: Bad code vs Clean code ---
Console.WriteLine("--- Part 6: Bad Code vs Clean Code ---");
Console.WriteLine();

// BAD: One method doing too many things with poor naming
Console.WriteLine("BAD example (do not write code like this):");
DoStuff(100, 20); // What does this do? Unclear!
Console.WriteLine();

// GOOD: Each method has one job with clear naming
Console.WriteLine("GOOD example (clean, readable code):");
decimal subtotal = 100m;
decimal taxRate = 0.20m;
decimal taxAmount = CalculateTax(subtotal, taxRate);
decimal total = CalculateTotal(subtotal, taxAmount);
string receipt = FormatReceipt(subtotal, taxAmount, total);
Console.WriteLine(receipt);
Console.WriteLine();

// --- Part 7: Refactoring exercise ---
Console.WriteLine("--- Part 7: Refactoring Example ---");
Console.WriteLine();

// BEFORE refactoring: one big messy method
Console.WriteLine("BEFORE refactoring:");
ProcessOrderBad("ORD-001", 5, 29.99m, true);
Console.WriteLine();

// AFTER refactoring: clean, separated responsibilities
Console.WriteLine("AFTER refactoring:");
ProcessOrderClean("ORD-001", 5, 29.99m, isPremiumCustomer: true);
Console.WriteLine();

Console.WriteLine("Workshop 04 complete!");

// ============================================================================
// Method definitions
// ============================================================================

void GreetUser(string name)
{
    Console.WriteLine($"Hello, {name}! Welcome to the workshop.");
}

int Add(int a, int b)
{
    return a + b;
}

double CalculateAverage(params int[] scores)
{
    if (scores.Length == 0) return 0;
    double total = 0;
    foreach (int s in scores) total += s;
    return total / scores.Length;
}

// Expression-bodied methods: concise for simple operations
int Square(int n) => n * n;
bool IsEven(int n) => n % 2 == 0;
string FormatCurrency(decimal amount) => $"${amount:N2}";

void PrintLog(string message, string level = "DEBUG")
{
    Console.WriteLine($"[{level}] {DateTime.Now:HH:mm:ss} - {message}");
}

// Overloaded methods (note: local functions can't share names, so we use distinct names)
double CalculateSquareArea(int side) => side * side;
double CalculateRectangleArea(int length, int width) => length * width;
double CalculateCircleArea(double radius) => Math.PI * radius * radius;

// BAD method: does too much, poor naming
void DoStuff(decimal a, decimal b)
{
    var t = a * (b / 100); // what is t?
    var r = a + t;          // what is r?
    Console.WriteLine($"  a={a}, b={b}, t={t}, r={r}"); // meaningless output
}

// GOOD methods: each has one clear responsibility
decimal CalculateTax(decimal subtotal, decimal taxRate) => subtotal * taxRate;
decimal CalculateTotal(decimal subtotal, decimal tax) => subtotal + tax;

string FormatReceipt(decimal subtotal, decimal tax, decimal total)
{
    return $"  Subtotal: {FormatCurrency(subtotal)}\n" +
           $"  Tax:      {FormatCurrency(tax)}\n" +
           $"  Total:    {FormatCurrency(total)}";
}

// BAD: messy combined method
void ProcessOrderBad(string id, int qty, decimal price, bool premium)
{
    var s = qty * price;
    if (premium) s *= 0.9m;
    var t = s * 0.08m;
    Console.WriteLine($"  Order {id}: qty={qty} price={price} sub={s} tax={t} total={s + t} premium={premium}");
}

// GOOD: refactored with clear steps
void ProcessOrderClean(string orderId, int quantity, decimal unitPrice, bool isPremiumCustomer)
{
    decimal subtotal2 = CalculateSubtotal(quantity, unitPrice);
    decimal discount = isPremiumCustomer ? CalculatePremiumDiscount(subtotal2) : 0m;
    decimal discountedSubtotal = subtotal2 - discount;
    decimal tax2 = CalculateSalesTax(discountedSubtotal);
    decimal orderTotal = discountedSubtotal + tax2;

    PrintOrderSummary(orderId, subtotal2, discount, tax2, orderTotal);
}

decimal CalculateSubtotal(int quantity, decimal unitPrice) => quantity * unitPrice;
decimal CalculatePremiumDiscount(decimal subtotal) => subtotal * 0.10m;
decimal CalculateSalesTax(decimal amount) => amount * 0.08m;

void PrintOrderSummary(string orderId, decimal subtotal, decimal discount, decimal tax, decimal total)
{
    Console.WriteLine($"  Order: {orderId}");
    Console.WriteLine($"    Subtotal:  {FormatCurrency(subtotal)}");
    if (discount > 0)
        Console.WriteLine($"    Discount:  -{FormatCurrency(discount)} (premium)");
    Console.WriteLine($"    Tax:       {FormatCurrency(tax)}");
    Console.WriteLine($"    Total:     {FormatCurrency(total)}");
}
