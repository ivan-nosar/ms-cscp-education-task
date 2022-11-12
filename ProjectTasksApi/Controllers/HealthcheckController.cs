namespace ProjectTasksApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using ProjectTasksApi.Interfaces;
using ProjectTasksApi.Models.Dto;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion( "1.0" )]
public class HealthcheckController : ControllerBase {

    private readonly IHealthcheckService service;

    public HealthcheckController(IHealthcheckService service)
    {
        this.service = service;
    }

    [HttpGet]
    public async Task<HealthcheckStatusDto> GetStatus()
    {
        return await service.GetStatus();
    }
}
