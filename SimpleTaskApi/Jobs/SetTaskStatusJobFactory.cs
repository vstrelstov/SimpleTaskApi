using Quartz;
using Quartz.Spi;

namespace SimpleTaskApi.Jobs;

public class SetTaskStatusJobFactory : IJobFactory
{
    private readonly IServiceProvider _serviceProvider;

    public SetTaskStatusJobFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler) =>
        _serviceProvider.GetService<SetTaskStatusJob>();

    public void ReturnJob(IJob job)
    {
        (job as IDisposable)?.Dispose();
    }
}