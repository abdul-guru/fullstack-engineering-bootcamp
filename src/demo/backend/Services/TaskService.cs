using TaskFlow.Api.Contracts;
using TaskFlow.Api.Data;
using TaskFlow.Api.Models;

namespace TaskFlow.Api.Services;

public sealed class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<TaskResponse>> GetAllAsync()
    {
        var items = await _repository.GetAllAsync();
        return items.Select(MapToResponse).ToList();
    }

    public async Task<TaskResponse?> GetByIdAsync(string id)
    {
        var item = await _repository.GetByIdAsync(id);
        return item is null ? null : MapToResponse(item);
    }

    public async Task<TaskResponse> CreateAsync(CreateTaskRequest request)
    {
        var item = new TaskItem
        {
            Title = request.Title.Trim(),
            Description = request.Description,
            Status = "New",
            CreatedAtUtc = DateTime.UtcNow
        };

        await _repository.CreateAsync(item);
        return MapToResponse(item);
    }

    public async Task<bool> UpdateAsync(string id, UpdateTaskRequest request)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing is null) return false;

        existing.Title = request.Title.Trim();
        existing.Description = request.Description;
        existing.Status = request.Status;

        return await _repository.UpdateAsync(id, existing);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static TaskResponse MapToResponse(TaskItem item)
    {
        return new TaskResponse
        {
            Id = item.Id,
            Title = item.Title,
            Description = item.Description,
            Status = item.Status,
            CreatedAtUtc = item.CreatedAtUtc
        };
    }
}
