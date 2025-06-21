using System.ComponentModel.DataAnnotations.Schema;

namespace PMT.Core.Entities;

[Table("Organizations", Schema = "public")]
public class Organization : EntityBase
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public string? Address { get; set; }
}
