namespace ProjectTasksApi.Interfaces;

using ProjectTasksApi.Models.Dto;

public interface IHealthcheckService
{
    public Task<HealthcheckStatusDto> GetStatus();
}
