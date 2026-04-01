# TaskFlow API

ASP.NET Core Web API backend for the TaskFlow application.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [MongoDB](https://www.mongodb.com/try/download/community) running on `localhost:27017`

## Getting Started

```bash
dotnet run
```

The API starts at **http://localhost:5026**.

## Configuration

MongoDB connection is configured in `appsettings.json`:

```json
{
  "MongoDb": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "taskflow"
  }
}
```

## API Endpoints

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/tasks` | List all tasks |
| GET | `/api/tasks/{id}` | Get task by ID |
| POST | `/api/tasks` | Create a new task |
| PUT | `/api/tasks/{id}` | Update a task |
| DELETE | `/api/tasks/{id}` | Delete a task |

## Project Structure

```
Controllers/       → HTTP entry points (thin, delegates to services)
Contracts/         → Request/response DTOs
Services/          → Business logic
Data/              → MongoDB repository and settings
Models/            → Database entity models
```
