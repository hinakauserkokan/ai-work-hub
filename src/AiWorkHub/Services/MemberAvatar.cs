namespace AiWorkHub.Services;

/// <summary>Just enough of a TeamMember to render an avatar chip.</summary>
public sealed record MemberAvatar(string Initials, string Tone)
{
    public static readonly MemberAvatar Unassigned = new("?", "");
}
