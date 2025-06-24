using System.ComponentModel.DataAnnotations;

namespace UaddAPI.Dto.User;

public class UserInfoDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    [EmailAddress]
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
}