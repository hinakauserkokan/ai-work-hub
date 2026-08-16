# AI Work Hub

AI Work Hub is a team workspace for projects, tasks, meeting decisions, and later, AI-assisted action planning.

## Current milestone

The app shell, dashboard prototype, and projects/task-board frontend are complete. These screens use temporary in-component data so we can learn the frontend before connecting a database.

## Run the app

From this folder:

```bash
DOTNET_CLI_HOME="$PWD/work/.dotnet-cli" dotnet run --project src/AiWorkHub
```

Open the local URL printed by .NET (normally `https://localhost:xxxx`).

## Documentation

- [Project roadmap](docs/00-project-roadmap.md)
- [App shell and navigation](docs/01-app-shell-and-navigation.md)
- [Projects and task board](docs/02-projects-and-task-board.md)
