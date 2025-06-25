
namespace PMT.Core.DTOs;

public class ProjectTaskResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Guid? ProjectId { get; set; }
    public Guid TeamId { get; set; }
    public int Status { get; set; }
    public DateTime ScheduledEndDate { get; set; }
    public DateTime? CompletionDate { get; set; }
}
