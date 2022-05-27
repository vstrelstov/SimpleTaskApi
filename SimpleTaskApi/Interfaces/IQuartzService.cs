namespace SimpleTaskApi.Interfaces;

public interface IQuartzService
{
    Task ScheduleTaskStatusChange(Guid taskId);
}