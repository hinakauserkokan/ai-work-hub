namespace AiWorkHub.Models;

/// <summary>
/// A unit of work on the Kanban board. Named "WorkItem" (not "Task") to avoid
/// colliding with System.Threading.Tasks.Task.
/// </summary>
public class WorkItem
{
    public int Id { get; set; }
    public required string Title { get; set; }

    public int? ProjectId { get; set; }
    public Project? Project { get; set; }

    public WorkItemStatus Status { get; set; } = WorkItemStatus.ToDo;
    public WorkItemPriority Priority { get; set; } = WorkItemPriority.Medium;
    public DateOnly? DueDate { get; set; }

    public int? AssigneeId { get; set; }
    public TeamMember? Assignee { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
