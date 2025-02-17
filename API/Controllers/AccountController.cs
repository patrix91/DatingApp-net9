using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using API.Entities;
using System.Text;
using API.Data;
using API.DTO;

namespace API.Controllers;

public class AccountController(DataContext dataContext) : BaseApiController
{
    [HttpPost("register")] // account/register
    public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
    {
        if(await UserExists(registerDto.Username)) return BadRequest("Uzytkownik o podaj nazwie już istnieje.");

        using var hmac = new HMACSHA512();
        var user = new AppUser{
            UserName = registerDto.Username.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };

        dataContext.Users.Add(user);
        await dataContext.SaveChangesAsync();
        return user;
    }


    private async Task<bool> UserExists(string username)
    {
        return await dataContext.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
    }    

    [HttpPost("login")]
    public async Task<ActionResult<AppUser>> Login(LoginDto loginDto)
    {
        var user = await dataContext.Users.FirstOrDefaultAsync(x => 
        x.UserName == loginDto.UserName.ToLower());

        if(user == null) return Unauthorized("Niepoprawna nazwa użytkownika.");

        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for(int i = 0; i < computedHash.Length; i++)
        {
            if  (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Niepoprawne hasło.");
        }
        return user;
    }
}
