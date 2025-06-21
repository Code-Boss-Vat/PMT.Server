
namespace PMT.Core.DTOs;

public class UserResponse
{
    public Guid Id { get; set; }
    public string? UserName { get; set; }
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public Guid? TeamId { get; set; }
    public bool? IsActive { get; set; }

    public bool IsInTeam
    {
        get
        {
            return (TeamId != null & TeamId != Guid.Empty);
        }
    }

}
