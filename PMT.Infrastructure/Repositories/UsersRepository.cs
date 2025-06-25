using ApplicationCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PMT.Core.Entities;
using PMT.Core.RepositoryContracts;
using PMT.Infrastructure.Context;

namespace PMT.Infrastructure.Repositories;

public class UsersRepository(ApplicationDbContext _dbContext) : IUsersRepository
{
    public async Task<ApplicationUser?> AddUserAsync(ApplicationUser user)
    {
        if (user == null) return null;
        if (user.Id != Guid.Empty) return null;

        user.Id = Guid.NewGuid();
        user.CreatedBy = user.UpdatedBy = user.Id;
        user.CreatedOn = user.UpdatedOn = DateTime.Now;

        await _dbContext.Users.AddAsync(user);
        int recordsAffected = await _dbContext.SaveChangesAsync();

        if (recordsAffected > 0)
        {
            return user;
        }
        return null;
    }

    public async Task<ApplicationUser?> UpdateUserAsync(ApplicationUser user)
    {
        ApplicationUser? existingUser = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == user.Id);

        if (existingUser == null) return null;

        existingUser.FullName = user.FullName;
        existingUser.Email = user.Email;
        existingUser.PhoneNumber = user.PhoneNumber;
        //existingUser.UpdatedBy = ??
        existingUser.UpdatedOn = DateTime.Now;

        int recordsAffected = await _dbContext.SaveChangesAsync();

        return recordsAffected > 0 ? existingUser : null;
    }

    public async Task<ApplicationUser?> ValidateLoginCredentialsAsync(string? userName, string? password)
    {
        ApplicationUser? existingUser = await _dbContext.Users.FirstOrDefaultAsync(user => userName == userName);
        if (existingUser == null) return null;

        string encryptedPassword = CryptoService.EncryptWithAlgoInfo(password!, existingUser.AlgoInfo!);
        if (encryptedPassword == null || encryptedPassword != existingUser.PasswordHash) return null;

        return existingUser;
    }

    public async Task<ApplicationUser?> GetUserByUserNameAsync(string? userName)
    {
        ApplicationUser? existingUser = await _dbContext.Users.FirstOrDefaultAsync(user => user.UserName == userName);

        return existingUser;
    }

    public async Task<bool> SetUserTeamAsync(Guid? userId, Guid? teamId)
    {
        if (userId == null || userId == Guid.Empty) throw new ArgumentNullException("Invalid UserId. Update operation failed");
        if (teamId == null || teamId == Guid.Empty) throw new ArgumentNullException("Invalid TeamId. Update operation failed.");

        ApplicationUser? appUser = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);
        if (appUser == null) throw new ArgumentNullException("User not found. Update operation failed.");

        appUser.TeamId = teamId;
        appUser.UpdatedOn = DateTime.Now;
        int recordsUpdated = await _dbContext.SaveChangesAsync();

        return recordsUpdated > 0;
    }

    public async Task<bool> DeleteUserAsync(Guid? userId)
    {
        if (userId == null || userId == Guid.Empty) throw new ArgumentNullException("Invalid UserId. Delete operation failed");

        ApplicationUser? existingUser = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);

        if (existingUser == null) throw new ArgumentNullException("User not found. Delete operation failed.");

        _dbContext.Users.Remove(existingUser);

        int recordsAffected = await _dbContext.SaveChangesAsync();

        return (recordsAffected > 0);
    }
}
