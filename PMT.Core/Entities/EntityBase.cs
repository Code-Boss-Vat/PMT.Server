
using System.ComponentModel.DataAnnotations.Schema;

namespace PMT.Core.Entities;

public class EntityBase
{
    public bool IsActive { get; set; }
    public Guid CreatedBy { get; set; }
    [Column(TypeName = "timestamp without time zone")]
    public DateTime CreatedOn { get; set; }
    public Guid UpdatedBy { get; set; }
    [Column(TypeName = "timestamp without time zone")]
    public DateTime UpdatedOn { get; set; }
}

