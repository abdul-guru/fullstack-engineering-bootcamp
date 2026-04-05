// ============================================================================
// Workshop 07: Collections and LINQ
// ============================================================================
// Topics covered:
//   - List<T> for ordered sequences
//   - Dictionary<TKey,TValue> for lookup
//   - HashSet<T> for uniqueness
//   - LINQ: Where, Select, OrderBy, GroupBy, First, Any, All, Sum, Average
//   - Method syntax vs query syntax
//   - Practical data transformation examples
// ============================================================================

Console.WriteLine("========================================");
Console.WriteLine("  Workshop 07 \u2013 Collections and LINQ");
Console.WriteLine("========================================");
Console.WriteLine();

// --- Sample data ---
var products = new List<Product>
{
    new("Laptop",      "Electronics", 999.99m,  15),
    new("Keyboard",    "Electronics", 79.99m,   150),
    new("Mouse",       "Electronics", 29.99m,   200),
    new("Desk",        "Furniture",   349.99m,  25),
    new("Chair",       "Furniture",   499.99m,  30),
    new("Monitor",     "Electronics", 399.99m,  45),
    new("Notebook",    "Stationery",  4.99m,    500),
    new("Pen Pack",    "Stationery",  12.99m,   300),
    new("Webcam",      "Electronics", 69.99m,   80),
    new("Bookshelf",   "Furniture",   199.99m,  20)
};

// --- Part 1: List<T> basics ---
Console.WriteLine("--- Part 1: List<T> Basics ---");

var names = new List<string> { "Alice", "Bob", "Carol" };
names.Add("Dan");
names.Insert(1, "Eve"); // insert at index 1
names.Remove("Bob");

Console.WriteLine($"Count: {names.Count}");
Console.WriteLine($"Contains 'Carol': {names.Contains("Carol")}");
Console.WriteLine($"Items: {string.Join(", ", names)}");
Console.WriteLine();

// --- Part 2: Dictionary<TKey,TValue> ---
Console.WriteLine("--- Part 2: Dictionary ---");

var countryCodes = new Dictionary<string, string>
{
    ["US"] = "United States",
    ["GB"] = "United Kingdom",
    ["JP"] = "Japan",
    ["DE"] = "Germany"
};

countryCodes["FR"] = "France"; // add new entry

foreach (var kvp in countryCodes)
{
    Console.WriteLine($"  {kvp.Key} => {kvp.Value}");
}

// Safe lookup
if (countryCodes.TryGetValue("JP", out string? country))
{
    Console.WriteLine($"  Found: JP = {country}");
}
Console.WriteLine();

// --- Part 3: HashSet<T> ---
Console.WriteLine("--- Part 3: HashSet<T> (Uniqueness) ---");

var tags = new HashSet<string>();
tags.Add("csharp");
tags.Add("dotnet");
tags.Add("csharp"); // duplicate, will not be added
tags.Add("api");
tags.Add("dotnet"); // duplicate

Console.WriteLine($"Unique tags ({tags.Count}): {string.Join(", ", tags)}");
Console.WriteLine();

// --- Part 4: LINQ - Where (filtering) ---
Console.WriteLine("--- Part 4: LINQ \u2013 Where (Filtering) ---");

var expensiveProducts = products.Where(p => p.Price > 100m).ToList();

Console.WriteLine("Products over $100:");
foreach (var p in expensiveProducts)
{
    Console.WriteLine($"  {p.Name,-15} {p.Price,10:C}");
}
Console.WriteLine();

// --- Part 5: LINQ - Select (projection) ---
Console.WriteLine("--- Part 5: LINQ \u2013 Select (Projection) ---");

var productSummaries = products
    .Select(p => new { p.Name, InventoryValue = p.Price * p.Stock })
    .OrderByDescending(p => p.InventoryValue)
    .ToList();

Console.WriteLine("Product inventory values:");
foreach (var p in productSummaries)
{
    Console.WriteLine($"  {p.Name,-15} {p.InventoryValue,12:C}");
}
Console.WriteLine();

// --- Part 6: LINQ - OrderBy ---
Console.WriteLine("--- Part 6: LINQ \u2013 OrderBy ---");

var cheapest = products.OrderBy(p => p.Price).Take(3).ToList();

Console.WriteLine("3 cheapest products:");
foreach (var p in cheapest)
{
    Console.WriteLine($"  {p.Name,-15} {p.Price,10:C}");
}
Console.WriteLine();

// --- Part 7: LINQ - GroupBy ---
Console.WriteLine("--- Part 7: LINQ \u2013 GroupBy ---");

var byCategory = products
    .GroupBy(p => p.Category)
    .Select(g => new
    {
        Category = g.Key,
        Count = g.Count(),
        AveragePrice = g.Average(p => p.Price),
        TotalValue = g.Sum(p => p.Price * p.Stock)
    })
    .OrderBy(g => g.Category)
    .ToList();

Console.WriteLine("Products by category:");
foreach (var g in byCategory)
{
    Console.WriteLine($"  {g.Category,-15} Count: {g.Count}  Avg: {g.AveragePrice,8:C}  Total Value: {g.TotalValue,12:C}");
}
Console.WriteLine();

// --- Part 8: LINQ - First, Any, All ---
Console.WriteLine("--- Part 8: LINQ \u2013 First, Any, All ---");

var firstElectronics = products.First(p => p.Category == "Electronics");
Console.WriteLine($"First electronics item: {firstElectronics.Name}");

bool anyOutOfStock = products.Any(p => p.Stock == 0);
Console.WriteLine($"Any out of stock? {anyOutOfStock}");

bool allInStock = products.All(p => p.Stock > 0);
Console.WriteLine($"All in stock? {allInStock}");

var totalProducts = products.Count;
var totalValue = products.Sum(p => p.Price * p.Stock);
var avgPrice = products.Average(p => p.Price);

Console.WriteLine($"Total products: {totalProducts}");
Console.WriteLine($"Total inventory value: {totalValue:C}");
Console.WriteLine($"Average price: {avgPrice:C}");
Console.WriteLine();

// --- Part 9: LINQ - Chaining (filter -> sort -> project) ---
Console.WriteLine("--- Part 9: LINQ \u2013 Chaining (Narrative Query) ---");
Console.WriteLine("The slides say: Readable LINQ beats clever LINQ.");
Console.WriteLine("Use query steps as a narrative: filter -> sort -> project.");
Console.WriteLine();

var report = products
    .Where(p => p.Category == "Electronics")     // filter
    .Where(p => p.Price > 50m)                    // filter more
    .OrderByDescending(p => p.Price)               // sort
    .Select(p => new                               // project
    {
        p.Name,
        p.Price,
        StockStatus = p.Stock > 100 ? "Well Stocked" : "Low Stock"
    })
    .ToList();

Console.WriteLine("Electronics over $50:");
foreach (var item in report)
{
    Console.WriteLine($"  {item.Name,-15} {item.Price,10:C}  [{item.StockStatus}]");
}
Console.WriteLine();

// --- Part 10: Dictionary from LINQ ---
Console.WriteLine("--- Part 10: Building a Dictionary with LINQ ---");

var priceLookup = products.ToDictionary(p => p.Name, p => p.Price);

Console.WriteLine($"Laptop price: {priceLookup["Laptop"]:C}");
Console.WriteLine($"Chair price:  {priceLookup["Chair"]:C}");
Console.WriteLine();

Console.WriteLine("Workshop 07 complete!");

// --- Data model ---
record Product(string Name, string Category, decimal Price, int Stock);
