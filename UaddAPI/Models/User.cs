using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UaddAPI.Models;

public enum Role
{
    Admin = 0,
    User = 1,
    Admin_Evento = 2,
    Admin_Matchs = 3,
    Admin_Post = 4,
    Admin_Internship = 5,
    Admin_IntensiveCourse = 6
}


public class User
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Username { get; set; } = null!;

    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public byte[] PasswordHash { get; set; } = null!;

    [Required]
    public byte[] PasswordSalt { get; set; } = null!;

    [Required]
    public Role Role { get; set; } = Role.User;

    [JsonIgnore]
    public List<NewsPost> NewsPosts { get; set; } = [];
}