using System.ComponentModel.DataAnnotations;
using UaddAPI.Models;

namespace UaddAPI.Dto.User;

public class UserRegisterDto
{
    [Required]
    public string Username { get; set; } = null!;

    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}