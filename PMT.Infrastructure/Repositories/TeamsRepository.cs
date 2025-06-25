using Microsoft.EntityFrameworkCore;
using PMT.Core.Entities;
using PMT.Core.RepositoryContracts;
using PMT.Infrastructure.Context;

namespace PMT.Infrastructure.Repositories;

public class TeamsRepository(ApplicationDbContext _dbContext) : ITeamsRepository
{
    public async Task<Team?> AddTeamAsync(Team team)
    {
        if (team == null) return null;
        if (team.Id != Guid.Empty) return null;

        team.Id = Guid.NewGuid();

        await _dbContext.Teams.AddAsync(team);

        int recordsAffected = await _dbContext.SaveChangesAsync();

        if (recordsAffected > 0)
        {
            return team;
        }
        return null;
    }

    public async Task<bool> DeleteTeamAsync(Guid? teamId)
    {
        if (teamId == null || teamId == Guid.Empty) throw new ArgumentNullException("Invalid TeamId. Delete operation failed");
        Team? existingTeam = await _dbContext.Teams.FirstOrDefaultAsync(team => team.Id == teamId);

        if (existingTeam == null) throw new ArgumentNullException("Teams not found. Delete operation failed.");

        _dbContext.Teams.Remove(existingTeam);
        int recordsAffected = await _dbContext.SaveChangesAsync();

        return (recordsAffected > 0);
    }

    public async Task<Team?> UpdateTeamAsync(Team team)
    {
        if (team == null) return null;
        if (team.Id == Guid.Empty) return null;

        Team? existingTeam = await _dbContext.Teams.FirstOrDefaultAsync(t => t.Id == team.Id);

        if (existingTeam == null) return null;

        existingTeam.Name = team.Name;
        existingTeam.OrganizationId = team.OrganizationId;
        existingTeam.UpdatedOn = team.UpdatedOn;
        existingTeam.UpdatedBy = team.UpdatedBy;

        int rowsAffected = await _dbContext.SaveChangesAsync();

        if (rowsAffected > 0)
        {
            return existingTeam;
        }
        return null;
    }
}
