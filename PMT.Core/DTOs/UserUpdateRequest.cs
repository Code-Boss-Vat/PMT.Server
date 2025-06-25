
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PMT.Core.DTOs;

public class UserUpdateRequest
{
    [Required(ErrorMessage = "{0} is required.")]
    public Guid UserId { get; set; }

    [Required(ErrorMessage = "{0} is required.")]
    [Length(5, 15, ErrorMessage = "Length must be between {0} and {1} characters.")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "{0} is required.")]
    public string? FullName { get; set; }

    [Required(ErrorMessage = "{0} is required.")]
    [DisplayName("Password")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "{0} is required.")]
    [DisplayName("Confirm Password")]
    [Compare("Password")]
    public string? ConfirmPassword { get; set; }

    [Required(ErrorMessage = "{0} is required.")]
    [Phone]
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "{0} is required.")]
    [EmailAddress]
    public string? Email { get; set; }
}

