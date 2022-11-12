namespace ProjectTasksApi.Models.Dto;

public class HealthcheckStatusDto
{
    public bool status { get; set; }

    public bool dbConnectionStatus { get; set; }
}
