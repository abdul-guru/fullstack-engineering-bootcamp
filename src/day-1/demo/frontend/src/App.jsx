import { useEffect, useState } from "react";
import { getTasks, createTask, deleteTask } from "./api";
import TaskForm from "./TaskForm";
import TaskList from "./TaskList";
import "./App.css";

function App() {
  const [tasks, setTasks] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  const loadTasks = async () => {
    try {
      setError("");
      const data = await getTasks();
      setTasks(data);
    } catch (err) {
      setError("Failed to load tasks. Is the API running?");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadTasks();
  }, []);

  const handleCreate = async (task) => {
    await createTask(task);
    await loadTasks();
  };

  const handleDelete = async (id) => {
    try {
      await deleteTask(id);
      await loadTasks();
    } catch {
      setError("Failed to delete task");
    }
  };

  return (
    <div className="app">
      <h1>TaskFlow</h1>
      {error && <p className="error">{error}</p>}
      <TaskForm onTaskCreated={handleCreate} />
      {loading ? <p>Loading...</p> : <TaskList tasks={tasks} onDelete={handleDelete} />}
    </div>
  );
}

export default App;
