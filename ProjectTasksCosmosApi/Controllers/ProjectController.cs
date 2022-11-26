namespace ProjectTasksCosmosApi.Controllers; 

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectTasksCosmosApi.Interfaces;
using ProjectTasksCosmosApi.Models.Dto;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ProjectController : ControllerBase
{
    private readonly IProjectService service;
    private readonly ILogger<ProjectController> logger;
    private readonly IMapper mapper;

    public ProjectController(
        IProjectService service,
        ILogger<ProjectController> logger,
        IMapper mapper
    )
    {
        this.service = service;
        this.logger = logger;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<ProjectOutputDto>> GetAll([FromQuery] string? withTasks)
    {
        var shouldPopulateTasks = StringToBool(withTasks);

        var projects = await service.GetAll(shouldPopulateTasks);

        return projects.Select
        (
            project => shouldPopulateTasks ?
                mapper.Map<ProjectOutputDto>(project) :
                new ProjectOutputDto { ID = project.ID, Name = project.Name, PartitionKey = project.PartitionKey }
        );
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectOutputDto>> Get(int id, [FromQuery] string? withTasks)
    {
        var shouldPopulateTasks = StringToBool(withTasks);
        var project = await service.Get(id, shouldPopulateTasks);

        if (project == null)
        {
            logger.LogInformation($"Unable to find project #{id}");
            return NotFound();
        }

        return shouldPopulateTasks ?
            mapper.Map<ProjectOutputDto>(project) :
            new ProjectOutputDto { ID = project.ID, Name = project.Name, PartitionKey = project.PartitionKey };
    }

    private bool StringToBool(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return false;
        }

        if (value!.ToLower() == "false")
        {
            return false;
        }

        return true;
    }
}
