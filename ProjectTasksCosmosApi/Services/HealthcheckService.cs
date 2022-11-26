namespace ProjectTasksCosmosApi.Services;

using Microsoft.EntityFrameworkCore;
using ProjectTasksCosmosApi.Data;
using ProjectTasksCosmosApi.Interfaces;
using ProjectTasksCosmosApi.Models.Dto;

public class HealthCheckService : IHealthcheckService
{
    private readonly ProjectTasksCosmosContext context;
    ILogger<HealthCheckService> logger;

    public HealthCheckService(
        ProjectTasksCosmosContext context,
        ILogger<HealthCheckService> logger
    )
    {
        this.context = context;
        this.logger = logger;
    }

    public async Task<HealthcheckStatusDto> GetStatus()
    {
        bool serviceStatus = true;
        bool dbConnectionStatus = false;
        try
        {
            await context.Projects.CountAsync();
            dbConnectionStatus = true;
        } catch( Exception ex)
        {
            logger.LogError($"Unable to execute DB query: {ex.Message}");
        }

        return new HealthcheckStatusDto
        {
            ServiceStatus = serviceStatus,
            DbConnectionStatus = dbConnectionStatus
        };
    }
}
