using System.ComponentModel.DataAnnotations;

namespace UaddAPI.Dto.Internship;

public class InternshipCreateDto
{
    [Required, MaxLength(100)]
    public string Title { get; set; } = null!;

    [Required, MaxLength(500)]
    public string Description { get; set; } = null!;

    [Required, MaxLength(100)]
    public string Company { get; set; } = null!;

    [Required, MaxLength(100)]
    public string Location { get; set; } = null!;

    [Required]
    public string WhatsAppLink { get; set; } = null!;

    [Required]
    public string Category { get; set; } = null!;
}