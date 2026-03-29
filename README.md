# Full-Stack Engineering Bootcamp

## .NET Backend + React Frontend + Professional Delivery Habits

A hands-on engineering bootcamp designed to improve how developers think, structure code, and deliver full-stack software professionally.

## Overview

This bootcamp uses a single project — **TaskFlow** (a small task tracking system) — to teach end-to-end full-stack development with ASP.NET Core, MongoDB, and React. The focus is not just on making things work, but on building them with clarity, discipline, and maintainability.

## What You Will Learn

- How a real full-stack request lifecycle works — from button click to database and back
- Structured backend development with ASP.NET Core Controllers and Minimal APIs
- Clean separation of concerns: Controllers, DTOs, Services, Repositories
- MongoDB integration for CRUD and query operations
- React frontend connected to a .NET API
- Validation, error handling, and intentional HTTP response design
- Configuration, environment awareness, and deployment basics
- Code review thinking and engineering habits

## Technology Stack

- **.NET 10** / ASP.NET Core
- **Controllers** and **Minimal APIs**
- **MongoDB**
- **React**

## Roadmap

1. **Engineering Mindset & Flow** — Understanding the full-stack request lifecycle, system layers, coding vs engineering, and project walkthrough
2. **Structured Backend with Controllers** — Building disciplined APIs using Controllers, DTOs, service layers, dependency injection, validation, and proper HTTP semantics
3. **Minimal APIs & MongoDB** — Lean endpoint design with Minimal APIs, MongoDB CRUD, document-based thinking, and comparing API styles
4. **React & API Integration** — Connecting the React frontend to the backend, consuming APIs, managing state, and understanding the full-stack contract
5. **Deployment, Review & Capstone** — Configuration management, deployment basics, code review discipline, Definition of Done, and end-to-end ownership

## Repository Structure

Each stage of the bootcamp lives on its own Git branch. Each branch contains folders for demos, labs, and code comparisons relevant to that stage.

| Branch | Focus |
|---|---|
| `day0-starter` | Starter solution and project scaffolding |
| `day1-foundation` | Engineering mindset, flow, and setup |
| `day2-controllers` | Controllers, DTOs, services, validation |
| `day3-minimalapi-mongodb` | Minimal APIs and MongoDB integration |
| `day4-react-integration` | React frontend and API integration |
| `day5-deployment-capstone` | Configuration, deployment, review, capstone |

Each branch includes:

```
src/
├── demo/          # Trainer demo code
├── lab/           # Participant lab exercises
└── comparisons/   # Bad vs good code examples
```

## The Project: TaskFlow

A small work/task tracking system covering:

- Create, list, get, update, and delete tasks
- Change task status
- Search by title/status
- React UI for viewing and managing tasks
- Validation and error handling throughout