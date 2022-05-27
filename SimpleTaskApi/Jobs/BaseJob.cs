using Quartz;
using SimpleTaskApi.DAL;

namespace SimpleTaskApi.Jobs;

public abstract class BaseJob
{
    private readonly IServiceProvider _serviceProvider;

    protected BaseJob(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected static Guid GetTaskId(IJobExecutionContext executionContext)
    {
        JobDataMap dataMap = executionContext.MergedJobDataMap;
        return dataMap.GetGuidValue("TaskId");
    }
    
    protected TasksContext GetContext()
    {
        using var scope = _serviceProvider.CreateScope();
        return scope.ServiceProvider.GetService<TasksContext>();
    }
}