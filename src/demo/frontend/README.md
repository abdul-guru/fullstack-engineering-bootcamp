# TaskFlow UI

React frontend for the TaskFlow task management app. Built with React 19 and Vite.

## Prerequisites

- [Node.js](https://nodejs.org/) (v18+)
- The [backend API](../backend/README.md) running on `http://localhost:5026`

## Getting Started

```bash
npm install
npm run dev
```

The app will start at `http://localhost:5173` by default.

## Available Scripts

| Script          | Description                |
| --------------- | -------------------------- |
| `npm run dev`   | Start the dev server (HMR) |
| `npm run build` | Production build           |
| `npm run lint`  | Run ESLint                 |
| `npm run preview` | Preview production build |

## Project Structure

```
src/
├── api.js         # API client – calls the .NET backend REST API
├── App.jsx        # Root component – loads tasks, handles create & delete
├── App.css        # App styles
├── TaskForm.jsx   # Form for creating a new task (title + description)
├── TaskList.jsx   # Table that displays tasks with status and delete action
├── main.jsx       # Entry point
└── index.css      # Global styles
```

## Tech Stack

- **React 19** – UI library
- **Vite 8** – Dev server & bundler
- **ESLint** – Linting
