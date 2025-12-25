using ZenPlanner.Domain.Common;
using ZenPlanner.Domain.Enums;
using System.Collections.Generic; // Cần dùng thư viện này cho List
using System.Linq;

// Định nghĩa rõ: Khi dùng "DomainTaskStatus", nó là Enum của chúng ta
using DomainTaskStatus = ZenPlanner.Domain.Enums.TaskStatus;

namespace ZenPlanner.Domain.Entities;

public class TaskItem : BaseEntity
{
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public DateTime? DueDate { get; private set; }
    public TaskPriority Priority { get; private set; }

    public DomainTaskStatus Status { get; private set; } // Enum trạng thái công việc

    private readonly List<SubTask> _subTasks = new();

    // Public property: Bên ngoài chỉ được xem (Read-Only), không được Add() bừa bãi
    public virtual IReadOnlyCollection<SubTask> SubTasks => _subTasks.AsReadOnly();

    private TaskItem() { }

    public TaskItem(string title, string? description, DateTime? dueDate, TaskPriority priority)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title of the work cannot  be empty");
        }

        Title = title;
        Description = description;
        DueDate = dueDate;
        Priority = priority;
        Status = DomainTaskStatus.Todo; // Dùng Alias
    }

    // --- DOMAIN BEHAVIORS ---

    public void UpdateDetails(string title, string? description, TaskPriority priority)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title required");
        Title = title;
        Description = description;
        Priority = priority;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsCompleted()
    {
        Status = DomainTaskStatus.Completed;
        UpdatedAt = DateTime.UtcNow;

        // Logic nghiệp vụ: Khi task cha xong, có muốn đánh dấu hết task con xong không?
        // Tùy nghiệp vụ, ở đây ta tạm giữ nguyên task con.
    }

    public void ReopenTask()
    {
        Status = DomainTaskStatus.Todo;
        UpdatedAt = DateTime.UtcNow;
    }

    // Hàm thêm SubTask an toàn
    public void AddSubTask(string title)
    {
        var subTask = new SubTask(title, this.Id);
        _subTasks.Add(subTask);
    }
}