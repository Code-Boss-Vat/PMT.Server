
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace PMT.Core.DTOs;

public class SignInRequest
{
    [Required(ErrorMessage = "{0} is required.")]
    [DisplayName("User Name")]
    [Length(5, 20, ErrorMessage = "Length of {0} should be between {1} and {2}.")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "{0} is required.")]
    [DataType(DataType.Password)]
    [DisplayName("Password")]
    public string? Password { get; set; }
}