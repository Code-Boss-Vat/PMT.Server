
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace PMT.Core.DTOs;

public class SignInRequest
{
    [Required(ErrorMessage = "{0} is required.")]
    [DisplayName("User Name")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "Length of {0} should be between {1} and {2}.")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "{0} is required.")]
    [StringLength(20, MinimumLength = 8, ErrorMessage = "{0} length must be between {2} and {1} characters.")]
    [DataType(DataType.Password)]
    [DisplayName("Password")]
    public string? Password { get; set; }
}