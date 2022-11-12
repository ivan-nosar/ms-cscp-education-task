namespace ProjectTasksApi.Services;

using Microsoft.EntityFrameworkCore;
using ProjectTasksApi.Data;
using ProjectTasksApi.Interfaces;
using ProjectTasksApi.Models.Dto;

public class HealthCheckService : IHealthcheckService
{
    private readonly ProjectTasksContext context;
    ILogger<HealthCheckService> logger;

    public HealthCheckService(
        ProjectTasksContext context,
        ILogger<HealthCheckService> logger
    )
    {
        this.context = context;
        this.logger = logger;
    }

    public async Task<HealthcheckStatusDto> GetStatus()
    {
        bool status = true;
        bool dbConnectionStatus = false;
        try
        {
            await context.Database.ExecuteSqlRawAsync("SELECT 1;");
            dbConnectionStatus = true;
        } catch( Exception ex)
        {
            logger.LogError($"Unable to execute DB query: {ex.Message}");
        }

        return new HealthcheckStatusDto
        {
            status = status,
            dbConnectionStatus = dbConnectionStatus
        };
    }
}
