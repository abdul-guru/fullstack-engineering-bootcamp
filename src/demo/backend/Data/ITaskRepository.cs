using TaskFlow.Api.Models;

namespace TaskFlow.Api.Data;

public interface ITaskRepository
{
    Task<IReadOnlyList<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(string id);
    Task CreateAsync(TaskItem item);
    Task<bool> UpdateAsync(string id, TaskItem item);
    Task<bool> DeleteAsync(string id);
}
