namespace UaddAPI.Dto.Internship;

public class InternshipDetailDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Company { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string WhatsAppLink { get; set; } = null!;
    public DateTime PublishDate { get; set; }
    public string Category { get; set; } = null!;
}