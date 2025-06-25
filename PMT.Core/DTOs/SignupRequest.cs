
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PMT.Core.DTOs;

public class SignupRequest
{
    [Required(ErrorMessage = "{0} is required.")]
    [StringLength(15, MinimumLength = 5, ErrorMessage = "{0} length must be between {2} and {1} characters.")]
    [DisplayName("User Name")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "{0} is required.")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "{0} length must be between {2} and {1} characters.")]
    [DisplayName("Full Name")]
    public string? FullName { get; set; }

    [Required(ErrorMessage = "{0} is required.")]
    [DisplayName("Password")]
    [StringLength(20, MinimumLength = 8, ErrorMessage = "{0} length must be between {2} and {1} characters.")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Required(ErrorMessage = "{0} is required.")]
    [DisplayName("Confirm Password")]
    [Compare("Password", ErrorMessage = "Passwords are not matching.")]
    [DataType(DataType.Password)]
    public string? ConfirmPassword { get; set; }

    [Required(ErrorMessage = "{0} is required.")]
    [DisplayName("Phone Number")]
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "{0} is required.")]
    [EmailAddress(ErrorMessage = "{0} is not in proper format.")]
    [DisplayName("Email Address")]
    public string? Email { get; set; }

    [Range(typeof(bool), "true", "true", ErrorMessage = "Please read and accept the terms & conditions to create your account.")]
    public bool AreTermsNConditionsAccepted { get; set; }
}