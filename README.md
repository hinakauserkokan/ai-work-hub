# AI Work Hub

AI Work Hub is a team workspace for projects, tasks, meeting decisions, and later, AI-assisted action planning.

## Current milestone

The app shell, dashboard, and projects/task-board pages are backed by a real PostgreSQL
database through EF Core: projects, tasks, and team members are read and written by
`ProjectService` and `WorkItemService`, and changes now survive a browser refresh.

## Run the app

### 1. Database (one-time setup)

Requires a local PostgreSQL server. Create the database, then apply migrations:

```bash
createdb aiworkhub_dev
DOTNET_CLI_HOME="$PWD/work/.dotnet-cli" dotnet tool restore
DOTNET_CLI_HOME="$PWD/work/.dotnet-cli" dotnet ef database update --project src/AiWorkHub
```

The connection string lives in `src/AiWorkHub/appsettings.Development.json` and defaults to
`Host=localhost;Database=aiworkhub_dev;Username=<your OS user>` with no password, which works
out of the box on a local trust-authenticated Postgres install (e.g. `brew install postgresql`).

### 2. Run

From this folder:

```bash
DOTNET_CLI_HOME="$PWD/work/.dotnet-cli" dotnet run --project src/AiWorkHub
```

Open the local URL printed by .NET (normally `https://localhost:xxxx`). On startup the app also
applies any pending migrations and seeds sample data automatically, so the two setup steps above
only need to run once.

## Documentation

- [Project roadmap](docs/00-project-roadmap.md)
- [App shell and navigation](docs/01-app-shell-and-navigation.md)
- [Projects and task board](docs/02-projects-and-task-board.md)
- [Persistence](docs/03-persistence.md)
