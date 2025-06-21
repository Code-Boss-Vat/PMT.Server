using System.ComponentModel.DataAnnotations.Schema;

namespace PMT.Core.Entities;

[Table("Projects", Schema = "public")]
public class Project : EntityBase
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Guid TeamId { get; set; }
    public DateTime ScheduledEndDate { get; set; }
    public int Status { get; set; }
    public DateTime? CompletionDate { get; set; }
}
