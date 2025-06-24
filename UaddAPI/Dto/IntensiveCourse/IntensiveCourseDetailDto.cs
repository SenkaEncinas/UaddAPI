namespace UaddAPI.Dto.IntensiveCourse;

public class IntensiveCourseDetailDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Instructor { get; set; } = null!;
    public string Modality { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public string WhatsAppLink { get; set; } = null!;
    public DateTime PublishDate { get; set; }
    public string Category { get; set; } = null!;
}