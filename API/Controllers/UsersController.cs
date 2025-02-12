using Microsoft.AspNetCore.Mvc;
using API.Entities;
using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(DataContext context) : ControllerBase
{
    // private readonly DataContext context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await context.Users.ToListAsync();
        return Ok(users);
    }

    [HttpGet("{id:int}")] //api/users/1
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null){
            return NotFound();
        }
        else{
            return user;
        }
    }
}