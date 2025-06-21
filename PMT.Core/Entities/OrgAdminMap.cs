using System.ComponentModel.DataAnnotations.Schema;

namespace PMT.Core.Entities;

[Table("OrgAdminMap", Schema = "public")]
public class OrgAdminMap : EntityBase
{
    public Guid OrganizationId { get; set; }
    public Guid AdminId { get; set; }
}
