using PMT.Core.Entities;

namespace PMT.Core.RepositoryContracts;

public interface IUsersRepository
{
    Task<ApplicationUser?> AddUserAsync(ApplicationUser user);
    Task<ApplicationUser?> UpdateUserAsync(ApplicationUser user);
    Task<ApplicationUser?> ValidateLoginCredentialsAsync(string? userName, string? password);
    Task<ApplicationUser?> GetUserByUserNameAsync(string? userName);
    Task<bool> SetUserTeamAsync(Guid? userId, Guid? teamId);
    Task<bool> DeleteUserAsync(Guid? userId);
}