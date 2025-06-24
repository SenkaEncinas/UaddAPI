namespace UaddAPI.Dto.Internship;

public class InternshipDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Company { get; set; } = null!;
    public string Location { get; set; } = null!;
    public DateTime PublishDate { get; set; }
    public string Category { get; set; } = null!;
}