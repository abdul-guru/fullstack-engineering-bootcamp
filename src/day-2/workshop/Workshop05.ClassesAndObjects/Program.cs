// ============================================================================
// Workshop 05: Classes and Objects
// ============================================================================
// Topics covered:
//   - Class definition and instantiation
//   - Properties (auto, computed, init-only)
//   - Constructors
//   - Object initializer syntax
//   - Records
//   - Sealed classes
//   - ToString() override
// ============================================================================

using Workshop05.ClassesAndObjects;

Console.WriteLine("========================================");
Console.WriteLine("  Workshop 05 \u2013 Classes and Objects");
Console.WriteLine("========================================");
Console.WriteLine();

// --- Part 1: Simple class with properties ---
Console.WriteLine("--- Part 1: Simple Class ---");

var task1 = new TaskItem();
task1.Title = "Create Web API";
task1.Status = "InProgress";

Console.WriteLine($"Task: {task1.Title}");
Console.WriteLine($"  Id:     {task1.Id}");
Console.WriteLine($"  Status: {task1.Status}");
Console.WriteLine();

// --- Part 2: Object initializer syntax ---
Console.WriteLine("--- Part 2: Object Initializer ---");

var task2 = new TaskItem
{
    Title = "Write unit tests",
    Status = "New"
};

var task3 = new TaskItem
{
    Title = "Deploy to staging",
    Status = "Completed"
};

Console.WriteLine(task2);
Console.WriteLine(task3);
Console.WriteLine();

// --- Part 3: Constructor ---
Console.WriteLine("--- Part 3: Constructor ---");

var employee = new Employee("Jane Smith", "Engineering", 95000m);
Console.WriteLine(employee);
Console.WriteLine($"  Annual bonus: {employee.CalculateBonus():C}");
Console.WriteLine();

var manager = new Employee("Bob Johnson", "Engineering", 120000m);
Console.WriteLine(manager);
Console.WriteLine($"  Annual bonus: {manager.CalculateBonus():C}");
Console.WriteLine();

// --- Part 4: Computed properties ---
Console.WriteLine("--- Part 4: Computed Properties ---");

var rect = new Rectangle(10, 5);
Console.WriteLine($"Rectangle: {rect.Width} x {rect.Height}");
Console.WriteLine($"  Area:      {rect.Area}");
Console.WriteLine($"  Perimeter: {rect.Perimeter}");
Console.WriteLine($"  IsSquare:  {rect.IsSquare}");

var square = new Rectangle(7, 7);
Console.WriteLine($"Square: {square.Width} x {square.Height}");
Console.WriteLine($"  IsSquare: {square.IsSquare}");
Console.WriteLine();

// --- Part 5: Records (immutable data) ---
Console.WriteLine("--- Part 5: Records ---");

var address1 = new Address("123 Main St", "Seattle", "WA", "98101");
Console.WriteLine($"Address: {address1}");

// Records support value equality
var address2 = new Address("123 Main St", "Seattle", "WA", "98101");
Console.WriteLine($"address1 == address2: {address1 == address2}");

// With-expression: create a copy with one property changed
var address3 = address1 with { City = "Portland", State = "OR" };
Console.WriteLine($"Modified: {address3}");
Console.WriteLine();

// --- Part 6: Sealed class ---
Console.WriteLine("--- Part 6: Sealed Class (like TaskItem in the slides) ---");

var items = new List<TaskItem>
{
    new() { Title = "Design database schema", Status = "Completed" },
    new() { Title = "Implement API endpoints", Status = "InProgress" },
    new() { Title = "Write documentation", Status = "New" },
    new() { Title = "Code review", Status = "New" }
};

Console.WriteLine("All tasks:");
foreach (var item in items)
{
    Console.WriteLine($"  {item}");
}
Console.WriteLine();

// --- Part 7: Working with a list of objects ---
Console.WriteLine("--- Part 7: Working with Object Collections ---");

var team = new List<Employee>
{
    new("Alice Chen", "Engineering", 105000m),
    new("Bob Kim", "Design", 92000m),
    new("Carol Davis", "Engineering", 98000m),
    new("Dan Lee", "Product", 110000m)
};

Console.WriteLine("Team members:");
foreach (var member in team)
{
    Console.WriteLine($"  {member} | Bonus: {member.CalculateBonus():C}");
}
Console.WriteLine();

Console.WriteLine("Workshop 05 complete!");

// ============================================================================
// Class definitions (in a real project, each would be in its own file)
// ============================================================================

namespace Workshop05.ClassesAndObjects
{
    /// A sealed class: cannot be inherited.
    /// Matches the pattern from the slides.
    public sealed class TaskItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString()[..8];
        public string Title { get; set; } = "";
        public string Status { get; set; } = "New";

        public override string ToString() => $"[{Status}] {Title} (id: {Id})";
    }

    /// A class with a constructor and a method.
    public class Employee
    {
        public string Name { get; }
        public string Department { get; }
        public decimal Salary { get; }

        public Employee(string name, string department, decimal salary)
        {
            Name = name;
            Department = department;
            Salary = salary;
        }

        public decimal CalculateBonus() => Salary * 0.10m;

        public override string ToString() => $"{Name} ({Department}) - {Salary:C}";
    }

    /// A class demonstrating computed (read-only) properties.
    public class Rectangle
    {
        public double Width { get; }
        public double Height { get; }

        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public double Area => Width * Height;
        public double Perimeter => 2 * (Width + Height);
        public bool IsSquare => Math.Abs(Width - Height) < 0.001;
    }

    /// A record: immutable, value equality, with-expressions.
    public record Address(string Street, string City, string State, string ZipCode);
}
