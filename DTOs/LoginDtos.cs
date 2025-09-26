
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class LoginDtos
{
    [Required]
    public string Email { get; set; } = "";
    [Required]
    public string password { get; set; } = "";
}   