namespace MyTaskTracker.Services;

using System.Collections.Concurrent;
using MyTaskTracker.Models;

public class TaskService : ITaskService
{
    private readonly ConcurrentDictionary<Guid, TaskItem> _tasks = new();
    
    public IEnumerable<TaskItem> GetAll() => _tasks.Values;

    public TaskItem? GetById(Guid id)
    {
        _tasks.TryGetValue(id, out var task);
        return task;
    }

    public TaskItem Create(CreateTaskDto dto)
    {
        var task = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            IsCompleted = false,
            CreatedAt = DateTime.Now
        };
        
        _tasks[task.Id] = task;
        return task;
    }

    public bool Update(Guid id, UpdateTaskDto dto)
    {
        if(!_tasks.TryGetValue(id, out var existingTask))
        {
            return false;
        }

        existingTask.Title = dto.Title;
        existingTask.IsCompleted = dto.IsCompleted;
        return  true;
    }
    
    public bool Delete(Guid id) => _tasks.TryRemove(id, out _);
    
}