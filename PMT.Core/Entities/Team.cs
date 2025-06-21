using System.ComponentModel.DataAnnotations.Schema;

namespace PMT.Core.Entities;

[Table("Teams", Schema = "public")]
public class Team : EntityBase
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Guid OrganizationId { get; set; }
}
