
using Microsoft.EntityFrameworkCore;
using ZenPlanner.Application.Interfaces;
using ZenPlanner.Application.Services;
using ZenPlanner.Domain.Interfaces;
using ZenPlanner.Infrastructure.Data;
using ZenPlanner.Infrastructure.Repositories;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var builder = WebApplication.CreateBuilder(args);

// 2. CẤU HÌNH DATABASE
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

// 3. ĐĂNG KÝ SERVICES (DI)
builder.Services.AddControllers();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

// 4. CẤU HÌNH OPENAPI (Swagger)
builder.Services.AddOpenApi();
var app = builder.Build();

// 5. CẤU HÌNH MIDDLEWARE
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); 
}

app.UseHttpsRedirection();
app.UseAuthorization();

// 6. MAP CONTROLLERS (QUAN TRỌNG ĐỂ KHÔNG BỊ LỖI 404)
app.MapControllers();

app.Run();