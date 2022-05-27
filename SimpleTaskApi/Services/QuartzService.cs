using Quartz;
using Quartz.Impl;
using SimpleTaskApi.Interfaces;
using SimpleTaskApi.Jobs;

namespace SimpleTaskApi.Services;

public class QuartzService : IQuartzService
{
    private readonly IScheduler _scheduler;

    public QuartzService(IServiceProvider serviceProvider)
    {
        var factory = new StdSchedulerFactory();
        _scheduler = factory.GetScheduler().Result;
        _scheduler.JobFactory = new SetTaskStatusJobFactory(serviceProvider);
    }
    
    public async Task ScheduleTaskStatusChange(Guid taskId, int statusId)
    {
        IJobDetail startJob = JobBuilder.Create<SetTaskStatusJob>()
            .WithIdentity($"Setting status for task {taskId}")
            .Build();
        ITrigger triggerStartJob = TriggerBuilder.Create()
            .WithIdentity($"Trigger setting status for task {taskId}")
            .UsingJobData("taskId", taskId)
            .UsingJobData("statusId", statusId)
            .StartNow()
            .WithSimpleSchedule(x => x.Build())
            .Build();
        
        await _scheduler.ScheduleJob(startJob, triggerStartJob);
        await _scheduler.Start();
    }
}