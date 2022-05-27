namespace SimpleTaskApi.Interfaces;

public interface ITasksService
{
    Task<string> Get(Guid id);
    Task<Guid> Create();
}