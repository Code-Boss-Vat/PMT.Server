using PMT.Core.Entities;
namespace PMT.Core.RepositoryContracts;

public interface ITeamsRepository
{
    Task<Team?> AddTeamAsync(Team team);
    Task<Team?> UpdateTeamAsync(Team team);
    Task<bool> DeleteTeamAsync(Guid? teamId);
}

