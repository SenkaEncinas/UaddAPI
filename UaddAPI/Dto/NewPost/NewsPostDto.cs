namespace UaddAPI.Dto.NewsPost;

public class NewsPostDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string Condition { get; set; } = null!;
    public string PaymentMethod { get; set; } = null!;
    public double Price { get; set; }
    public string ImageUrl { get; set; } = null!;
    public DateTime PublishDate { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string WhatsAppLink { get; set; } = null!;
    public string Category { get; set; }
    
}