namespace ProjectTasksCosmosApi.Interfaces;

using ProjectTasksCosmosApi.Models.Dto;

public interface IHealthcheckService
{
    public Task<HealthcheckStatusDto> GetStatus();
}
