using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Api.Contracts;

public sealed class UpdateTaskRequest
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 200 characters")]
    public string Title { get; set; } = "";

    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Status is required")]
    public string Status { get; set; } = "";
}
