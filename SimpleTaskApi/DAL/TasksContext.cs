using Microsoft.EntityFrameworkCore;
using SimpleTaskApi.DAL.Configuration;
using SimpleTaskApi.DAL.Entities;

namespace SimpleTaskApi.DAL;

public class TasksContext : DbContext
{
    public TasksContext(DbContextOptions<TasksContext> options) : base(options)
    {
    }

    public DbSet<SimpleTask> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new StatusConfiguration());
    }
}