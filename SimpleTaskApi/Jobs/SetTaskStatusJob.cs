using Microsoft.EntityFrameworkCore;
using Quartz;
using SimpleTaskApi.DAL;

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
            _logger.LogInformation($"Started SetTaskStatusJob");

            var dataMap = context.MergedJobDataMap;
            Guid taskId = dataMap.GetGuidValue("taskId");
            int statusId = dataMap.GetIntValue("statusId");
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<TasksContext>();

            var task = await dbContext.Tasks.FirstAsync(x => x.Id == taskId);
            task.LastModifiedDate = DateTime.Now;
            task.StatusId = statusId;

            await dbContext.SaveChangesAsync();
            
            _logger.LogInformation("Completed SetTaskStatusJob");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
        }
        
    }
}