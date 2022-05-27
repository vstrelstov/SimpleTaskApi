using Microsoft.EntityFrameworkCore;
using Quartz;
using SimpleTaskApi.Domain;

namespace SimpleTaskApi.Jobs;

public class FinishTaskJob : BaseJob, IJob
{
    private readonly ILogger<FinishTaskJob> _logger;
    
    public FinishTaskJob(IServiceProvider serviceProvider, ILogger<FinishTaskJob> logger) : base(serviceProvider)
    {
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Started FinishTaskJob");
        
        try
        {
            var dbContext = GetContext();
            Guid taskId = GetTaskId(context);
        
            var retrievedTask = await dbContext.Tasks.FirstAsync(x => x.Id == taskId);
            retrievedTask.StatusId = (int)Status.Finished;
            retrievedTask.LastModifiedDate = DateTime.Now;

            await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
        }
        
        _logger.LogInformation("Completed FinishTaskJob");
    }
}