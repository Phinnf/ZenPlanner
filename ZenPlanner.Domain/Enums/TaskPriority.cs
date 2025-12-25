namespace ZenPlanner.Domain.Enums;

public enum TaskPriority
{
    // Quan trọng & Khẩn cấp (Làm ngay)
    DoFirst = 1,

    // Quan trọng & Không khẩn cấp (Lên lịch)
    Schedule = 2,

    // Không quan trọng & Khẩn cấp (Giao việc)
    Delegate = 3,

    // Không quan trọng & Không khẩn cấp (Xóa/Làm sau)
    DontDo = 4
}