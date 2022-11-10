namespace TaskTasksApi.Controllers; 

using Microsoft.AspNetCore.Mvc;
using ProjectTasksApi.Models;
using ProjectTasksApi.Servcies;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class TaskController : ControllerBase
{
    private readonly ILogger<TaskController> logger;

    public TaskController(ILogger<TaskController> logger)
    {
        this.logger = logger;
    }

    [HttpGet]
    public IEnumerable<Task> Get() => TaskService.GetAll();

    [HttpGet("{id}")]
    public ActionResult<Task> Get(int id)
    {
        Task? task = TaskService.Get(id);

        if (task== null)
        {
            logger.LogInformation($"Unable to find task #{id}");
            return NotFound();
        }

        return task;
    }

    [HttpPost]
    public ActionResult<Task> Post([FromBody] Task task)
    {
        TaskService.Add(task);
        return CreatedAtAction(nameof(Post), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public ActionResult<Task> Put(int id, [FromBody] Task task)
    {
        if (id != task.Id)
        {
            logger.LogInformation($"Task's id mismatch: ${id} (from URI) doesn't match ${task.Id} (from body)");
            return BadRequest();
        }

        bool updateResult = TaskService.Update(id, task);
        if (!updateResult)
        {
            logger.LogInformation($"Unable to find task #{id}");
            return NotFound();
        }

        return CreatedAtAction(nameof(Put), new { id = task.Id }, task);
    }

    [HttpDelete("{id}")]
    public ActionResult<Task> Delete(int id)
    {
        bool deleteResult = TaskService.Delete(id);
        if (!deleteResult)
        {
            logger.LogInformation($"Unable to find task #{id}");
            return NotFound();
        }

        return Ok();
    }
}
