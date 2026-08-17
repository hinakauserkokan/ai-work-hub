namespace AiWorkHub.Models;

public enum WorkItemStatus
{
    ToDo,
    InProgress,
    InReview,
}

/// <summary>Maps the enum to the Kanban column name and tone the Razor pages already use.</summary>
public static class WorkItemStatusExtensions
{
    public static string ColumnName(this WorkItemStatus status) => status switch
    {
        WorkItemStatus.ToDo => "To do",
        WorkItemStatus.InProgress => "In progress",
        WorkItemStatus.InReview => "In review",
        _ => status.ToString(),
    };

    public static string ColumnTone(this WorkItemStatus status) => status switch
    {
        WorkItemStatus.ToDo => "purple",
        WorkItemStatus.InProgress => "blue",
        WorkItemStatus.InReview => "amber",
        _ => "purple",
    };

    /// <summary>The status a task moves to when a user presses "Move to next stage". Wraps back to ToDo after InReview.</summary>
    public static WorkItemStatus Next(this WorkItemStatus status) => status switch
    {
        WorkItemStatus.ToDo => WorkItemStatus.InProgress,
        WorkItemStatus.InProgress => WorkItemStatus.InReview,
        WorkItemStatus.InReview => WorkItemStatus.ToDo,
        _ => WorkItemStatus.ToDo,
    };
}
