namespace ProjectTasksCosmosApi.Services;

using Microsoft.EntityFrameworkCore;
using ProjectTasksCosmosApi.Data;
using ProjectTasksCosmosApi.Interfaces;
using ProjectTasksCosmosApi.Models;
using System.Collections.Generic;

public class TaskService : ITaskService
{
    private readonly ProjectTasksCosmosContext context;
    ILogger<TaskService> logger;

    public TaskService(
        ProjectTasksCosmosContext context,
        ILogger<TaskService> logger
    )
    {
        this.context = context;
        this.logger = logger;
    }

    public async Task<IEnumerable<Task>> GetAll(int? projectId)
    {
        try
        {
            if (projectId != null)
            {
                return await context.Tasks
                    .Where(task => task.ProjectID == projectId)
                    .ToListAsync();
            }

            return await context.Tasks
                .ToListAsync();
        }
        catch (Exception ex)
        {
            logger.LogError($"Database query tasks error: {ex.Message}");
            return new List<Task>();
        }
    }

    public async Task<Task?> Get(int id)
    {
        try
        {
            return await context.Tasks.SingleOrDefaultAsync(entity => entity.ID == id);
        }
        catch (Exception ex)
        {
            logger.LogError($"Database query task #{id} error: {ex.Message}");
            return null;
        }
    }
}
