using Microsoft.EntityFrameworkCore;
using SimpleTaskApi.DAL;
using SimpleTaskApi.DAL.Entities;
using SimpleTaskApi.Interfaces;
using Status = SimpleTaskApi.Enums.Status;

namespace SimpleTaskApi.Services;

public class TasksService : ITasksService
{
    private readonly TasksContext _context;
    private readonly IQuartzService _quartzService;

    public TasksService(TasksContext context, IQuartzService quartzService)
    {
        _context = context;
        _quartzService = quartzService;
    }
    
    public async Task<string> Get(Guid id)
    {
        var task = await _context.Tasks.Include(x => x.Status).FirstOrDefaultAsync(x => x.Id == id);
        return task == null
            ? string.Empty
            : task.Status.Name;
    }

    public async Task<Guid> Create()
    {
        var newTask = new SimpleTask
        {
            Id = Guid.NewGuid(),
            LastModifiedDate = DateTime.UtcNow,
            StatusId = (int)Status.Created
        };

        await _context.Tasks.AddAsync(newTask);
        await _context.SaveChangesAsync();

        _quartzService.ScheduleTaskStatusChange(newTask.Id);

        return newTask.Id;
    }
}