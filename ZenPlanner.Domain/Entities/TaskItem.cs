using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZenPlanner.Domain.Entities
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DueDate { get; set; }
        public DateTime? ScheduledTime { get; set; }

        // Ma trận Eisenhower
        public bool IsUrgent { get; set; } = false;
        public bool IsImportant { get; set; } = false;

        public bool IsCompleted { get; set; } = false;

        public bool IsAiGenerated { get; set; } = false;

        public ICollection<SubTask> SubTasks { get; set; } = new List<SubTask>();
    }
}