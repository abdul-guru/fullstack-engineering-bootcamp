// ============================================================================
// Workshop 10: Task Tracker Console App
// ============================================================================
// This is a combined project that exercises:
//   - OOP: TaskItem class with encapsulated behavior
//   - Collections: List<T> for storage
//   - LINQ: Filtering, grouping, projections
//   - Clean code: Named methods, single responsibility
//   - Async: Simulated async repository
//   - Console I/O: Interactive menu
//
// Matches Slide 20: "Task Tracker API" project idea
// ============================================================================

using TaskTracker;

var repository = new InMemoryTaskRepository();
await SeedSampleDataAsync(repository);

bool running = true;

Console.WriteLine("========================================");
Console.WriteLine("  Workshop 10 \u2013 Task Tracker Console");
Console.WriteLine("========================================");

while (running)
{
    Console.WriteLine();
    Console.WriteLine("--- Menu ---");
    Console.WriteLine("  1. List all tasks");
    Console.WriteLine("  2. Add a task");
    Console.WriteLine("  3. Update task status");
    Console.WriteLine("  4. Delete a task");
    Console.WriteLine("  5. Filter by status");
    Console.WriteLine("  6. View summary");
    Console.WriteLine("  7. Search tasks");
    Console.WriteLine("  0. Exit");
    Console.Write("  Choice: ");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            await ListAllTasksAsync(repository);
            break;
        case "2":
            await AddTaskAsync(repository);
            break;
        case "3":
            await UpdateTaskStatusAsync(repository);
            break;
        case "4":
            await DeleteTaskAsync(repository);
            break;
        case "5":
            await FilterByStatusAsync(repository);
            break;
        case "6":
            await ViewSummaryAsync(repository);
            break;
        case "7":
            await SearchTasksAsync(repository);
            break;
        case "0":
            running = false;
            Console.WriteLine("Goodbye!");
            break;
        default:
            Console.WriteLine("Invalid choice. Try again.");
            break;
    }
}

// ============================================================================
// Menu action methods
// ============================================================================

async Task ListAllTasksAsync(InMemoryTaskRepository repo)
{
    var tasks = await repo.GetAllAsync();
    if (tasks.Count == 0)
    {
        Console.WriteLine("  No tasks found.");
        return;
    }

    Console.WriteLine();
    Console.WriteLine($"  {"ID",-8} {"Title",-30} {"Status",-15} {"Created",-12}");
    Console.WriteLine($"  {new string('-', 65)}");
    foreach (var t in tasks)
    {
        Console.WriteLine($"  {t.Id,-8} {t.Title,-30} {t.Status,-15} {t.CreatedAt:yyyy-MM-dd}");
    }
}

async Task AddTaskAsync(InMemoryTaskRepository repo)
{
    Console.Write("  Task title: ");
    string? title = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(title))
    {
        Console.WriteLine("  Title cannot be empty.");
        return;
    }

    var task = new TaskItem(title);
    await repo.AddAsync(task);
    Console.WriteLine($"  Added: [{task.Id}] {task.Title}");
}

async Task UpdateTaskStatusAsync(InMemoryTaskRepository repo)
{
    Console.Write("  Task ID: ");
    string? id = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(id)) return;

    var task = await repo.GetByIdAsync(id);
    if (task is null)
    {
        Console.WriteLine("  Task not found.");
        return;
    }

    Console.WriteLine($"  Current status: {task.Status}");
    Console.WriteLine("  Available: New, InProgress, Completed, Cancelled");
    Console.Write("  New status: ");
    string? newStatus = Console.ReadLine();

    if (TaskItem.ValidStatuses.Contains(newStatus ?? ""))
    {
        task.UpdateStatus(newStatus!);
        Console.WriteLine($"  Updated [{task.Id}] to '{task.Status}'");
    }
    else
    {
        Console.WriteLine("  Invalid status.");
    }
}

async Task DeleteTaskAsync(InMemoryTaskRepository repo)
{
    Console.Write("  Task ID to delete: ");
    string? id = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(id)) return;

    bool deleted = await repo.DeleteAsync(id);
    Console.WriteLine(deleted ? "  Task deleted." : "  Task not found.");
}

