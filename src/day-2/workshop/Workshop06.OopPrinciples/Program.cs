// ============================================================================
// Workshop 06: OOP Principles
// ============================================================================
// Topics covered:
//   - Encapsulation: hiding internal state, exposing behavior
//   - Inheritance: base classes and derived classes
//   - Polymorphism: virtual/override methods, interfaces
//   - Composition over inheritance
//   - Interfaces for contracts
//   - The "who should own this behavior?" question
// ============================================================================

using Workshop06.OopPrinciples;

Console.WriteLine("========================================");
Console.WriteLine("  Workshop 06 \u2013 OOP Principles");
Console.WriteLine("========================================");
Console.WriteLine();

// --- Part 1: Encapsulation ---
Console.WriteLine("--- Part 1: Encapsulation ---");
Console.WriteLine("The Order class owns its state and validates changes.");
Console.WriteLine();

var order = new Order("Alice");
order.AddLine("WIDGET-A", 2, 15.00m);
order.AddLine("GADGET-B", 1, 45.50m);

Console.WriteLine($"Customer: {order.CustomerName}");
Console.WriteLine($"Lines:    {order.Lines.Count}");
Console.WriteLine($"Total:    {order.Total():C}");
Console.WriteLine();

// Try adding an invalid line
try
{
    order.AddLine("BAD-ITEM", 0, 10.00m); // qty <= 0 should fail
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Validation caught: {ex.Message}");
}
Console.WriteLine();

// --- Part 2: Interfaces ---
Console.WriteLine("--- Part 2: Interfaces (Contracts) ---");

INotificationService emailService = new EmailNotificationService();
INotificationService smsService = new SmsNotificationService();

emailService.Send("alice@example.com", "Your order has shipped!");
smsService.Send("+1-555-0100", "Your order has shipped!");
Console.WriteLine();

// --- Part 3: Inheritance and Polymorphism ---
Console.WriteLine("--- Part 3: Inheritance and Polymorphism ---");

Shape[] shapes =
[
    new Circle(5),
    new RectangleShape(4, 6),
    new Triangle(3, 7)
];

foreach (var shape in shapes)
{
    // Polymorphism: each shape computes its own area
    Console.WriteLine($"  {shape.Name}: Area = {shape.CalculateArea():F2}");
}
Console.WriteLine();

// --- Part 4: Composition over Inheritance ---
Console.WriteLine("--- Part 4: Composition over Inheritance ---");
Console.WriteLine("Instead of inheriting logger behavior, inject it.");
Console.WriteLine();

var logger = new ConsoleLogger();
var orderService = new OrderService(logger);
orderService.PlaceOrder("ORD-2024-001", 3, 29.99m);
Console.WriteLine();

// --- Part 5: Practical example combining OOP concepts ---
Console.WriteLine("--- Part 5: Practical Example \u2013 Payment Processing ---");

IPaymentProcessor[] processors =
[
    new CreditCardProcessor(),
    new PayPalProcessor(),
    new BankTransferProcessor()
];

foreach (var processor in processors)
{
    var result = processor.ProcessPayment(149.99m);
    Console.WriteLine($"  {processor.Name}: {(result ? "SUCCESS" : "FAILED")}");
}
Console.WriteLine();

Console.WriteLine("Workshop 06 complete!");

// ============================================================================
// Type definitions
// ============================================================================

namespace Workshop06.OopPrinciples
{
    // --- Encapsulation: Order class owns its data ---
    public class Order
    {
        private readonly List<OrderLine> _lines = new();

        public string CustomerName { get; }
        public IReadOnlyList<OrderLine> Lines => _lines;

        public Order(string customerName)
        {
            CustomerName = customerName;
        }

        public void AddLine(string sku, int quantity, decimal unitPrice)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");
            if (unitPrice < 0)
                throw new ArgumentException("Unit price cannot be negative.");

            _lines.Add(new OrderLine(sku, quantity, unitPrice));
        }

        public decimal Total() => _lines.Sum(l => l.Quantity * l.UnitPrice);
    }

    public record OrderLine(string Sku, int Quantity, decimal UnitPrice);

    // --- Interfaces ---
    public interface INotificationService
    {
        void Send(string recipient, string message);
    }

    public class EmailNotificationService : INotificationService
    {
        public void Send(string recipient, string message)
            => Console.WriteLine($"  [EMAIL] To: {recipient} | {message}");
    }

    public class SmsNotificationService : INotificationService
    {
        public void Send(string recipient, string message)
            => Console.WriteLine($"  [SMS] To: {recipient} | {message}");
    }

    // --- Inheritance and Polymorphism ---
    public abstract class Shape
    {
        public abstract string Name { get; }
        public abstract double CalculateArea();
    }

    public class Circle : Shape
    {
        public double Radius { get; }
        public override string Name => "Circle";

        public Circle(double radius) => Radius = radius;

        public override double CalculateArea() => Math.PI * Radius * Radius;
    }

    public class RectangleShape : Shape
    {
        public double Width { get; }
        public double Height { get; }
        public override string Name => "Rectangle";

        public RectangleShape(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public override double CalculateArea() => Width * Height;
    }

    public class Triangle : Shape
    {
        public double Base { get; }
        public double Height { get; }
        public override string Name => "Triangle";

        public Triangle(double @base, double height)
        {
            Base = @base;
            Height = height;
        }

        public override double CalculateArea() => 0.5 * Base * Height;
    }

    // --- Composition: inject dependencies instead of inheriting ---
    public class ConsoleLogger
    {
        public void Log(string message) => Console.WriteLine($"  [LOG] {message}");
    }

    public class OrderService
    {
        private readonly ConsoleLogger _logger;

        public OrderService(ConsoleLogger logger) => _logger = logger;

        public void PlaceOrder(string orderId, int quantity, decimal price)
        {
            _logger.Log($"Placing order {orderId}...");
            decimal total = quantity * price;
            _logger.Log($"Order {orderId} placed. Total: {total:C}");
        }
    }

    // --- Interface-based polymorphism for payment ---
    public interface IPaymentProcessor
    {
        string Name { get; }
        bool ProcessPayment(decimal amount);
    }

    public class CreditCardProcessor : IPaymentProcessor
    {
        public string Name => "Credit Card";
        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine($"    Processing ${amount} via credit card...");
            return true;
        }
    }

    public class PayPalProcessor : IPaymentProcessor
    {
        public string Name => "PayPal";
        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine($"    Processing ${amount} via PayPal...");
            return true;
        }
    }

    public class BankTransferProcessor : IPaymentProcessor
    {
        public string Name => "Bank Transfer";
        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine($"    Processing ${amount} via bank transfer...");
            return true;
        }
    }
}
