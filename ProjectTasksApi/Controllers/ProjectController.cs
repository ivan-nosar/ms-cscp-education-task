namespace ProjectTasksApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using ProjectTasksApi.Interfaces;
using ProjectTasksApi.Models.Dto;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ProjectController : AuthorizedControllerBase
{
    private readonly IProjectService service;
    private readonly ILogger<ProjectController> logger;
    private readonly IMapper mapper;

    public ProjectController(
        IConfiguration configuration,
        IProjectService service,
        ILogger<ProjectController> logger,
        IMapper mapper
    ) : base(configuration)
    {
        this.service = service;
        this.logger = logger;
        this.mapper = mapper;
    }

    [Authorize]
    [HttpGet]
    public async Task<IEnumerable<ProjectOutputDto>> GetAll([FromQuery] string? withTasks)
    {
        VerifyUserPermissions();

        var shouldPopulateTasks = StringToBool(withTasks);

        var projects = await service.GetAll(shouldPopulateTasks);

        return projects.Select
        (
            project => shouldPopulateTasks ?
                mapper.Map<ProjectOutputDto>(project) :
                new ProjectOutputDto { ID = project.ID, Name = project.Name }
        );
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectOutputDto>> Get(int id, [FromQuery] string? withTasks)
    {
        VerifyUserPermissions();

        var shouldPopulateTasks = StringToBool(withTasks);
        var project = await service.Get(id, shouldPopulateTasks);

        if (project == null)
        {
            logger.LogInformation($"Unable to find project #{id}");
            return NotFound();
        }

        return shouldPopulateTasks ?
            mapper.Map<ProjectOutputDto>(project) :
            new ProjectOutputDto { ID = project.ID, Name = project.Name };
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ProjectOutputDto>> Post([FromBody] ProjectInputDto project)
    {
        VerifyUserPermissions();

        if (!ModelState.IsValid)
        {
            logger.LogInformation($"Project model validation failed");
            return BadRequest(ModelState);
        }

        var newEntity = await service.Add(project);
        if (newEntity == null)
        {
            logger.LogInformation($"Unable to create project with name {project.Name}");
            return BadRequest();
        }

        return CreatedAtAction(
            nameof(Post),
            new { id = newEntity.ID },
            mapper.Map<ProjectOutputDto>(newEntity)
        );
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectOutputDto>> Put(int id, [FromBody] ProjectInputDto project)
    {
        VerifyUserPermissions();

        if (!ModelState.IsValid)
        {
            logger.LogInformation($"Project model validation failed");
            return BadRequest(ModelState);
        }

        var updatedEntity = await service.Update(id, project);
        if (updatedEntity == null)
        {
            logger.LogInformation($"Unable to update project #{id}");
            return BadRequest();
        }

        return CreatedAtAction(
            nameof(Put),
            new { id = updatedEntity.ID },
            mapper.Map<ProjectOutputDto>(updatedEntity)
        );
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        VerifyUserPermissions();

        var isDeletionSucceed = await service.Delete(id);
        if (!isDeletionSucceed)
        {
            logger.LogInformation($"Unable to delete project #{id}");
            return NotFound();
        }

        return Ok();
    }

    private bool StringToBool(string? value)
    {
        if (value.IsNullOrEmpty())
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
