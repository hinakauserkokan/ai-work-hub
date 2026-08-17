namespace AiWorkHub.Models;

/// <summary>Turns a stored due date into the label and CSS class the Razor pages render.</summary>
public static class DueDateExtensions
{
    public static string DueText(this DateOnly? dueDate, DateOnly today)
    {
        if (dueDate is not { } date)
        {
            return "No due date";
        }

        if (date == today)
        {
            return "Today";
        }

        if (date == today.AddDays(1))
        {
            return "Tomorrow";
        }

        var formatted = date.ToString("ddd, d MMM");
        return date < today ? $"Overdue · {formatted}" : formatted;
    }

    /// <summary>CSS class that highlights a due date needing attention (due today or overdue).</summary>
    public static string DueClass(this DateOnly? dueDate, DateOnly today) => dueDate is { } date && date <= today ? "today" : string.Empty;
}
