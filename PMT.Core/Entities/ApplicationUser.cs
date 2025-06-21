using System.ComponentModel.DataAnnotations.Schema;

namespace PMT.Core.Entities;

[Table("Users", Schema = "public")]
public class ApplicationUser : EntityBase
{
    public Guid Id { get; set; }
    public string? UserName { get; set; }
    public string? FullName { get; set; }
    public string? PasswordHash { get; set; }
    public string? AlgoInfo { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public Guid? TeamId { get; set; }
}
