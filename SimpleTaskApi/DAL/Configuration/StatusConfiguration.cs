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
                Id = (int)Enums.Status.Created,
                Name = Enum.GetName(typeof(Enums.Status), (int)Enums.Status.Created)
            },
            new Status
            {
                Id = (int)Enums.Status.Running,
                Name = Enum.GetName(typeof(Enums.Status), (int)Enums.Status.Running)
            },
            new Status
            {
                Id = (int)Enums.Status.Finished,
                Name = Enum.GetName(typeof(Enums.Status), (int)Enums.Status.Finished)
            });
    }
}