
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterDtos
{
    [Required]
    public string DisplayName { get; set; } = "";

    [EmailAddress]
    public string Email { get; set; } = "";
    [Phone]
    public string PhoneNumber { get; set; } = "";
    [Required]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    [DataType(DataType.Password)]
    public string password { get; set; } = "";
}   