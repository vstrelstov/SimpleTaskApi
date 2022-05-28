using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleTaskApi.DAL.Entities;

[Table("Task")]
public class SimpleTask
{
    [Key]
    public Guid Id { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public int StatusId { get; set; }
    [ForeignKey(nameof(StatusId))]
    public virtual Status Status { get; set; }
}