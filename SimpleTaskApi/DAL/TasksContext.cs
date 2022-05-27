using Microsoft.EntityFrameworkCore;
using SimpleTaskApi.DAL.Models;

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

        modelBuilder.Entity<Status>().HasData(
            new Status
            {
                Id = 1,
                Name = "Created"
            },
            new Status
            {
                Id = 2,
                Name = "Running"
            },
            new Status
            {
                Id = 3,
                Name = "Finished"
            });
    }
}