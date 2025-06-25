using ApplicationCore.DTOs;
using AutoMapper;
using PMT.Core.DTOs;
using PMT.Core.Entities;
using PMT.Core.Helpers;
using PMT.Core.RepositoryContracts;
using PMT.Core.ServiceContracts;

namespace PMT.Core.Services;

public class TasksService(ITasksRepository _tasksRepository, IMapper _mapper) : ITasksService
{
    public async Task<OperationResult<ProjectTaskResponse?>> CreateTaskAsync(ProjectTaskCreateRequest projectTaskCreateRequest)
    {
        OperationType operation = OperationType.Create;
        if (projectTaskCreateRequest == null)
        {
            return OperationResult.Failure<ProjectTaskResponse?>(operation, message: "Invalid task creation request!");
        }
        if (!ValidationHelper.TryValidate(projectTaskCreateRequest, out var validationResults))
        {
            return OperationResult.Failure<ProjectTaskResponse?>(operation, message: validationResults.First().ErrorMessage);
        }

        ProjectTask? taskToAdd = _mapper.Map<ProjectTask>(projectTaskCreateRequest);
        ProjectTask? addedTask = await _tasksRepository.CreateTaskAsync(taskToAdd);

        if (addedTask == null)
        {
            return OperationResult.Failure<ProjectTaskResponse?>(operation, message: "Task creation failed!");
        }

        ProjectTaskResponse? taskResponse = _mapper.Map<ProjectTaskResponse>(addedTask);

        return OperationResult.Success(taskResponse, operation, message: "Task successfully created.")!;
    }

    public async Task<bool> DeleteTaskByIdAsync(Guid taskId)
    {
        return await _tasksRepository.DeleteTaskByIdAsync(taskId);
    }

    public async Task<List<ProjectTaskResponse?>> GetAllTasksAsync()
    {
        List<ProjectTask?> tasks = await _tasksRepository.GetAllTasksAsync();

        return tasks.Select(t => _mapper.Map<ProjectTaskResponse?>(t)).ToList();
    }

    public async Task<ProjectTaskResponse?> GetTaskByIdAsync(Guid? taskId)
    {
        if (taskId == null) return null;

        ProjectTask? task = await _tasksRepository.GetTaskByIdAsync(taskId.Value);

        return _mapper.Map<ProjectTaskResponse?>(task);
    }

    public async Task<OperationResult<ProjectTaskResponse?>> UpdateTaskByIdAsync(ProjectTaskUpdateRequest projectTaskUpdateRequest)
    {
        OperationType operation = OperationType.Update;
        if (projectTaskUpdateRequest == null || projectTaskUpdateRequest.Id == Guid.Empty)
        {
            return OperationResult.Failure<ProjectTaskResponse?>(operation, message: "Invalid task update request!");
        }
        if (!ValidationHelper.TryValidate(projectTaskUpdateRequest, out var validationResults))
        {
            return OperationResult.Failure<ProjectTaskResponse?>(operation, message: validationResults.First().ErrorMessage);
        }

        ProjectTask? taskToUpdate = _mapper.Map<ProjectTask>(projectTaskUpdateRequest);
        ProjectTask? updatedTask = await _tasksRepository.UpdateTaskAsync(taskToUpdate);

        if (updatedTask == null)
        {
            return OperationResult.Failure<ProjectTaskResponse?>(operation, message: "Task update failed!");
        }

        ProjectTaskResponse? taskResponse = _mapper.Map<ProjectTaskResponse>(updatedTask);

        return OperationResult.Success(taskResponse, operation, message: "Task successfully updated.")!;
    }
}
