
using PMT.Core.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PMT.Core.DTOs;

public class ProjectUpdateRequest
{
    [Required(ErrorMessage = "{0} is required.")]
    [DisplayName("Project Id")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "{0} is required.")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "{0} length must be between {2} and {1} characters.")]
    [DisplayName("Project Name")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "{0} is required.")]
    [DisplayName("Project Team")]
    public Guid TeamId { get; set; }

    [Required(ErrorMessage = "{0} is required.")]
    [DisplayName("Scheduled End Date")]
    public DateTime ScheduledEndDate { get; set; }

    [Required(ErrorMessage = "{0} is required.")]
    [DisplayName("Project Status")]
    public ProjectStatus Status { get; set; }

    [DisplayName("Completion Date")]
    public DateTime? CompletionDate { get; set; }
}

