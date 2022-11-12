namespace ProjectTasksApi.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectTasksApi.Data;
using ProjectTasksApi.Interfaces;
using ProjectTasksApi.Models;
using ProjectTasksApi.Models.Dto;

public class ProjectService : IProjectService
{
    private readonly ProjectTasksContext context;
    ILogger<ProjectService> logger;

    public ProjectService(
        ProjectTasksContext context,
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

    public async Task<Project?> Add(ProjectInputDto projectDto)
    {
        var newEntity = new Project() { Name = projectDto.Name };
        try
        {
            context.Projects.Add(newEntity);
            await context.SaveChangesAsync();

            return newEntity;
        }
        catch (Exception ex)
        {
            logger.LogError($"Database insert project {projectDto.Name} error: {ex.Message}");
            return null;
        }
    }

    public async Task<Project?> Update(int id, ProjectInputDto projectDto)
    {
        var existingEntity = await Get(id);
        if (existingEntity == null)
        {
            return null;
        }

        try
        {
            context.Entry(existingEntity).CurrentValues.SetValues(projectDto);
            await context.SaveChangesAsync();

            return existingEntity;
        }
        catch (Exception ex)
        {
            logger.LogError($"Database update project #{id} error: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> Delete(int id)
    {
        var existingEntity = await Get(id);
        if (existingEntity == null)
        {
            return false;
        }

        try
        {
            context.Projects.Remove(existingEntity);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError($"Database delete project #{id} error: {ex.Message}");
            return false;
        }

        return true;
    }
}
