namespace AiWorkHub.Models;

public enum ProjectStatus
{
    Planning,
    InProgress,
    Complete,
}

/// <summary>Maps the enum to the display text and CSS class the Razor pages already use.</summary>
public static class ProjectStatusExtensions
{
    public static string DisplayText(this ProjectStatus status) => status switch
    {
        ProjectStatus.Planning => "Planning",
        ProjectStatus.InProgress => "In progress",
        ProjectStatus.Complete => "Complete",
        _ => status.ToString(),
    };

    public static string CssClass(this ProjectStatus status) => status switch
    {
        ProjectStatus.Planning => "planning",
        ProjectStatus.InProgress => "in-progress",
        ProjectStatus.Complete => "complete",
        _ => status.ToString().ToLowerInvariant(),
    };
}
