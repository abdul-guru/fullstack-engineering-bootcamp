using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace TaskFlow.Lab.Day2;

// =============================================================
// Day 2 Lab — Part D: Spot the Smell
// =============================================================
// Review this controller action and answer:
//   1. What is acceptable here?
//   2. What responsibilities are mixed?
//   3. What should move to a service?
//   4. Is Ok(task) the best response for a create?
//   5. What could become messy as the feature grows?
// =============================================================

[ApiController]
[Route("api/tasks")]
public class SmellTasksController : ControllerBase
{
    private readonly IMongoDatabase _db;

    public SmellTasksController(IMongoDatabase db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(CreateTaskInput request)
    {
        // Manual validation in controller
        if (string.IsNullOrWhiteSpace(request.Title))
            return BadRequest("Title missing");

        // Entity creation in controller
        var task = new TaskEntity
        {
            Title = request.Title,
            Description = request.Description,
            Status = "New",
            CreatedAt = DateTime.UtcNow
        };

        // Direct database access in controller
        var collection = _db.GetCollection<TaskEntity>("tasks");
        await collection.InsertOneAsync(task);

        // Business rule buried in controller
        if (request.Title.Contains("admin"))
        {
            task.Status = "Requires Approval";
            await collection.ReplaceOneAsync(x => x.Id == task.Id, task);
        }

        // Returning raw entity — leaks internal model
        // Using Ok instead of CreatedAtAction — wrong status code
        return Ok(task);
    }
}

// Used by the exercise above — simplified models
public class CreateTaskInput
{
    public string Title { get; set; } = "";
    public string? Description { get; set; }
}

public class TaskEntity
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public string Status { get; set; } = "";
    public DateTime CreatedAt { get; set; }
}
