using Microsoft.EntityFrameworkCore;
using Quartz;
using SimpleTaskApi.DAL;
using SimpleTaskApi.Domain;

namespace SimpleTaskApi.Jobs;

public class SetTaskStatusJob : IJob
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<SetTaskStatusJob> _logger;

    public SetTaskStatusJob(IServiceProvider serviceProvider, ILogger<SetTaskStatusJob> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    
    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            var dataMap = context.MergedJobDataMap;
            Guid taskId = dataMap.GetGuidValue("taskId");
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<TasksContext>();

            var task = await dbContext.Tasks.FirstAsync(x => x.Id == taskId);
            task.LastModifiedDate = DateTime.Now;
            task.StatusId = (Status)task.StatusId switch
            {
                Status.New => (int)Status.Running,
                Status.Running => (int)Status.Finished,
                _ => task.StatusId
            };

            await dbContext.SaveChangesAsync();
            
            _logger.LogInformation($"Setting task status for {taskId} completed");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error setting task status: {ex}");
        }
        
    }
}