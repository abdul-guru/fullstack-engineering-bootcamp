namespace TaskFlow.Lab.Day2;

// =============================================================
// Day 2 Lab — Part C: DTO vs Entity
// =============================================================
// Compare these two classes and answer:
//   1. Which should the client send as input?
//   2. Which should stay internal to the application?
//   3. Why might exposing CreatedAtUtc or internal fields
//      directly be a problem later?
// =============================================================

// --- This is the ENTITY (persistence/data model) ---
// Used internally to represent a task in the database.
// Contains fields the client should NOT control.

public sealed class TaskItemEntity
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public string Status { get; set; } = "";
    public DateTime CreatedAtUtc { get; set; }

    // Internal fields that should never be exposed:
    // public string InternalAuditTrail { get; set; } = "";
    // public int RetryCount { get; set; }
}

// --- This is the DTO (Data Transfer Object) for input ---
// Shaped for the client's needs. Only the fields the
// client should provide when creating a task.

public sealed class CreateTaskDto
{
    public string Title { get; set; } = "";
    public string? Description { get; set; }

    // Note: No Id, no Status, no CreatedAtUtc.
    // The server controls those values.
}

// --- This is the DTO for output ---
// Shaped for what the client should receive back.
// May exclude sensitive internal fields.

public sealed class TaskResponseDto
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public string Status { get; set; } = "";
    public DateTime CreatedAtUtc { get; set; }

    // If we later add internal fields to the entity,
    // they won't accidentally leak to the client.
}

// =============================================================
// Key Takeaways:
// - DTOs protect the API contract
// - Entities represent storage concerns
// - Separating them gives you room to evolve independently
// - Exposing entities directly creates tight coupling between
//   your database schema and your API consumers
// =============================================================
