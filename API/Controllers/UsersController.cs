using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using API.Entities;
using API.Data;

namespace API.Controllers;

public class UsersController(DataContext context) : BaseApiController
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