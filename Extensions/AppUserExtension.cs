using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.StaticAssets;

namespace API.Extensions;



public static class AppUserExtension
{
    public static UserDtos ToDto(this User user, ITokenServices tokenServices)
    {
        return new UserDtos
        {
            DisplayName = user.DisplayName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Id = user.Id,
            Token = tokenServices.CreateToken(user),
            ImageUrl = "",
        };
        
    }
}