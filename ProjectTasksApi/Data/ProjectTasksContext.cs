namespace ProjectTasksApi.Data;

using Microsoft.EntityFrameworkCore;
using ProjectTasksApi.Models;

public class ProjectTasksContext : DbContext
{
    public ProjectTasksContext(DbContextOptions<ProjectTasksContext> options)
        : base(options) { }

    public DbSet<Task> Tasks { get; set; }
    public DbSet<Project> Projects { get; set; }
}
