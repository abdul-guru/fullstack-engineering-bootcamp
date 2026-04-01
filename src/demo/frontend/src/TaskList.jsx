export default function TaskList({ tasks, onDelete }) {
  if (tasks.length === 0) {
    return <p className="empty">No tasks yet. Create one above.</p>;
  }

  return (
    <div className="task-list">
      <h2>Tasks</h2>
      <table>
        <thead>
          <tr>
            <th>Title</th>
            <th>Status</th>
            <th>Created</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {tasks.map((task) => (
            <tr key={task.id}>
              <td>
                <strong>{task.title}</strong>
                {task.description && (
                  <p className="description">{task.description}</p>
                )}
              </td>
              <td>
                <span className={`status status-${task.status.toLowerCase()}`}>
                  {task.status}
                </span>
              </td>
              <td>{new Date(task.createdAtUtc).toLocaleDateString()}</td>
              <td>
                <button
                  className="btn-delete"
                  onClick={() => onDelete(task.id)}
                >
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
