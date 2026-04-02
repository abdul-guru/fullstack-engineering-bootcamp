using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace TaskFlow.Comparisons;

// =============================================================
// BAD EXAMPLE — Fat Controller
// =============================================================
// Problems:
//   - Direct MongoDB access in controller
//   - Business logic mixed with transport logic
//   - Validation mixed in controller
//   - Returns raw entity (leaks internal model)
//   - String literal status values
//   - No service layer, no separation of concerns
// =============================================================

[ApiController]
[Route("api/tasks")]
public class BadTasksController : ControllerBase
{
    private readonly IMongoDatabase _db;

    public BadTasksController(IMongoDatabase db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var collection = _db.GetCollection<TaskItem>("tasks");
        var tasks = await collection.Find(_ => true).ToListAsync();
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> Create(TaskItem task)
    {
        if (string.IsNullOrWhiteSpace(task.Title))
            return BadRequest("Title required");

        task.Status = "New";
        task.CreatedAt = DateTime.UtcNow;

        var collection = _db.GetCollection<TaskItem>("tasks");
        await collection.InsertOneAsync(task);

        if (task.Title.Contains("urgent"))
        {
            // business rule buried in controller
            task.Status = "High Priority";
            await collection.ReplaceOneAsync(x => x.Id == task.Id, task);
        }

        return Ok(task);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var collection = _db.GetCollection<TaskItem>("tasks");
        await collection.DeleteOneAsync(x => x.Id == id);
        return Ok("deleted");
    }
}

public class TaskItem
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public string Status { get; set; } = "";
    public DateTime CreatedAt { get; set; }
}
