using ZenPlanner.Domain.Common;

namespace ZenPlanner.Domain.Entities;

public class SubTask : BaseEntity
{
    public string Title { get; private set; }
    public bool IsCompleted { get; private set; }

    // Foreign Key (Khóa ngoại)
    public Guid TaskItemId { get; private set; }

    // Navigation Property (Để Entity Framework hiểu mối quan hệ)
    public virtual TaskItem TaskItem { get; private set; }

    private SubTask() { } // Constructor cho EF Core

    public SubTask(string title, Guid taskItemId)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Subtask cannot be empty");

        Title = title;
        TaskItemId = taskItemId;
        IsCompleted = false;
    }

    public void MarkComplete(bool completed)
    {
        IsCompleted = completed;
    }
}