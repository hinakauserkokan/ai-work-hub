using AiWorkHub.Models;
using Microsoft.EntityFrameworkCore;

namespace AiWorkHub.Data;

/// <summary>
/// Fills a freshly migrated database with the same sample workspace the pages used to
/// hardcode, so the UI looks the same right after cutover. Safe to call every startup:
/// it does nothing once the Projects table has any rows.
/// </summary>
public static class DbSeeder
{
    public static async Task SeedAsync(AiWorkHubDbContext db)
    {
        if (await db.Projects.AnyAsync())
        {
            return;
        }

        var today = DateOnly.FromDateTime(DateTime.Now);

        var jordan = new TeamMember { Name = "Jordan Smith", Initials = "JS", Tone = "purple" };
        var mina = new TeamMember { Name = "Mina Bose", Initials = "MB", Tone = "green" };
        var kai = new TeamMember { Name = "Kai Lee", Initials = "KL", Tone = "orange" };
        var hina = new TeamMember { Name = "Hina", Initials = "H", Tone = "" };
        db.TeamMembers.AddRange(jordan, mina, kai, hina);

        var mobileApp = new Project
        {
            Name = "Mobile app",
            Description = "A clearer, faster experience for customers on the go.",
            Icon = "📱",
            Accent = "purple",
            Status = ProjectStatus.InProgress,
            DueDate = today.AddDays(12),
            Members = [jordan, mina, kai],
        };
        var websiteRedesign = new Project
        {
            Name = "Website redesign",
            Description = "Refresh the marketing site and improve conversion paths.",
            Icon = "◈",
            Accent = "blue",
            Status = ProjectStatus.InProgress,
            DueDate = today.AddDays(19),
            Members = [kai, hina],
        };
        var operationsRefresh = new Project
        {
            Name = "Operations refresh",
            Description = "Document repeatable workflows for the growing team.",
            Icon = "⌘",
            Accent = "amber",
            Status = ProjectStatus.Planning,
            DueDate = today.AddDays(26),
            Members = [hina, jordan],
        };
        var researchRepository = new Project
        {
            Name = "Research repository",
            Description = "Centralise interviews, insights, and opportunity areas.",
            Icon = "✦",
            Accent = "green",
            Status = ProjectStatus.Complete,
            DueDate = today.AddDays(-3),
            Members = [mina, hina],
        };
        db.Projects.AddRange(mobileApp, websiteRedesign, operationsRefresh, researchRepository);

        db.WorkItems.AddRange(
            new WorkItem { Title = "Review onboarding flow", Project = mobileApp, Status = WorkItemStatus.ToDo, Priority = WorkItemPriority.High, DueDate = today, Assignee = jordan },
            new WorkItem { Title = "Prepare sprint planning", Project = operationsRefresh, Status = WorkItemStatus.ToDo, Priority = WorkItemPriority.Medium, DueDate = today.AddDays(1), Assignee = hina },
            new WorkItem { Title = "Share research findings", Project = websiteRedesign, Status = WorkItemStatus.ToDo, Priority = WorkItemPriority.Low, DueDate = today.AddDays(5), Assignee = mina },
            new WorkItem { Title = "Mobile app wireframes", Project = mobileApp, Status = WorkItemStatus.InProgress, Priority = WorkItemPriority.High, DueDate = today, Assignee = jordan },
            new WorkItem { Title = "Update pricing page", Project = websiteRedesign, Status = WorkItemStatus.InProgress, Priority = WorkItemPriority.Medium, DueDate = today.AddDays(4), Assignee = kai },
            new WorkItem { Title = "Draft interview guide", Project = researchRepository, Status = WorkItemStatus.InReview, Priority = WorkItemPriority.Low, DueDate = today.AddDays(8), Assignee = mina });

        await db.SaveChangesAsync();
    }
}
