# App shell and navigation

## What we built

The app shell is the UI that remains in place while the user changes pages:

```text
Sidebar (brand + links) | Top bar (search + actions)
                        | Page-specific content
```

The sidebar and top bar live in `Components/Layout/MainLayout.razor`. The placeholder `@Body` is where Blazor renders the current page.

## Files to read, in order

1. `Program.cs` starts the ASP.NET Core application and enables interactive server-side Blazor.
2. `Components/App.razor` is the HTML document used by the app. It loads global CSS and the Blazor JavaScript file.
3. `Components/Routes.razor` matches the browser URL to a page component and applies `MainLayout`.
4. `Components/Layout/MainLayout.razor` defines the shared shell.
5. `Components/Layout/MainLayout.razor.css` contains CSS scoped to that layout.
6. `Components/Pages/Home.razor` is the dashboard page.
7. `wwwroot/app.css` contains styles shared across all pages.

## Blazor terms introduced

| Term | Meaning in this project |
| --- | --- |
| Component | A `.razor` file containing UI markup and optional C# code. |
| Route | A URL assigned by `@page`, such as `@page "/projects"`. |
| Layout | A reusable outer component that wraps page content. |
| `@Body` | The place where the selected page is inserted into a layout. |
| `NavLink` | A navigation link that automatically gets an `active` CSS class when its route matches the current URL. |
| `@code` | A C# section inside a Razor component. It currently holds temporary dashboard data. |
| `@foreach` | C# loop syntax that repeats markup once for every item in a collection. |

## Dashboard data is temporary

`Home.razor` defines `TaskPreview` and `ProjectProgress` records inside its `@code` block. This is deliberately simple: we can learn how the page renders data before adding APIs and a database. In the persistence milestone, these records will move to domain models and be loaded by a service.

## How a navigation click works

1. A user clicks a `NavLink`, for example **Projects**.
2. Blazor updates the URL to `/projects` without a full page refresh.
3. `Routes.razor` finds `Pages/Projects.razor`, which declares `@page "/projects"`.
4. `MainLayout.razor` stays on screen and replaces only `@Body` with the Projects page.
5. `NavLink` marks Projects as active, allowing CSS to highlight it.
