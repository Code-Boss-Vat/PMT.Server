
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PMT.Core.DTOs;

public class OrganizationUpdateRequest
{
    [Required(ErrorMessage = "{0} is required.")]
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "{0} is required.")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "{0} length must be between {2} and {1} characters.")]
    [DisplayName("Organization Name")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "{0} is required.")]
    [DisplayName("Phone Number")]
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "{0} is required.")]
    [DisplayName("Organization Email")]
    public string? Email { get; set; }

    [DisplayName("Website")]
    public string? Website { get; set; }

    [Required(ErrorMessage = "{0} is required.")]
    [DisplayName("Organization Address")]
    public string? Address { get; set; }

    public Guid? AdminId { get; set; }

    public Guid? UpdatedBy { get; set; }
}
