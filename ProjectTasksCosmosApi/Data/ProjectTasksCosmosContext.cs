using Azure;
using Microsoft.EntityFrameworkCore;
using ProjectTasksCosmosApi.Interfaces;
using ProjectTasksCosmosApi.Models;
using System.Reflection.Metadata;

namespace ProjectTasksCosmosApi.Data;

public class ProjectTasksCosmosContext : DbContext
{
    public ProjectTasksCosmosContext(DbContextOptions<ProjectTasksCosmosContext> options)
        : base(options) { }

    public DbSet<Project> Projects { get; set; }

    public DbSet<Models.Task> Tasks { get; set; }

    public const string PartitionKey = nameof(PartitionKey);

    public static string ComputePartitionKey<T>() =>
        typeof(T).Name;

    public void SetPartitionKey<T>(T entity)
        where T : IContainerEntity =>
            Entry(entity).Property(PartitionKey).CurrentValue =
                ComputePartitionKey<T>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>()
            .HasNoDiscriminator()
            .ToContainer(nameof(Projects))
            .HasPartitionKey(entity => entity.PartitionKey)
            .HasKey(entity => new { entity.ID });

        modelBuilder.Entity<Models.Task>()
            .HasNoDiscriminator()
            .ToContainer(nameof(Models.Task))
            .HasPartitionKey(entity => entity.PartitionKey)
            .HasKey(entity => new { entity.ID });

        base.OnModelCreating(modelBuilder);
    }
}
