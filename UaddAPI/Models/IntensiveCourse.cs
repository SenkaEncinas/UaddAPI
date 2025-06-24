using System.ComponentModel.DataAnnotations;

namespace UaddAPI.Models;

public class IntensiveCourse
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Title { get; set; } = null!;

    [Required, MaxLength(500)]
    public string Description { get; set; } = null!;

    [Required, MaxLength(100)]
    public string Instructor { get; set; } = null!;

    [Required, MaxLength(50)]
    public string Modality { get; set; } = null!; // Presencial, Virtual, etc.
    
    [Required]
    public DateTime StartDate { get; set; }
    
    [Required]
    public string WhatsAppLink { get; set; } = null!;

    [Required]
    public DateTime PublishDate { get; set; }
    
    public string Category { get; set; } = null!;
}

