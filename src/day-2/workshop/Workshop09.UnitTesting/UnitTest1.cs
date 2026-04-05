// ============================================================================
// Workshop 09: Unit Testing with xUnit
// ============================================================================
// Topics covered:
//   - [Fact] tests for single assertions
//   - [Theory] with [InlineData] for parameterized tests
//   - Arrange / Act / Assert pattern
//   - Testing business rules and edge cases
//   - Test naming: reads like a requirement
//   - Assert methods: Equal, True, False, Null, NotNull, Throws, Contains
// ============================================================================

namespace Workshop09.UnitTesting;

// ============================================================================
// Part 1: Basic [Fact] tests
// ============================================================================

public class CalculatorTests
{
    [Fact]
    public void Add_returns_sum_of_two_numbers()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        int result = calculator.Add(3, 5);

        // Assert
        Assert.Equal(8, result);
    }

    [Fact]
    public void Add_with_negative_numbers_returns_correct_sum()
    {
        var calculator = new Calculator();

        int result = calculator.Add(-3, -7);

        Assert.Equal(-10, result);
    }

    [Fact]
    public void Divide_by_zero_throws_DivideByZeroException()
    {
        var calculator = new Calculator();

        Assert.Throws<DivideByZeroException>(() => calculator.Divide(10, 0));
    }

    [Fact]
    public void Divide_returns_correct_quotient()
    {
        var calculator = new Calculator();

        double result = calculator.Divide(10, 4);

        Assert.Equal(2.5, result);
    }
}

// ============================================================================
// Part 2: [Theory] with [InlineData] for parameterized tests
// ============================================================================

public class GradeCalculatorTests
{
    [Theory]
    [InlineData(95, "A")]
    [InlineData(85, "B")]
    [InlineData(75, "C")]
    [InlineData(65, "D")]
    [InlineData(50, "F")]
    public void GetGrade_returns_correct_grade_for_score(int score, string expectedGrade)
    {
        string grade = GradeCalculator.GetGrade(score);

        Assert.Equal(expectedGrade, grade);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void GetGrade_throws_for_invalid_score(int invalidScore)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => GradeCalculator.GetGrade(invalidScore));
    }
}

// ============================================================================
// Part 3: Testing an Order class (from the slides)
// ============================================================================

public class OrderTests
{
    [Fact]
    public void Total_returns_sum_of_order_lines()
    {
        // Arrange
        var order = new Order();
        order.AddLine("A-1", 2, 10m);
        order.AddLine("B-2", 1, 25m);

        // Act
        decimal total = order.Total();

        // Assert
        Assert.Equal(45m, total);
    }

    [Fact]
    public void Total_returns_zero_for_empty_order()
    {
        var order = new Order();

        Assert.Equal(0m, order.Total());
    }

    [Fact]
    public void AddLine_with_zero_quantity_throws()
    {
        var order = new Order();

        Assert.Throws<ArgumentException>(() => order.AddLine("X-1", 0, 10m));
    }

    [Fact]
    public void AddLine_with_negative_quantity_throws()
    {
        var order = new Order();

        Assert.Throws<ArgumentException>(() => order.AddLine("X-1", -1, 10m));
    }

    [Fact]
    public void Lines_returns_readonly_list()
    {
        var order = new Order();
        order.AddLine("A-1", 1, 5m);

        var lines = order.Lines;

        Assert.Single(lines);
        Assert.Equal("A-1", lines[0].Sku);
    }
}

// ============================================================================
// Part 4: String validation tests
// ============================================================================

public class StringValidatorTests
{
    [Theory]
    [InlineData("hello@example.com", true)]
    [InlineData("user.name@domain.org", true)]
    [InlineData("", false)]
    [InlineData("   ", false)]
    [InlineData("no-at-sign", false)]
    [InlineData("@no-local-part.com", false)]
    public void IsValidEmail_returns_expected_result(string email, bool expected)
    {
        bool result = StringValidator.IsValidEmail(email);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Capitalize_returns_first_letter_uppercase()
    {
        string result = StringValidator.Capitalize("hello");

        Assert.Equal("Hello", result);
    }

    [Fact]
    public void Capitalize_with_empty_string_returns_empty()
    {
        string result = StringValidator.Capitalize("");

        Assert.Equal("", result);
    }
}

// ============================================================================
// Part 5: Collection assertion tests
// ============================================================================

public class ProductFilterTests
{
    [Fact]
    public void GetExpensiveProducts_returns_only_products_above_threshold()
    {
        var products = new List<Product>
        {
            new("Laptop", 999m),
            new("Mouse", 29m),
            new("Monitor", 399m),
            new("Pen", 2m)
        };

        var expensive = ProductFilter.GetExpensiveProducts(products, 100m);

        Assert.Equal(2, expensive.Count);
        Assert.Contains(expensive, p => p.Name == "Laptop");
        Assert.Contains(expensive, p => p.Name == "Monitor");
        Assert.DoesNotContain(expensive, p => p.Name == "Mouse");
    }

    [Fact]
    public void GetExpensiveProducts_returns_empty_when_none_match()
    {
        var products = new List<Product>
        {
            new("Pen", 2m),
            new("Eraser", 1m)
        };

        var expensive = ProductFilter.GetExpensiveProducts(products, 100m);

        Assert.Empty(expensive);
    }
}

// ============================================================================
// Classes under test
// ============================================================================

public class Calculator
{
    public int Add(int a, int b) => a + b;
    public int Subtract(int a, int b) => a - b;

    public double Divide(int a, int b)
    {
        if (b == 0) throw new DivideByZeroException("Cannot divide by zero.");
        return (double)a / b;
    }
}

public static class GradeCalculator
{
    public static string GetGrade(int score)
    {
        if (score < 0 || score > 100)
            throw new ArgumentOutOfRangeException(nameof(score), "Score must be between 0 and 100.");

        return score switch
        {
            >= 90 => "A",
            >= 80 => "B",
            >= 70 => "C",
            >= 60 => "D",
            _ => "F"
        };
    }
}

public class Order
{
    private readonly List<OrderLine> _lines = new();
    public IReadOnlyList<OrderLine> Lines => _lines;

    public void AddLine(string sku, int qty, decimal price)
    {
        if (qty <= 0) throw new ArgumentException("Quantity must be positive.");
        _lines.Add(new OrderLine(sku, qty, price));
    }

    public decimal Total() => _lines.Sum(l => l.Qty * l.Price);
}

public record OrderLine(string Sku, int Qty, decimal Price);

public static class StringValidator
{
    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;
        int atIndex = email.IndexOf('@');
        return atIndex > 0 && atIndex < email.Length - 1;
    }

    public static string Capitalize(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;
        return char.ToUpper(input[0]) + input[1..];
    }
}

public record Product(string Name, decimal Price);

public static class ProductFilter
{
    public static List<Product> GetExpensiveProducts(List<Product> products, decimal threshold)
        => products.Where(p => p.Price > threshold).ToList();
}
