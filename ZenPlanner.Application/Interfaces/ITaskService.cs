using ZenPlanner.Application.DTOs;
using ZenPlanner.Domain.Entities;

namespace ZenPlanner.Application.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskItem>> GetAllTasksAsync();
    Task<TaskItem?> GetTaskByIdAsync(Guid id);
    Task<TaskItem> CreateTaskAsync(CreateTaskRequest request);
    Task UpdateTaskAsync(Guid id, UpdateTaskRequest request);
    Task DeleteTaskAsync(Guid id);
}