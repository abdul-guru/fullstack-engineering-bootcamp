using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace TaskFlow.Lab.Day2;

// =============================================================
// BAD EXAMPLE — Fat Controller (Anti-Pattern)
// =============================================================
// Problems to identify:
//   - Direct MongoDB access in controller
//   - Business logic mixed with transport logic
//   - Validation mixed in controller body
//   - Returns raw entity (leaks internal model)
//   - String literal status values scattered
//   - No service layer, no separation of concerns
//   - No DTOs — accepts and returns the persistence model
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
        // Direct collection access in controller
        var collection = _db.GetCollection<TaskEntity>("tasks");
        var tasks = await collection.Find(_ => true).ToListAsync();
        return Ok(tasks); // Returns raw entities
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var collection = _db.GetCollection<TaskEntity>("tasks");
        var task = await collection.Find(x => x.Id == id).FirstOrDefaultAsync();

        if (task == null)
            return Ok(null); // Wrong: should be NotFound()

        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> Create(TaskEntity task)
    {
        // Accepts the entity directly — no DTO
        if (string.IsNullOrWhiteSpace(task.Title))
            return BadRequest("Title required");

        task.Status = "New";
        task.CreatedAt = DateTime.UtcNow;

        var collection = _db.GetCollection<TaskEntity>("tasks");
        await collection.InsertOneAsync(task);

        // Business rule buried in controller
        if (task.Title.Contains("urgent"))
        {
            task.Status = "High Priority";
            await collection.ReplaceOneAsync(x => x.Id == task.Id, task);
        }

        return Ok(task); // Should be CreatedAtAction with 201
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, TaskEntity task)
    {
        var collection = _db.GetCollection<TaskEntity>("tasks");
        await collection.ReplaceOneAsync(x => x.Id == id, task);
        return Ok("updated"); // Wrong: should be NoContent (204)
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var collection = _db.GetCollection<TaskEntity>("tasks");
        await collection.DeleteOneAsync(x => x.Id == id);
        return Ok("deleted"); // Wrong: should be NoContent (204)
    }
}
