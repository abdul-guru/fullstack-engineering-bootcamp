using TaskFlow.Api.Contracts;

namespace TaskFlow.Api.Services;

public interface ITaskService
{
    Task<IReadOnlyList<TaskResponse>> GetAllAsync();
    Task<TaskResponse?> GetByIdAsync(string id);
    Task<TaskResponse> CreateAsync(CreateTaskRequest request);
    Task<bool> UpdateAsync(string id, UpdateTaskRequest request);
    Task<bool> DeleteAsync(string id);
}
