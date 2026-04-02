namespace TaskFlow.Api.Contracts;

public sealed class CreateTaskRequest
{
    public string Title { get; set; } = "";
    public string? Description { get; set; }
}
