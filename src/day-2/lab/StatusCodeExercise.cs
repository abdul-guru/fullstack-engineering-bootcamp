namespace TaskFlow.Lab.Day2;

// =============================================================
// Day 2 Lab — Part E: Status Code Thinking
// =============================================================
// For each scenario below, choose the most appropriate
// HTTP status code. Write your answer in the comment.
//
// Available codes:
//   200 OK
//   201 Created
//   204 No Content
//   400 Bad Request
//   404 Not Found
//   409 Conflict
//   500 Internal Server Error
// =============================================================

public static class StatusCodeExercise
{
    // Scenario 1: Client sends a POST request with an empty title
    // Expected status code: ___
    public static int Scenario1_InvalidInput() => 0; // Replace with correct code

    // Scenario 2: Client sends GET /api/tasks/999 but task 999 doesn't exist
    // Expected status code: ___
    public static int Scenario2_ItemNotFound() => 0; // Replace with correct code

    // Scenario 3: Client sends POST /api/tasks with valid data, task is created
    // Expected status code: ___
    public static int Scenario3_ItemCreated() => 0; // Replace with correct code

    // Scenario 4: Client sends PUT /api/tasks/1 with valid data, task is updated
    // Expected status code: ___
    public static int Scenario4_SuccessfulUpdateNoBody() => 0; // Replace with correct code

    // Scenario 5: An unhandled exception occurs in the service layer
    // Expected status code: ___
    public static int Scenario5_UnexpectedFailure() => 0; // Replace with correct code

    // Scenario 6: Client tries to create a task with a title that already exists
    //             and business rules say titles must be unique
    // Expected status code: ___
    public static int Scenario6_DuplicateConflict() => 0; // Replace with correct code

    // Scenario 7: Client sends GET /api/tasks and there are tasks to return
    // Expected status code: ___
    public static int Scenario7_SuccessfulRead() => 0; // Replace with correct code
}

// =============================================================
// ANSWER KEY (for trainer reference — remove before distributing)
// =============================================================
// Scenario 1: 400 (Bad Request)
// Scenario 2: 404 (Not Found)
// Scenario 3: 201 (Created)
// Scenario 4: 204 (No Content)
// Scenario 5: 500 (Internal Server Error)
// Scenario 6: 409 (Conflict)
// Scenario 7: 200 (OK)
// =============================================================
