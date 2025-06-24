using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UaddAPI.Models;

public class NewsPost
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Title { get; set; } = null!;

    [Required, MaxLength(500)]
    public string Description { get; set; } = null!;

    [Required, MaxLength(100)]
    public string Location { get; set; } = null!;

    [Required, MaxLength(50)]
    public string Condition { get; set; } = null!;

    [Required, MaxLength(50)]
    public string PaymentMethod { get; set; } = null!;

    [Required]
    public double Price { get; set; }

    [Required]  
    public string ImageUrl { get; set; } = null!;

    [Required]
    public DateTime PublishDate { get; set; }

    [Required, MaxLength(20)]
    public string PhoneNumber { get; set; } = null!;

    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string WhatsAppLink { get; set; } = null!;

    [Required]
    public int AuthorId { get; set; }

    [JsonIgnore]
    public User Author { get; set; } = null!;
    
    public string Category { get; set; } = null!;
}