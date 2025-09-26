using System.Security.Cryptography;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers;

public class AccountController(AppDbContext context,ITokenServices tokenServices) : BaseApiController
{
    [HttpPost("register")]
    public async Task<UserDtos> Register(RegisterDtos registerDtos)
    {
        if (await context.Users.AnyAsync(u => u.Email == registerDtos.Email))
        {
            throw new Exception("Email is already taken.");
        }
        using var hmac = new HMACSHA256();
        var user = new User
        {
            DisplayName = registerDtos.DisplayName,
            Email = registerDtos.Email,
            PhoneNumber = registerDtos.PhoneNumber,
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(registerDtos.password)),
            passwordSalt = hmac.Key
        };
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user.ToDto(tokenServices);
    }
    [HttpPost("login")]
    public async Task<UserDtos> Login(LoginDtos loginDtos)
    {
        var user = await context.Users.SingleOrDefaultAsync(u => u.Email == loginDtos.Email);
        if (user == null)
        {
            throw new Exception("Invalid email.");
        }
        using var hmac = new HMACSHA256(user.passwordSalt);
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(loginDtos.password));
        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.passwordHash[i])
            {
                throw new Exception("Invalid password.");
            }
        }

        return user.ToDto(tokenServices);
    }   
}