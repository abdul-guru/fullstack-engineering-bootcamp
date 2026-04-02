using Microsoft.AspNetCore.Mvc;

namespace TaskFlow.Lab.Day2;

// =============================================================
// Day 2 Lab — Part B: Build or Improve an Endpoint
// =============================================================
// Exercise: Complete the TODO sections below.
//
// This is a starter controller with some endpoints missing
// or incomplete. Your tasks:
//
//   Task 1: Complete the GetById endpoint
//   Task 2: Complete the Update endpoint
//   Task 3: Complete the Delete endpoint
//   Task 4: Improve the Create endpoint response
//
// Guidelines:
//   - Use correct HTTP routes
//   - Call the service (don't access DB directly)
//   - Use appropriate status codes
//   - Keep the controller thin
// =============================================================

[ApiController]
[Route("api/tasks")]
public class WorkshopTasksController : ControllerBase
{
    private readonly IWorkshopTaskService _taskService;

    public WorkshopTasksController(IWorkshopTaskService taskService)
    {
        _taskService = taskService;
    }

    // ✅ This one is done — use as a reference
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<WorkshopTaskResponse>>> GetAll()
    {
        var tasks = await _taskService.GetAllAsync();
        return Ok(tasks);
    }

    // TODO Task 1: Complete GetById
    // - Add the correct route attribute with id parameter
    // - Call the service to get the task
    // - Return NotFound() if the task doesn't exist
    // - Return Ok() with the task if found
    [HttpGet("{id}")]
    public async Task<ActionResult<WorkshopTaskResponse>> GetById(string id)
    {
        // Your code here:
        throw new NotImplementedException("Complete this endpoint");
    }

    // TODO Task 4: Improve the Create response
    // - Currently returns Ok(), but what's the better status code for creation?
    // - Hint: Look at CreatedAtAction()
    [HttpPost]
    public async Task<ActionResult<WorkshopTaskResponse>> Create(WorkshopCreateRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            return BadRequest(new { message = "Title is required" });

        var created = await _taskService.CreateAsync(request);

        // TODO: Replace Ok() with a better response
        return Ok(created);
    }

    // TODO Task 2: Complete the Update endpoint
    // - Add the correct HTTP verb and route
    // - Validate the title
    // - Call the service
    // - Return NotFound() if the task doesn't exist
    // - Return the appropriate success status code
    // [Http???("{id}")]
    public async Task<IActionResult> Update(string id, WorkshopUpdateRequest request)
    {
        // Your code here:
        throw new NotImplementedException("Complete this endpoint");
    }

    // TODO Task 3: Complete the Delete endpoint
    // - Add the correct HTTP verb and route
    // - Call the service
    // - Return NotFound() if the task doesn't exist
    // - Return the appropriate success status code
    // [Http???("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        // Your code here:
        throw new NotImplementedException("Complete this endpoint");
    }
}

// =============================================================
// Supporting types for the workshop
// =============================================================

public interface IWorkshopTaskService
{
    Task<IReadOnlyList<WorkshopTaskResponse>> GetAllAsync();
    Task<WorkshopTaskResponse?> GetByIdAsync(string id);
    Task<WorkshopTaskResponse> CreateAsync(WorkshopCreateRequest request);
    Task<bool> UpdateAsync(string id, WorkshopUpdateRequest request);
    Task<bool> DeleteAsync(string id);
}

public sealed class WorkshopCreateRequest
{
    public string Title { get; set; } = "";
    public string? Description { get; set; }
}

public sealed class WorkshopUpdateRequest
{
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public string Status { get; set; } = "";
}

public sealed class WorkshopTaskResponse
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public string Status { get; set; } = "";
    public DateTime CreatedAtUtc { get; set; }
}
