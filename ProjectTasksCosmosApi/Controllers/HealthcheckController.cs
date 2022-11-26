namespace ProjectTasksCosmosApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using ProjectTasksCosmosApi.Interfaces;
using ProjectTasksCosmosApi.Models.Dto;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
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
