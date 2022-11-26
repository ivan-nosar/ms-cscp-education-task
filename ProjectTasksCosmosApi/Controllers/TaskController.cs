namespace ProjectTasksCosmosApi.Controllers; 

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectTasksCosmosApi.Interfaces;
using ProjectTasksCosmosApi.Models.Dto;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class TaskController : ControllerBase
{
    private readonly ITaskService service;
    private readonly ILogger<TaskController> logger;
    private readonly IMapper mapper;

    public TaskController(
        ITaskService service,
        ILogger<TaskController> logger,
        IMapper mapper
    )
    {
        this.service = service;
        this.logger = logger;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<TaskOutputDto>> GetAll([FromQuery( Name = "projectId")] string? projectIdentifier)
    {
        int? projectId = string.IsNullOrEmpty(projectIdentifier) ?
            null :
            int.Parse(projectIdentifier!);

        var tasks = await service.GetAll(projectId);
        return tasks.Select(project => mapper.Map<TaskOutputDto>(project));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskOutputDto>> Get(int id)
    {
        var task = await service.Get(id);

        if (task == null)
        {
            logger.LogInformation($"Unable to find task #{id}");
            return NotFound();
        }

        return mapper.Map<TaskOutputDto>(task);
    }
}
