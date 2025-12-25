using Microsoft.AspNetCore.Mvc;
using ZenPlanner.Application.DTOs;
using ZenPlanner.Application.Interfaces;
using ZenPlanner.Domain.Entities;

namespace ZenPlanner.API.Controllers;

[ApiController] // Attribute này giúp tự động validate dữ liệu đầu vào
[Route("api/[controller]")] // Đường dẫn sẽ là: api/tasks
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    // Inject Service vào Controller (Không inject Repository vào đây nhé!)
    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    // GET: api/tasks
    // Lấy danh sách tất cả công việc
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetAll()
    {
        var tasks = await _taskService.GetAllTasksAsync();
        return Ok(tasks); // Trả về HTTP 200 kèm dữ liệu
    }

    // GET: api/tasks/{id}
    // Lấy chi tiết 1 công việc
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItem>> GetById(Guid id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);
        if (task == null)
        {
            return NotFound(new { message = "Can't find this job" }); // HTTP 404
        }
        return Ok(task);
    }

    // POST: api/tasks
    // Tạo mới công việc
    [HttpPost]
    public async Task<ActionResult<TaskItem>> Create(CreateTaskRequest request)
    {
        var createdTask = await _taskService.CreateTaskAsync(request);

        // Trả về HTTP 201 Created và header Location trỏ đến link lấy chi tiết
        return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
    }

    // PUT: api/tasks/{id}
    // Cập nhật công việc
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateTaskRequest request)
    {
        try
        {
            await _taskService.UpdateTaskAsync(id, request);
            return NoContent(); // HTTP 204: Thành công nhưng không trả về dữ liệu gì
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = "Không tìm thấy công việc để cập nhật." });
        }
    }

    // DELETE: api/tasks/{id}
    // Xóa công việc
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _taskService.DeleteTaskAsync(id);
        return NoContent(); // HTTP 204
    }
}