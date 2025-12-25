using ZenPlanner.Application.DTOs;
using ZenPlanner.Application.Interfaces;
using ZenPlanner.Domain.Entities;
using ZenPlanner.Domain.Interfaces;

namespace ZenPlanner.Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<TaskItem?> GetTaskByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<TaskItem> CreateTaskAsync(CreateTaskRequest request)
    {
        // 1. Chuyển đổi từ DTO sang Entity (Mapping)
        var newTask = new TaskItem(
            request.Title,
            request.Description,
            request.DueDate,
            request.Priority
        );

        // 2. Gọi Repository để lưu
        await _repository.AddAsync(newTask);
        await _repository.SaveChangesAsync();

        return newTask;
    }

    public async Task UpdateTaskAsync(Guid id, UpdateTaskRequest request)
    {
        // 1. Tìm task cũ
        var task = await _repository.GetByIdAsync(id);
        if (task == null)
        {
            throw new KeyNotFoundException("Cant find this job");
        }

        // 2. Cập nhật thông tin (Gọi phương thức của Domain - Clean Code)
        task.UpdateDetails(request.Title, request.Description, request.Priority);

        // 3. Lưu lại
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteTaskAsync(Guid id)
    {
        var task = await _repository.GetByIdAsync(id);
        if (task != null)
        {
            await _repository.DeleteAsync(task);
            await _repository.SaveChangesAsync();
        }
    }
}