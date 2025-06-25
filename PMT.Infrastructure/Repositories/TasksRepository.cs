using Microsoft.EntityFrameworkCore;
using PMT.Core.Entities;
using PMT.Core.RepositoryContracts;
using PMT.Infrastructure.Context;

namespace PMT.Infrastructure.Repositories;

public class TasksRepository(ApplicationDbContext _dbContext) : ITasksRepository
{
    public async Task<ProjectTask?> CreateTaskAsync(ProjectTask projectTask)
    {
        projectTask.Id = Guid.NewGuid();
        _dbContext.Tasks.Add(projectTask);

        int recordsAffected = await _dbContext.SaveChangesAsync();

        return recordsAffected > 0 ? projectTask : null;
    }

    public async Task<bool> DeleteTaskByIdAsync(Guid taskId)
    {
        if (taskId == Guid.Empty) return false;

        ProjectTask? task = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == taskId);

        if (task == null) return false;

        _dbContext.Tasks.Remove(task);
        int recordsAffected = await _dbContext.SaveChangesAsync();

        return (recordsAffected > 0);
    }

    public async Task<List<ProjectTask?>> GetAllTasksAsync()
    {
        return await _dbContext.Tasks.ToListAsync();
    }

    public async Task<ProjectTask?> GetTaskByIdAsync(Guid taskId)
    {
        if (taskId == Guid.Empty) return null;

        ProjectTask? projectTask = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == taskId);

        return projectTask;
    }

    public async Task<ProjectTask?> UpdateTaskAsync(ProjectTask projectTask)
    {
        ProjectTask existingTask = (await GetTaskByIdAsync(projectTask.Id))!;

        existingTask.Name = projectTask.Name;
        existingTask.ProjectId = projectTask.ProjectId;
        existingTask.TeamId = projectTask.TeamId;
        existingTask.Status = projectTask.Status;
        existingTask.ScheduledEndDate = projectTask.ScheduledEndDate;
        existingTask.CompletionDate = projectTask.CompletionDate;
        existingTask.UpdatedBy = projectTask.UpdatedBy;
        existingTask.UpdatedOn = projectTask.UpdatedOn;

        int recordsAffected = await _dbContext.SaveChangesAsync();

        return recordsAffected > 0 ? projectTask : null;
    }
}
