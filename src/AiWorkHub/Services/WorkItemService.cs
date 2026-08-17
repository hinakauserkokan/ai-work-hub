using AiWorkHub.Data;
using AiWorkHub.Models;
using Microsoft.EntityFrameworkCore;

namespace AiWorkHub.Services;

public sealed class WorkItemSummary
{
    public required int Id { get; init; }
    public required string Title { get; init; }
    public required string ProjectName { get; init; }
    public required WorkItemStatus Status { get; set; }
    public required WorkItemPriority Priority { get; init; }
    public DateOnly? DueDate { get; init; }
    public required string Due { get; set; }
    public required string DueClass { get; set; }
    public required MemberAvatar Member { get; init; }
}

/// <summary>Reads and writes Kanban-board work items. Talks to the database through <see cref="AiWorkHubDbContext"/>.</summary>
public class WorkItemService(AiWorkHubDbContext db)
{
    public async Task<List<WorkItemSummary>> GetWorkItemSummariesAsync()
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        var workItems = await db.WorkItems
            .Include(w => w.Project)
            .Include(w => w.Assignee)
            .OrderBy(w => w.CreatedAt)
            .ToListAsync();

        return workItems.Select(w => ToSummary(w, today)).ToList();
    }

    public async Task<WorkItemSummary> AddWorkItemAsync(WorkItemStatus status)
    {
        var workItem = new WorkItem
        {
            Title = "Untitled task",
            Status = status,
            Priority = WorkItemPriority.Medium,
        };

        db.WorkItems.Add(workItem);
        await db.SaveChangesAsync();
        return ToSummary(workItem, DateOnly.FromDateTime(DateTime.Now));
    }

    /// <summary>Advances a task to the next Kanban column and returns its new status.</summary>
    public async Task<WorkItemStatus> MoveToNextStatusAsync(int workItemId)
    {
        var workItem = await db.WorkItems.FindAsync(workItemId)
            ?? throw new InvalidOperationException($"Work item {workItemId} was not found.");

        workItem.Status = workItem.Status.Next();
        await db.SaveChangesAsync();
        return workItem.Status;
    }

    private static WorkItemSummary ToSummary(WorkItem workItem, DateOnly today) => new()
    {
        Id = workItem.Id,
        Title = workItem.Title,
        ProjectName = workItem.Project?.Name ?? "Choose a project",
        Status = workItem.Status,
        Priority = workItem.Priority,
        DueDate = workItem.DueDate,
        Due = workItem.DueDate.DueText(today),
        DueClass = workItem.DueDate.DueClass(today),
        Member = workItem.Assignee is { } assignee ? new MemberAvatar(assignee.Initials, assignee.Tone) : MemberAvatar.Unassigned,
    };
}
