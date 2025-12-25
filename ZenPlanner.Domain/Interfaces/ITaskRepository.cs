using ZenPlanner.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZenPlanner.Domain.Interfaces;

public interface ITaskRepository
{
    // Lấy 1 task theo ID
    Task<TaskItem?> GetByIdAsync(Guid id);

    // Lấy danh sách task (có thể filter sau này)
    Task<IEnumerable<TaskItem>> GetAllAsync();

    // Thêm mới
    Task AddAsync(TaskItem task);

    // Xóa
    Task DeleteAsync(TaskItem task);

    // Lưu thay đổi vào DB (Unit of Work)
    Task SaveChangesAsync();
}