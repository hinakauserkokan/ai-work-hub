using AiWorkHub.Models;
using Microsoft.EntityFrameworkCore;

namespace AiWorkHub.Data;

public class AiWorkHubDbContext(DbContextOptions<AiWorkHubDbContext> options) : DbContext(options)
{
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<WorkItem> WorkItems => Set<WorkItem>();
    public DbSet<TeamMember> TeamMembers => Set<TeamMember>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TeamMember>(member =>
        {
            member.Property(m => m.Name).HasMaxLength(120).IsRequired();
            member.Property(m => m.Initials).HasMaxLength(4).IsRequired();
            member.Property(m => m.Tone).HasMaxLength(20);
        });

        modelBuilder.Entity<Project>(project =>
        {
            project.Property(p => p.Name).HasMaxLength(200).IsRequired();
            project.Property(p => p.Description).HasMaxLength(1000);
            project.Property(p => p.Icon).HasMaxLength(10);
            project.Property(p => p.Accent).HasMaxLength(20);

            // Store enums as text so rows stay readable when inspected directly in Postgres.
            project.Property(p => p.Status).HasConversion<string>().HasMaxLength(20);

            // Implicit many-to-many: EF Core creates and manages the join table (ProjectTeamMember).
            project.HasMany(p => p.Members)
                .WithMany(m => m.Projects)
                .UsingEntity(join => join.ToTable("ProjectTeamMember"));
        });

        modelBuilder.Entity<WorkItem>(workItem =>
        {
            workItem.Property(w => w.Title).HasMaxLength(200).IsRequired();
            workItem.Property(w => w.Status).HasConversion<string>().HasMaxLength(20);
            workItem.Property(w => w.Priority).HasConversion<string>().HasMaxLength(20);

            workItem.HasOne(w => w.Project)
                .WithMany(p => p.WorkItems)
                .HasForeignKey(w => w.ProjectId)
                .OnDelete(DeleteBehavior.SetNull);

            workItem.HasOne(w => w.Assignee)
                .WithMany(m => m.AssignedWorkItems)
                .HasForeignKey(w => w.AssigneeId)
                .OnDelete(DeleteBehavior.SetNull);
        });
    }
}
