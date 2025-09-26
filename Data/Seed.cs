using System.Text.Json;
using API.DTOs;
using API.Entities;

namespace API.Data;

public class Seed
{
    public static async Task SeedUsers(AppDbContext context)
    {
        if (context.Users.Any()) return;

        var memberData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
        var members = JsonSerializer.Deserialize<List<SeedUserDtos>>(memberData);
        if (members == null)
        {
            Console.WriteLine("No user data found to seed.");
            return;
        }
        int index = 1;
        foreach (var member in members)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA256();
            var user = new User
            {
                PhoneNumber = "9462025166",
                Id = member.Id,
                DisplayName = member.DisplayName.ToLower(),
                Email = member.Email,
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("Pa$$w0rd")),
                passwordSalt = hmac.Key,
                Members = new Members
                {
                    Id = member.Id,
                    DisplayName = member.DisplayName,
                    Created = member.Created,
                    DateOfBirth = member.DateOfBirth,
                    LastActive = member.LastActive,
                    City = member.City,
                    Country = member.Country,
                    Description = member.Description,
                    ImageUrl = member.ImageUrl,
                    Gender = member.Gender
                }
            };
            user.Members.Photos.Add(new Photo
            {
                Id =index.ToString(),
                Url = member.ImageUrl ?? "",
                MembersId = member.Id
            });
            context.Users.Add(user);
            index++; 
        }
       
        await context.SaveChangesAsync();
    }
}