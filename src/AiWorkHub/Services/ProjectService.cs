using AiWorkHub.Data;
using AiWorkHub.Models;
using Microsoft.EntityFrameworkCore;

namespace AiWorkHub.Services;

public sealed record ProjectSummary(
    int Id,
    string Name,
    string Description,
    string Icon,
    string Accent,
    string Status,
    string StatusClass,
    string DueText,
    int Completed,
    int Total,
    int Percent,
    MemberAvatar[] Members,
    int MemberCount);

/// <summary>Reads and writes projects for the Razor pages. Talks to the database through <see cref="AiWorkHubDbContext"/>.</summary>
public class ProjectService(AiWorkHubDbContext db)
{
    public async Task<List<ProjectSummary>> GetProjectSummariesAsync()
    {
        var projects = await db.Projects
            .Include(p => p.Members)
            .Include(p => p.WorkItems)
            .OrderBy(p => p.CreatedAt)
            .ToListAsync();

        return projects.Select(ToSummary).ToList();
    }

    public async Task<ProjectSummary> AddProjectAsync()
    {
        var project = new Project
        {
            Name = "Untitled project",
            Description = "Add a purpose and tasks to get this project underway.",
            Icon = "＋",
            Accent = "purple",
            Status = ProjectStatus.Planning,
        };

        db.Projects.Add(project);
        await db.SaveChangesAsync();
        return ToSummary(project);
    }

    private static ProjectSummary ToSummary(Project project) => new(
        project.Id,
        project.Name,
        project.Description,
        project.Icon,
        project.Accent,
        project.Status.DisplayText(),
        project.Status.CssClass(),
        project.DueSummary(),
        project.TaskCompleted(),
        project.TaskTotal(),
        project.CompletionPercent(),
        project.Members.Select(m => new MemberAvatar(m.Initials, m.Tone)).ToArray(),
        project.Members.Count);
}
