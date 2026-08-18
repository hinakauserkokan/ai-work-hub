namespace AiWorkHub.Models;

public static class ProjectExtensions
{
    /// <summary>"Due 29 Aug" while active, "Completed 14 Aug" once finished, or "No due date".</summary>
    public static string DueSummary(this Project project)
    {
        if (project.Status == ProjectStatus.Complete)
        {
            return project.DueDate is { } completedOn ? $"Completed {completedOn:d MMM}" : "Completed";
        }

        return project.DueDate is { } due ? $"Due {due:d MMM}" : "No due date";
    }

    public static int TaskTotal(this Project project) => project.WorkItems.Count;

    public static int TaskCompleted(this Project project) => project.WorkItems.Count(w => w.Status == WorkItemStatus.InReview);

    public static int CompletionPercent(this Project project)
    {
        var total = project.TaskTotal();
        return total == 0 ? 0 : project.TaskCompleted() * 100 / total;
    }
}
