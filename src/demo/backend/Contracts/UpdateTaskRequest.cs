namespace TaskFlow.Api.Contracts;

public sealed class UpdateTaskRequest
{
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public string Status { get; set; } = "";
}
