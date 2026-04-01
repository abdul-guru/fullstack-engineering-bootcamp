using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskFlow.Api.Models;

public sealed class TaskItem
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = "";

    public string Title { get; set; } = "";

    public string? Description { get; set; }

    public string Status { get; set; } = "New";

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
