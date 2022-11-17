namespace ProjectTasksApi.Models.Dto;

public class HealthcheckStatusDto
{
    public bool serviceStatus { get; set; }

    public bool dbConnectionStatus { get; set; }
}
