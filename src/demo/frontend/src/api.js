const API_BASE = "http://localhost:5026/api/tasks";

export async function getTasks() {
  const response = await fetch(API_BASE);
  if (!response.ok) throw new Error("Failed to fetch tasks");
  return response.json();
}

export async function getTaskById(id) {
  const response = await fetch(`${API_BASE}/${id}`);
  if (!response.ok) throw new Error("Failed to fetch task");
  return response.json();
}

export async function createTask(task) {
  const response = await fetch(API_BASE, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(task),
  });
  if (!response.ok) {
    const error = await response.json();
    throw new Error(error.message || "Failed to create task");
  }
  return response.json();
}

export async function updateTask(id, task) {
  const response = await fetch(`${API_BASE}/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(task),
  });
  if (!response.ok) {
    const error = await response.json().catch(() => ({}));
    throw new Error(error.message || "Failed to update task");
  }
}

export async function deleteTask(id) {
  const response = await fetch(`${API_BASE}/${id}`, {
    method: "DELETE",
  });
  if (!response.ok) throw new Error("Failed to delete task");
}
