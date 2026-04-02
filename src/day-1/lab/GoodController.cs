using Microsoft.AspNetCore.Mvc;

namespace TaskFlow.Comparisons;

// =============================================================
// GOOD EXAMPLE — Thin Controller with Proper Separation
// =============================================================
// Improvements over the bad version:
//   - Controller only coordinates (receives, delegates, responds)
//   - Uses DTOs for input/output (not raw entities)
//   - Delegates to a service layer
//   - Uses appropriate status codes
//   - No direct database access in controller
//   - Clean, readable, maintainable
// =============================================================

[ApiController]
[Route("api/tasks")]
public class GoodTasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public GoodTasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TaskResponse>>> GetAll()
    {
        var tasks = await _taskService.GetAllAsync();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskResponse>> GetById(string id)
    {
        var task = await _taskService.GetByIdAsync(id);

        if (task is null)
            return NotFound();

        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<TaskResponse>> Create(CreateTaskRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            return BadRequest(new { message = "Title is required" });

        var created = await _taskService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, UpdateTaskRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            return BadRequest(new { message = "Title is required" });

        var updated = await _taskService.UpdateAsync(id, request);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await _taskService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}

// --- Supporting contracts and interfaces (for readability) ---

public class CreateTaskRequest
{
    public string Title { get; set; } = "";
    public string? Description { get; set; }
}

public class UpdateTaskRequest
{
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public string Status { get; set; } = "";
}

public class TaskResponse
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public string Status { get; set; } = "";
    public DateTime CreatedAtUtc { get; set; }
}

public interface ITaskService
{
    Task<IReadOnlyList<TaskResponse>> GetAllAsync();
    Task<TaskResponse?> GetByIdAsync(string id);
    Task<TaskResponse> CreateAsync(CreateTaskRequest request);
    Task<bool> UpdateAsync(string id, UpdateTaskRequest request);
    Task<bool> DeleteAsync(string id);
}
