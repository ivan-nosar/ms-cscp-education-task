namespace ProjectTasksApi.Controllers; 

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectTasksApi.Interfaces;
using ProjectTasksApi.Models.Dto;

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
        int? projectId = projectIdentifier.IsNullOrEmpty() ?
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

    [HttpPost]
    public async Task<ActionResult<TaskOutputDto>> Post([FromBody] TaskInputDto task)
    {
        if (!ModelState.IsValid)
        {
            logger.LogInformation($"Task model validation failed");
            return BadRequest(ModelState);
        }

        var newEntity = await service.Add(task);
        if (newEntity == null)
        {
            logger.LogInformation($"Unable to create task with name {task.Name}");
            return BadRequest();
        }

        return CreatedAtAction(
            nameof(Post),
            new { id = newEntity.ID },
            mapper.Map<TaskOutputDto>(newEntity)
        );
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TaskOutputDto>> Put(int id, [FromBody] TaskInputDto task)
    {
        if (!ModelState.IsValid)
        {
            logger.LogInformation($"Task model validation failed");
            return BadRequest(ModelState);
        }

        var updatedEntity = await service.Update(id, task);
        if (updatedEntity == null)
        {
            logger.LogInformation($"Unable to update task #{id}");
            return BadRequest();
        }

        return CreatedAtAction(
            nameof(Put),
            new { id = updatedEntity.ID },
            mapper.Map<TaskOutputDto>(updatedEntity)
        );
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var isDeletionSucceed = await service.Delete(id);
        if (!isDeletionSucceed)
        {
            logger.LogInformation($"Unable to delete task #{id}");
            return NotFound();
        }

        return Ok();
    }
}
