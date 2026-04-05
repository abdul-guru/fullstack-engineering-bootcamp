// ============================================================================
// Workshop 08: Async/Await
// ============================================================================
// Topics covered:
//   - Understanding async as a control-flow tool for I/O
//   - Task and Task<T>
//   - async/await keywords
//   - Simulating async I/O operations
//   - Running multiple async operations
//   - Why to avoid .Result and .Wait()
//   - Async naming conventions (Async suffix)
// ============================================================================

Console.WriteLine("========================================");
Console.WriteLine("  Workshop 08 – Async/Await");
Console.WriteLine("========================================");
Console.WriteLine();

// --- Part 1: Synchronous vs Asynchronous mental model ---
Console.WriteLine("--- Part 1: Why Async? ---");
Console.WriteLine("Async lets the thread do other work while waiting for I/O.");
Console.WriteLine("Think of it as: Start I/O → Await → Resume when result arrives.");
Console.WriteLine();

// --- Part 2: Simple async method ---
Console.WriteLine("--- Part 2: Simple Async Method ---");

string greeting = await GetGreetingAsync("Workshop Team");
Console.WriteLine(greeting);
Console.WriteLine();

// --- Part 3: Simulating database call ---
Console.WriteLine("--- Part 3: Simulating a Database Call ---");

var user = await GetUserByIdAsync(42);
if (user is not null)
{
    Console.WriteLine($"Found user: {user}");
}
Console.WriteLine();

// --- Part 4: Multiple sequential async calls ---
Console.WriteLine("--- Part 4: Sequential Async Calls ---");

var stopwatch = System.Diagnostics.Stopwatch.StartNew();

var weather = await FetchWeatherAsync("Seattle");
var news = await FetchNewsAsync("technology");
var stock = await FetchStockPriceAsync("MSFT");

stopwatch.Stop();

Console.WriteLine($"  Weather: {weather}");
Console.WriteLine($"  News:    {news}");
Console.WriteLine($"  Stock:   {stock}");
Console.WriteLine($"  Sequential time: {stopwatch.ElapsedMilliseconds}ms");
Console.WriteLine();

// --- Part 5: Parallel async calls with Task.WhenAll ---
Console.WriteLine("--- Part 5: Parallel Async Calls (Task.WhenAll) ---");

stopwatch.Restart();

var weatherTask = FetchWeatherAsync("Seattle");
var newsTask = FetchNewsAsync("technology");
var stockTask = FetchStockPriceAsync("MSFT");

await Task.WhenAll(weatherTask, newsTask, stockTask);

stopwatch.Stop();

Console.WriteLine($"  Weather: {weatherTask.Result}");
Console.WriteLine($"  News:    {newsTask.Result}");
Console.WriteLine($"  Stock:   {stockTask.Result}");
Console.WriteLine($"  Parallel time: {stopwatch.ElapsedMilliseconds}ms");
Console.WriteLine("  (Notice: parallel is faster because calls run concurrently!)");
Console.WriteLine();

// --- Part 6: Async file I/O ---
Console.WriteLine("--- Part 6: Async File I/O ---");

string tempFile = Path.Combine(Path.GetTempPath(), "workshop08_demo.txt");

await WriteFileAsync(tempFile, "Hello from async file I/O!\nThis was written asynchronously.");
Console.WriteLine($"  Written to: {tempFile}");

string content = await ReadFileAsync(tempFile);
Console.WriteLine($"  Read back: {content}");

// Clean up
File.Delete(tempFile);
Console.WriteLine();

// --- Part 7: Error handling in async ---
Console.WriteLine("--- Part 7: Error Handling in Async ---");

try
{
    await FetchDataWithErrorAsync();
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"  Caught async error: {ex.Message}");
}
Console.WriteLine();

// --- Part 8: Async best practices ---
Console.WriteLine("--- Part 8: Async Best Practices ---");
Console.WriteLine("  1. Name async methods with 'Async' suffix");
Console.WriteLine("  2. Keep the entire call chain async");
Console.WriteLine("  3. Use async for I/O-bound work, not CPU-bound");
Console.WriteLine("  4. AVOID .Result and .Wait() in application code");
Console.WriteLine("  5. Use Task.WhenAll for independent parallel operations");
Console.WriteLine("  6. Always await or return the Task");
Console.WriteLine();

Console.WriteLine("Workshop 08 complete!");

// ============================================================================
// Async method definitions
// ============================================================================

async Task<string> GetGreetingAsync(string name)
{
    // Simulate async work (e.g., fetching from a service)
    await Task.Delay(100);
    return $"Hello, {name}! This greeting was fetched asynchronously.";
}

async Task<UserDto?> GetUserByIdAsync(int id)
{
    // Simulate a database lookup
    Console.WriteLine($"  Fetching user {id} from database...");
    await Task.Delay(200);

    // Simulate returning data
    return new UserDto(id, "Jane Smith", "jane@example.com");
}

async Task<string> FetchWeatherAsync(string city)
{
    await Task.Delay(300); // simulate API call
    return $"{city}: 72°F, Partly Cloudy";
}

async Task<string> FetchNewsAsync(string topic)
{
    await Task.Delay(300); // simulate API call
    return $"Latest {topic} news: .NET 10 released!";
}

async Task<string> FetchStockPriceAsync(string symbol)
{
    await Task.Delay(300); // simulate API call
    return $"{symbol}: $425.30 (+1.2%)";
}

async Task WriteFileAsync(string path, string content)
{
    await File.WriteAllTextAsync(path, content);
}

async Task<string> ReadFileAsync(string path)
{
    return await File.ReadAllTextAsync(path);
}

async Task FetchDataWithErrorAsync()
{
    await Task.Delay(100);
    throw new HttpRequestException("Connection to API timed out (simulated error)");
}

// Simple DTO for the async example
record UserDto(int Id, string Name, string Email);
