namespace AiWorkHub.Models;

public enum WorkItemPriority
{
    Low,
    Medium,
    High,
}

public static class WorkItemPriorityExtensions
{
    /// <summary>Lowercase CSS class, e.g. "high", matching the Razor pages.</summary>
    public static string CssClass(this WorkItemPriority priority) => priority.ToString().ToLowerInvariant();

    public static string DisplayText(this WorkItemPriority priority) => priority.ToString();
}
