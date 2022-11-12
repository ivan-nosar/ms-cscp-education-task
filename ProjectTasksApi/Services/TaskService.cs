namespace ProjectTasksApi.Services;

using Microsoft.EntityFrameworkCore;
using ProjectTasksApi.Data;
using ProjectTasksApi.Interfaces;
using ProjectTasksApi.Models;
using ProjectTasksApi.Models.Dto;
using System.Collections.Generic;

public class TaskService : ITaskService
{
    private readonly ProjectTasksContext context;
    ILogger<TaskService> logger;

    public TaskService(
        ProjectTasksContext context,
        ILogger<TaskService> logger
    )
    {
        this.context = context;
        this.logger = logger;
    }

    public async Task<IEnumerable<Task>> GetAll()
    {
        try
        {
            return await context.Tasks
                .Where(_ => true)
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

    public async Task<Task?> Add(TaskInputDto taskDto)
    {
        var newEntity = new Task()
        {
            Name = taskDto.Name,
            Description = taskDto.Description,
            ProjectID = (int)taskDto.ProjectID!,
        };

        try
        {
            var existingProject = await context.Projects.SingleOrDefaultAsync
            (
                project => project.ID == newEntity.ProjectID
            );
            if (existingProject == null)
            {
                logger.LogInformation($"Project #{taskDto.ProjectID} not found");
                return null;
            }

            context.Tasks.Add(newEntity);
            await context.SaveChangesAsync();

            return newEntity;
        }
        catch (Exception ex)
        {
            logger.LogError($"Database insert task {taskDto.Name} error: {ex.Message}");
            return null;
        }
    }

    public async Task<Task?> Update(int id, TaskInputDto taskDto)
    {
        var existingEntity = await Get(id);
        if (existingEntity == null)
        {
            return null;
        }

        try
        {
            var existingProject = await context.Projects.SingleOrDefaultAsync
            (
                project => project.ID == taskDto.ProjectID
            );
            if (existingProject == null)
            {
                logger.LogInformation($"Project #{taskDto.ProjectID} not found");
                return null;
            }

            context.Entry(existingEntity).CurrentValues.SetValues(taskDto);
            await context.SaveChangesAsync();

            return existingEntity;
        }
        catch (Exception ex)
        {
            logger.LogError($"Database update task #{id} error: {ex.Message}");
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
            context.Tasks.Remove(existingEntity);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError($"Database delete task #{id} error: {ex.Message}");
            return false;
        }

        return true;
    }
}
