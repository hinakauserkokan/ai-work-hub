namespace AiWorkHub.Models;

/// <summary>A person who can be assigned to projects and tasks.</summary>
public class TeamMember
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Initials { get; set; }

    /// <summary>CSS accent class used for the avatar, e.g. "purple". Empty string is a valid, neutral tone.</summary>
    public string Tone { get; set; } = string.Empty;

    public ICollection<Project> Projects { get; set; } = [];
    public ICollection<WorkItem> AssignedWorkItems { get; set; } = [];
}
