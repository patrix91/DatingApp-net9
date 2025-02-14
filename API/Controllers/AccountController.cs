using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountController(DataContext dataContext) : BaseApiController
{
    [HttpPost("register")] // account/register
    public async Task<ActionResult<AppUser>> Register(string userName, string password){

        using var hmac = new HMACSHA512();
        var user = new AppUser{
            UserName = userName,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
            PasswordSalt = hmac.Key
        };

        dataContext.Users.Add(user);
        await dataContext.SaveChangesAsync();
        return Ok(dataContext);
    }

    
}
