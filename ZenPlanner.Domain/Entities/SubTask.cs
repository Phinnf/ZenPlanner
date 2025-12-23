using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZenPlanner.Domain.Entities
{
    public class SubTask
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        public bool IsCompleted { get; set; } = false;

        public int EstimatedDurationMinutes { get; set; }

        // Khóa ngoại
        public int TaskItemId { get; set; }

        [ForeignKey("TaskItemId")]
        public TaskItem? TaskItem { get; set; }
    }
}