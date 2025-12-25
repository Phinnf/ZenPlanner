using Microsoft.EntityFrameworkCore;
using ZenPlanner.Domain.Entities;

namespace ZenPlanner.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Khai báo các bảng trong Database
    public DbSet<TaskItem> TaskItems { get; set; }
    public DbSet<SubTask> SubTasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // --- CẤU HÌNH FLUENT API ---
        // (Clean Code: Tách biệt logic cấu hình khỏi Entity)

        // 1. Cấu hình cho TaskItem
        modelBuilder.Entity<TaskItem>(builder =>
        {
            builder.HasKey(t => t.Id);

            // Quan trọng: Chỉ định EF Core sử dụng backing field "_subTasks"
            // vì property "SubTasks" là IReadOnlyCollection (không set trực tiếp được)
            builder.Metadata.FindNavigation(nameof(TaskItem.SubTasks))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            // Cấu hình quan hệ 1-Nhiều: 1 Task có nhiều SubTasks
            builder.HasMany(t => t.SubTasks)
                   .WithOne(s => s.TaskItem)
                   .HasForeignKey(s => s.TaskItemId)
                   .OnDelete(DeleteBehavior.Cascade); // Xóa cha thì xóa luôn con
        });

        // 2. Cấu hình cho SubTask
        modelBuilder.Entity<SubTask>(builder =>
        {
            builder.HasKey(s => s.Id);
        });
    }
}