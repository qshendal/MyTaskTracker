using MyTaskTracker.Models;
using MyTaskTracker.Services;
using Xunit;

namespace MyTaskTracker.Tests;

public class TaskServiceTests
{
    [Fact]
    public void CreateTask_ShouldReturnCreatedTask()
    {
        var service = new TaskService();
        var dto = new CreateTaskDto("Сделать лабу");

        var result = service.Create(dto);

        Assert.NotNull(result);
        Assert.Equal("Сделать лабу", result.Title);
        Assert.False(result.IsCompleted);
    }

    [Fact]
    public void GetById_ShouldReturnTask_WhenIdExists()
    {
        var service = new TaskService();
        var dto = new CreateTaskDto("Тестовая задача");
        var createdTask = service.Create(dto);

        var foundTask = service.GetById(createdTask.Id);

        Assert.NotNull(foundTask);
        Assert.Equal(createdTask.Id, foundTask.Id);
    }

    [Fact]
    public void GetById_ShouldReturnNull_WhenIdDoesNotExist()
    {
        var service = new TaskService();
        var randomId = Guid.NewGuid();

        var result = service.GetById(randomId);

        Assert.Null(result);
    }

    [Fact]
    public void Delete_ShouldRemoveTask()
    {
        var service = new TaskService();
        var createdTask = service.Create(new CreateTaskDto("Задача на удаление"));

        var isDeleted = service.Delete(createdTask.Id);

        Assert.True(isDeleted);

        var checkTask = service.GetById(createdTask.Id);
        Assert.Null(checkTask);
    }
}