using Microsoft.AspNetCore.Mvc;

namespace MyTaskTracker.Controllers;
using MyTaskTracker.Models;
using MyTaskTracker.Services;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;
    
    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var tasks = _taskService.GetAll();
        return Ok(tasks);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var task = _taskService.GetById(id);
        if (task == null)
            return NotFound(new  { message = $"Задча с id: {id} не найдена"});
        return Ok(task);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateTaskDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
        {
            return BadRequest(new {message = "Заголовок не может быть пустым"});
        }
        var createdTask = _taskService.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] UpdateTaskDto dto)
    {
        var succes = _taskService.Update(id, dto);
        if (!succes)
        {
            return NotFound(new  { message = $"Задча с id: {id} не найдена"});
        }
        
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var succes = _taskService.Delete(id);
        if (!succes)
        {
            return NotFound(new  { message = $"Задча с id: {id} не найдена"});
        }
        
        return NoContent();
    }
}