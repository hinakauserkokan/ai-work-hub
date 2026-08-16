# Projects and task-board frontend

## What we built

- A searchable project overview with status filters and progress indicators.
- A responsive Kanban board for personal work, with three task states: **To do**, **In progress**, and **In review**.
- Temporary interactive state: buttons can add a project or task, and task cards move to the next stage.

## What to notice in the code

`Projects.razor` and `MyTasks.razor` both keep temporary data in `@code`. This lets us learn the component and event model before database work begins.

- `@bind="searchTerm"` keeps the search input and C# field in sync.
- `@onclick` calls a C# method when a user presses a button.
- `VisibleProjects` and `TasksFor(...)` are computed collections: the markup stays simple while the component decides what to display.
- Changes remain only for the current browser session. Persistence replaces this local data with database-backed models and services.

## Next milestone

Persistence: define the project and task domain models, add Entity Framework Core and PostgreSQL, create the first migration, then replace the preview records with CRUD services.
