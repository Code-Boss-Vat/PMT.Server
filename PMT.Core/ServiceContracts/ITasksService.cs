using ApplicationCore.DTOs;
using PMT.Core.DTOs;

namespace PMT.Core.ServiceContracts;

public interface ITasksService
{
    Task<List<ProjectTaskResponse?>> GetAllTasksAsync();
    Task<ProjectTaskResponse?> GetTaskByIdAsync(Guid? taskId);
    Task<OperationResult<ProjectTaskResponse?>> CreateTaskAsync(ProjectTaskCreateRequest projectTaskCreateRequest);
    Task<OperationResult<ProjectTaskResponse?>> UpdateTaskByIdAsync(ProjectTaskUpdateRequest projectTaskUpdateRequest);
    Task<bool> DeleteTaskByIdAsync(Guid taskId);
}
