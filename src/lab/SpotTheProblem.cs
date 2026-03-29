using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace TaskFlow.Lab.Snippets;

// =============================================================
// Day 1 Lab — Part C: Spot the Problem
// =============================================================
// Review this controller action and answer:
//   1. What responsibilities are mixed here?
//   2. What would become hard to maintain?
//   3. What should move to another layer?
//   4. What is missing or weak in this code?
// =============================================================

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly IMongoDatabase _mongoDatabase;

    public TasksController(IMongoDatabase mongoDatabase)
    {
        _mongoDatabase = mongoDatabase;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(CreateTaskRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            return BadRequest("Title required");

        var task = new TaskItem
        {
            Title = request.Title,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow,
            Status = "New"
        };

        var collection = _mongoDatabase.GetCollection<TaskItem>("tasks");
        await collection.InsertOneAsync(task);

        if (request.Title == "admin")
        {
            // special business rule
        }

        return Ok(task);
    }
}

// These classes are here just so the snippet compiles for reading.
// In a real project they would be in separate files.

public class CreateTaskRequest
{
    public string Title { get; set; } = "";
    public string? Description { get; set; }
}

public class TaskItem
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public string Status { get; set; } = "";
    public DateTime CreatedAt { get; set; }
}
