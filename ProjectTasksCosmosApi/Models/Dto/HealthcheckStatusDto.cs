namespace ProjectTasksCosmosApi.Models.Dto;

public class HealthcheckStatusDto
{
    public bool ServiceStatus { get; set; }

    public bool DbConnectionStatus { get; set; }
}