async Task FilterByStatusAsync(InMemoryTaskRepository repo)
{
    Console.Write("  Filter by status (New/InProgress/Completed/Cancelled): ");
    string? status = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(status)) return;

    var tasks = await repo.GetAllAsync();
    var filtered = tasks.Where(t => t.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();

    Console.WriteLine($"  Found {filtered.Count} task(s) with status '{status}':");
    foreach (var t in filtered)
    {
        Console.WriteLine($"    [{t.Id}] {t.Title}");
    }
}

async Task ViewSummaryAsync(InMemoryTaskRepository repo)
{
    var tasks = await repo.GetAllAsync();

    if (tasks.Count == 0)
    {
        Console.WriteLine("  No tasks to summarize.");
        return;
    }

    Console.WriteLine();
    Console.WriteLine("  --- Task Summary ---");
    Console.WriteLine($"  Total tasks: {tasks.Count}");

    var byStatus = tasks
        .GroupBy(t => t.Status)
        .OrderBy(g => g.Key)
        .ToList();

    foreach (var group in byStatus)
    {
        Console.WriteLine($"    {group.Key}: {group.Count()}");
    }

    var oldest = tasks.OrderBy(t => t.CreatedAt).First();
    var newest = tasks.OrderByDescending(t => t.CreatedAt).First();
    Console.WriteLine($"  Oldest: [{oldest.Id}] {oldest.Title} ({oldest.CreatedAt:yyyy-MM-dd})");
    Console.WriteLine($"  Newest: [{newest.Id}] {newest.Title} ({newest.CreatedAt:yyyy-MM-dd})");
}

async Task SearchTasksAsync(InMemoryTaskRepository repo)
{
    Console.Write("  Search term: ");
    string? term = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(term)) return;

    var tasks = await repo.GetAllAsync();
    var results = tasks
        .Where(t => t.Title.Contains(term, StringComparison.OrdinalIgnoreCase))
        .ToList();

    Console.WriteLine($"  Found {results.Count} task(s) matching '{term}':");
    foreach (var t in results)
    {
        Console.WriteLine($"    [{t.Id}] {t.Title} ({t.Status})");
    }
}

async Task SeedSampleDataAsync(InMemoryTaskRepository repo)
{
    await repo.AddAsync(new TaskItem("Design database schema") { Status = "Completed" });
    await repo.AddAsync(new TaskItem("Implement REST API endpoints") { Status = "InProgress" });
    await repo.AddAsync(new TaskItem("Write unit tests") { Status = "InProgress" });
    await repo.AddAsync(new TaskItem("Set up CI/CD pipeline") { Status = "New" });
    await repo.AddAsync(new TaskItem("Create React frontend") { Status = "New" });
    await repo.AddAsync(new TaskItem("Deploy to staging environment") { Status = "New" });
}

// ============================================================================
// Domain model and repository
// ============================================================================

namespace TaskTracker
{
    public class TaskItem
    {
        public static readonly string[] ValidStatuses = ["New", "InProgress", "Completed", "Cancelled"];

        public string Id { get; } = Guid.NewGuid().ToString()[..8];
        public string Title { get; }
        public string Status { get; set; } = "New";
        public DateTime CreatedAt { get; } = DateTime.Now;

        public TaskItem(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.", nameof(title));
            Title = title;
        }

        public void UpdateStatus(string newStatus)
        {
            if (!ValidStatuses.Contains(newStatus))
                throw new ArgumentException($"Invalid status: {newStatus}");
            Status = newStatus;
        }

        public override string ToString() => $"[{Status}] {Title} (id: {Id})";
    }

    public class InMemoryTaskRepository
    {
        private readonly List<TaskItem> _tasks = new();

        public async Task<List<TaskItem>> GetAllAsync()
        {
            await Task.Delay(10); // simulate async I/O
            return _tasks.ToList();
        }

        public async Task<TaskItem?> GetByIdAsync(string id)
        {
            await Task.Delay(10);
            return _tasks.FirstOrDefault(t => t.Id == id);
        }

        public async Task AddAsync(TaskItem task)
        {
            await Task.Delay(10);
            _tasks.Add(task);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            await Task.Delay(10);
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task is null) return false;
            _tasks.Remove(task);
            return true;
        }
    }
}
