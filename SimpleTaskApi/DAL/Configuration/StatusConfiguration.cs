using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleTaskApi.DAL.Entities;

namespace SimpleTaskApi.DAL.Configuration;

public class StatusConfiguration : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
        builder.HasData(
            new Status
            {
                Id = (int)Domain.Status.Created,
                Name = Enum.GetName(typeof(Domain.Status), (int)Domain.Status.Created)
            },
            new Status
            {
                Id = (int)Domain.Status.Running,
                Name = Enum.GetName(typeof(Domain.Status), (int)Domain.Status.Running)
            },
            new Status
            {
                Id = (int)Domain.Status.Finished,
                Name = Enum.GetName(typeof(Domain.Status), (int)Domain.Status.Finished)
            });
    }
}