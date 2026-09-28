namespace MyTaskTracker.Services;

using MyTaskTracker.Models;

public interface ITaskService
{
    IEnumerable<TaskItem> GetAll();
    TaskItem? GetById(Guid id);
    TaskItem Create(CreateTaskDto dto);
    bool Update(Guid id, UpdateTaskDto dto);
    bool Delete(Guid id);
}