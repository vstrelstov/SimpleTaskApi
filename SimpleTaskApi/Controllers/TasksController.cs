using Microsoft.AspNetCore.Mvc;

namespace SimpleTaskApi.Controllers;

[ApiController]
[Route("task")]
public class TasksController : ControllerBase
{
    private readonly ILogger<TasksController> _logger;

    public TasksController(ILogger<TasksController> logger)
    {
        _logger = logger;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] string id)
    {
        if (!Guid.TryParse(id, out var guidId))
        {
            return BadRequest("Input id was not in correct format");
        }
        
        return Ok();
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> Create()
    {
        return Accepted("8FA0CDC1-EA81-4E61-874D-584F368E5E69");
    }
}