using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;
using MyFirstApi.Models;
using System.Linq;

namespace MyFirstApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private static readonly List<User> Users = new()
    {
        new User{Id = 1, Name = "Дима", Email = "dima@gmail.com"},
        new User{Id = 2, Name = "Катя", Email = "katya@gmail.com"}
    };
    
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(Users);
    }

    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        var user = Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound("Пользвоатель не найден");
        }
        
        return Ok(user);
    }

    [HttpPost]
    public IActionResult Create([FromBody] User newUser)
    {
        if (newUser.Name == "Кирилл")
        {
            return BadRequest(new{Message = "Нельзя добавить пользвоателя с таким именем"});
        }
        newUser.Id = Users.Any() ? Users.Max(u => u.Id) + 1 : 1;
        
        Users.Add(newUser);
        
        return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] User updateUser)
    {
        var existingUser = Users.FirstOrDefault(u=>u.Id == id);
        
        if (existingUser == null)
        {
            return NotFound("Пользвоатель не найден");
        }

        existingUser.Name = updateUser.Name;
        existingUser.Email = updateUser.Email;
        
        return Ok(existingUser);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var user = Users.FirstOrDefault(u=> u.Id == id);
        
        if (user == null)
        {
            return NotFound("Пользвоатель не найден");
        }
        
        Users.Remove(user);
        
        return NoContent();
    }
}