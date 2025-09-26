using System;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
namespace API.Entities;

public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public string DisplayName { get; set; } = "";

    [EmailAddress]
    public string Email { get; set; } = "";
    public string PhoneNumber { get; set; } = "";

    public string? ImageUrl { get; set; }
    public byte[] passwordHash { get; set; } = Array.Empty<byte>();
    public byte[] passwordSalt { get; set; } = Array.Empty<byte>();
    
    public Members Members { get; set; } = null!;

}