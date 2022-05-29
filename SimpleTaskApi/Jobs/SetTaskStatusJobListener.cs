using Quartz;
using SimpleTaskApi.Enums;

namespace SimpleTaskApi.Jobs;

public class SetTaskStatusJobListener : IJobListener
{
    private readonly ILogger<SetTaskStatusJobListener> _logger;

    public SetTaskStatusJobListener(ILogger<SetTaskStatusJobListener> logger)
    {
        _logger = logger;
    }
    
    public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        _logger.LogInformation($"Setting task status for task {GetTaskId(context)}");
        return Task.CompletedTask;
    }

    public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        _logger.LogWarning($"Setting task status for task {GetTaskId(context)} was vetoed");
        return Task.CompletedTask;
    }

    public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (jobException == null)
        {
            var statusId = context.MergedJobDataMap.GetIntValue("statusId");
            if ((Status)statusId != Status.Running)
            {
                return;
            }

            var taskId = GetTaskId(context);
            context.MergedJobDataMap.Clear();

            var triggerStartJob = TriggerBuilder.Create()
                .WithIdentity($"Trigger setting Completed status for task {taskId}")
                .StartAt(DateTime.UtcNow.AddMinutes(2))
                .UsingJobData("taskId", taskId)
                .UsingJobData("statusId", (int)Status.Finished)
                .WithSimpleSchedule(x => x.Build())
                .Build();

            await context.Scheduler.ScheduleJob(context.JobDetail, new[] { triggerStartJob }, true);
        }
    }

    public string Name => "SetTaskStatusJob listener";

    private Guid GetTaskId(IJobExecutionContext executionContext) =>
        executionContext.MergedJobDataMap.GetGuidValue("taskId");
}