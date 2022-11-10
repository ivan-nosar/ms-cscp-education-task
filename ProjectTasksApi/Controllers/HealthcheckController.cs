namespace ProjectTasksApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using ProjectTasksApi.Models;
using ProjectTasksApi.Services;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion( "1.0" )]
public class HealthcheckController : ControllerBase {

    private readonly ILogger<HealthcheckController> _logger;

    public HealthcheckController(ILogger<HealthcheckController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public HealthcheckStatus GetStatus() => HealthCheckService.GetStatus();
}
