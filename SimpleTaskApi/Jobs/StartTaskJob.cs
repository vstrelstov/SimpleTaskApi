using Microsoft.EntityFrameworkCore;
using Quartz;
using SimpleTaskApi.Domain;

namespace SimpleTaskApi.Jobs;

public class StartTaskJob : BaseJob, IJob
{
    private readonly ILogger<StartTaskJob> _logger;
    
    public StartTaskJob(IServiceProvider serviceProvider, ILogger<StartTaskJob> logger) : base(serviceProvider)
    {
        _logger = logger;
    }
    
    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Started StartTaskJob");
        
        try
        {
            var dbContext = GetContext();
            var taskId = GetTaskId(context);

            var retrievedTask = await dbContext.Tasks.FirstAsync(x => x.Id == taskId);
            retrievedTask.StatusId = (int)Status.Running;
            retrievedTask.LastModifiedDate = DateTime.Now;

            await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
        }
        
        _logger.LogInformation("Completed StartTaskJob");
    }
    
}