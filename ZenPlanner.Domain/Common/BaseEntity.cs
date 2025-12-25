namespace ZenPlanner.Domain.Common;

public abstract class BaseEntity
{
    // Sử dụng Guid để tạo ID ngẫu nhiên, bảo mật hơn số thứ tự (int)
    public Guid Id { get; set; } = Guid.NewGuid();

    // Ngày tạo bản ghi (UTC để chuẩn hóa thời gian quốc tế)
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Ngày cập nhật gần nhất
    public DateTime? UpdatedAt { get; set; }
}