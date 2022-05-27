using Microsoft.EntityFrameworkCore;
using SimpleTaskApi.DAL;
using SimpleTaskApi.DAL.Models;
using SimpleTaskApi.Interfaces;

namespace SimpleTaskApi.Services;

public class TasksService : ITasksService
{
    private readonly TasksContext _context;
    
    public TasksService(TasksContext context)
    {
        _context = context;
    }
    
    public async Task<string> Get(Guid id)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        return task == null
            ? string.Empty
            : task.Status.Name;
    }

    public async Task<Guid> Create()
    {
        var newTask = new SimpleTask
        {
            Id = Guid.NewGuid(),
            LastModifiedDate = DateTime.Now,
            StatusId = (int)Domain.Status.New
        };

        await _context.Tasks.AddAsync(newTask);
        await _context.SaveChangesAsync();

        return newTask.Id;
    }
}