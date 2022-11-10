namespace ProjectTasksApi.Services;

using ProjectTasksApi.Models;

public static class HealthCheckService
{
    public static HealthcheckStatus GetStatus()
    {
        return new HealthcheckStatus { status = true };
    }
}
