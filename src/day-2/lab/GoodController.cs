using Microsoft.AspNetCore.Mvc;

namespace TaskFlow.Lab.Day2;

// =============================================================
// GOOD EXAMPLE — Thin Controller with Proper Separation
// =============================================================
// Improvements over the bad version:
//   - Controller only coordinates (receives, delegates, responds)
//   - Uses DTOs for input/output (not raw entities)
//   - Delegates to a service layer
//   - Uses appropriate HTTP status codes
//   - No direct database access in controller
//   - Clean, readable, maintainable
// =============================================================

[ApiController]
[Route("api/tasks")]
public class GoodTasksController : ControllerBase
{
    private readonly IGoodTaskService _taskService;

    public GoodTasksController(IGoodTaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<GoodTaskResponse>>> GetAll()
    {
        var tasks = await _taskService.GetAllAsync();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GoodTaskResponse>> GetById(string id)
    {
        var task = await _taskService.GetByIdAsync(id);

        if (task is null)
            return NotFound();

        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<GoodTaskResponse>> Create(GoodCreateTaskRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            return BadRequest(new { message = "Title is required" });

        var created = await _taskService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, GoodUpdateTaskRequest request)
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

// =============================================================
// Supporting types for the Good Example
// =============================================================

public interface IGoodTaskService
{
    Task<IReadOnlyList<GoodTaskResponse>> GetAllAsync();
    Task<GoodTaskResponse?> GetByIdAsync(string id);
    Task<GoodTaskResponse> CreateAsync(GoodCreateTaskRequest request);
    Task<bool> UpdateAsync(string id, GoodUpdateTaskRequest request);
    Task<bool> DeleteAsync(string id);
}

public sealed class GoodCreateTaskRequest
{
    public string Title { get; set; } = "";
    public string? Description { get; set; }
}

public sealed class GoodUpdateTaskRequest
{
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public string Status { get; set; } = "";
}

public sealed class GoodTaskResponse
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public string Status { get; set; } = "";
    public DateTime CreatedAtUtc { get; set; }
}
