using PMT.Core.Entities;

namespace PMT.Core.RepositoryContracts;

public interface ITasksRepository
{
    Task<List<ProjectTask?>> GetAllTasksAsync();
    Task<ProjectTask?> GetTaskByIdAsync(Guid taskId);
    Task<ProjectTask?> CreateTaskAsync(ProjectTask projectTask);
    Task<ProjectTask?> UpdateTaskAsync(ProjectTask projectTask);
    Task<bool> DeleteTaskByIdAsync(Guid taskId);
}
