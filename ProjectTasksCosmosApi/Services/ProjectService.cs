namespace ProjectTasksCosmosApi.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectTasksCosmosApi.Data;
using ProjectTasksCosmosApi.Interfaces;
using ProjectTasksCosmosApi.Models;
using System.Reflection.Metadata;

public class ProjectService : IProjectService
{
    private readonly ProjectTasksCosmosContext context;
    ILogger<ProjectService> logger;

    public ProjectService(
        ProjectTasksCosmosContext context,
        ILogger<ProjectService> logger
    )
    {
        this.context = context;
        this.logger = logger;
    }

    public async Task<IEnumerable<Project>> GetAll(bool shouldPopulateTasks = false)
    {
        try
        {
            var projects = await context.Projects
                .Where(_ => true)
                .ToListAsync();

            if (shouldPopulateTasks)
            {
                projects.ForEach(project =>
                {
                    context.Entry(project)
                        .Collection(b => b.Tasks)
                        .Load();
                });
            }

            return projects;
        }
        catch (Exception ex)
        {
            logger.LogError($"Database query projects error: {ex.Message}");
            return new List<Project>();
        }
    }

    public async Task<Project?> Get(int id, bool shouldPopulateTasks = false)
    {
        try
        {
            var project = await context.Projects.SingleOrDefaultAsync(entity => entity.ID == id);

            if (project == null) {
                return null;
            }

            if (shouldPopulateTasks)
            {
                context.Entry(project)
                    .Collection(b => b.Tasks)
                    .Load();
            }

            return project;
        }
        catch (Exception ex)
        {
            logger.LogError($"Database query project #{id} error: {ex.Message}");
            return null;
        }
    }
}
