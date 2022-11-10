namespace ProjectTasksApi.Controllers; 

using Microsoft.AspNetCore.Mvc;
using ProjectTasksApi.Models;
using ProjectTasksApi.Servcies;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ProjectController : ControllerBase
{
    private readonly ILogger<ProjectController> logger;

    public ProjectController(ILogger<ProjectController> logger)
    {
        this.logger = logger;
    }

    [HttpGet]
    public IEnumerable<Project> Get() => ProjectService.GetAll();

    [HttpGet("{id}")]
    public ActionResult<Project> Get(int id)
    {
        Project? project = ProjectService.Get(id);

        if (project== null)
        {
            logger.LogInformation($"Unable to find project #{id}");
            return NotFound();
        }

        return project;
    }

    [HttpPost]
    public ActionResult<Project> Post([FromBody] Project project)
    {
        ProjectService.Add(project);
        return CreatedAtAction(nameof(Post), new { id = project.Id }, project);
    }

    [HttpPut("{id}")]
    public ActionResult<Project> Put(int id, [FromBody] Project project)
    {
        if (id != project.Id)
        {
            logger.LogInformation($"Project's id mismatch: ${id} (from URI) doesn't match ${project.Id} (from body)");
            return BadRequest();
        }

        bool updateResult = ProjectService.Update(id, project);
        if (!updateResult)
        {
            logger.LogInformation($"Unable to find project #{id}");
            return NotFound();
        }

        return CreatedAtAction(nameof(Put), new { id = project.Id }, project);
    }

    [HttpDelete("{id}")]
    public ActionResult<Project> Delete(int id)
    {
        bool deleteResult = ProjectService.Delete(id);
        if (!deleteResult)
        {
            logger.LogInformation($"Unable to find project #{id}");
            return NotFound();
        }

        return Ok();
    }
}
