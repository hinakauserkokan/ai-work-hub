namespace AiWorkHub.Models;

public class Project
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;

    /// <summary>A single emoji or symbol shown as the project's icon.</summary>
    public string Icon { get; set; } = "◈";

    /// <summary>CSS accent class for the icon, e.g. "purple".</summary>
    public string Accent { get; set; } = "purple";

    public ProjectStatus Status { get; set; } = ProjectStatus.Planning;
    public DateOnly? DueDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<TeamMember> Members { get; set; } = [];
    public ICollection<WorkItem> WorkItems { get; set; } = [];
}
