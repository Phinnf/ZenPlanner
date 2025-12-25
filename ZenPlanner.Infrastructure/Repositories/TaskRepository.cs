using Microsoft.EntityFrameworkCore;
using ZenPlanner.Domain.Entities;
using ZenPlanner.Domain.Interfaces;
using ZenPlanner.Infrastructure.Data;

namespace ZenPlanner.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id)
    {
        // Include(t => t.SubTasks): Kèm theo cả các task con
        return await _context.TaskItems
            .Include(t => t.SubTasks)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        return await _context.TaskItems
            .Include(t => t.SubTasks)
            .OrderByDescending(t => t.CreatedAt) // Mới nhất lên đầu
            .ToListAsync();
    }

    public async Task AddAsync(TaskItem task)
    {
        await _context.TaskItems.AddAsync(task);
        // Lưu ý: Chưa gọi SaveChanges ở đây để đảm bảo Transaction nếu cần
    }

    public async Task DeleteAsync(TaskItem task)
    {
        _context.TaskItems.Remove(task);
        // Dùng Task.CompletedTask vì hàm Remove của EF là đồng bộ
        await Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}