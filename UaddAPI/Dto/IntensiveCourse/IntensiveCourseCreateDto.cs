using System.ComponentModel.DataAnnotations;

namespace UaddAPI.Dto.IntensiveCourse;

public class IntensiveCourseCreateDto
{
    [Required, MaxLength(100)]
    public string Title { get; set; } = null!;

    [Required, MaxLength(500)]
    public string Description { get; set; } = null!;

    [Required, MaxLength(100)]
    public string Instructor { get; set; } = null!;

    [Required, MaxLength(50)]
    public string Modality { get; set; } = null!;

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public string WhatsAppLink { get; set; } = null!;

    [Required]
    public string Category { get; set; } = null!;
}