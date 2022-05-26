using Microsoft.AspNetCore.Mvc;

namespace SimpleTaskApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private readonly ILogger<TasksController> _logger;

    public TasksController(ILogger<TasksController> logger)
    {
        _logger = logger;
    }

    [HttpGet("task/{id}")]
    public async Task<IActionResult> Get([FromRoute] string taskId)
    {
        return Ok();
    }
    
    [HttpPost("task")]
    public async Task<IActionResult> Create()
    {
        return Ok();
    }
}