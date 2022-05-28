using Microsoft.AspNetCore.Mvc;
using SimpleTaskApi.Interfaces;

namespace SimpleTaskApi.Controllers;

[ApiController]
[Route("task")]
public class TasksController : ControllerBase
{
    private readonly ITasksService _tasksService;

    public TasksController(ITasksService tasksService)
    {
        _tasksService = tasksService;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var taskStatus = await _tasksService.Get(id);

        return string.IsNullOrWhiteSpace(taskStatus)
            ? NotFound("The requested task cannot be found")
            : Ok(taskStatus);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> Create()
    {
        var createdTaskId = await _tasksService.Create();
        return Accepted(createdTaskId);
    }
}