using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UaddAPI.Data;
using UaddAPI.Dto.NewsPost;
using UaddAPI.Models;

namespace UaddAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NewsPostController : ControllerBase
{
    private readonly UaddDbContext _context;

    public NewsPostController(UaddDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<NewsPostDto>>> GetAll()
    {
        var posts = await _context.NewsPosts
            .Select(p => new NewsPostDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Location = p.Location,
                Condition = p.Condition,
                PaymentMethod = p.PaymentMethod,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                PublishDate = p.PublishDate,
                PhoneNumber = p.PhoneNumber,
                Email = p.Email,
                WhatsAppLink = p.WhatsAppLink,
                Category = p.Category
            })
            .ToListAsync();

        return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NewsPostDetailDto>> Get(int id)
    {
        var post = await _context.NewsPosts
            .Include(p => p.Author)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (post == null) return NotFound();

        var dto = new NewsPostDetailDto
        {
            Title = post.Title,
            Price = post.Price,
            Location = post.Location,
            Condition = post.Condition,
            PaymentMethod = post.PaymentMethod,
            Description = post.Description,
            PhoneNumber = post.PhoneNumber,
            Email = post.Email,
            WhatsAppLink = post.WhatsAppLink,
            ImageUrl = post.ImageUrl,
            PublishDate = post.PublishDate,
            Category = post.Category
        };

        return Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Admin_Post")]
    public async Task<ActionResult<NewsPostDto>> Create(NewsPostCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = int.Parse(User.Identity!.Name!);

        var post = new NewsPost
        {
            Title = dto.Title,
            Description = dto.Description,
            Location = dto.Location,
            Condition = dto.Condition,
            PaymentMethod = dto.PaymentMethod,
            Price = dto.Price,
            ImageUrl = dto.ImageUrl,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            WhatsAppLink = dto.WhatsAppLink,
            PublishDate = DateTime.UtcNow,
            AuthorId = userId,
            Category = dto.Category
        };

        _context.NewsPosts.Add(post);
        await _context.SaveChangesAsync();

        var result = new NewsPostDto
        {
            Id = post.Id,
            Title = post.Title,
            Description = post.Description,
            Location = post.Location,
            Condition = post.Condition,
            PaymentMethod = post.PaymentMethod,
            Price = post.Price,
            ImageUrl = post.ImageUrl,
            PublishDate = post.PublishDate,
            PhoneNumber = post.PhoneNumber,
            Email = post.Email,
            WhatsAppLink = post.WhatsAppLink,
            Category = post.Category
        };

        return CreatedAtAction(nameof(Get), new { id = post.Id }, result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Admin_Post")]
    public async Task<IActionResult> Update(int id, NewsPostCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var post = await _context.NewsPosts.FirstOrDefaultAsync(p => p.Id == id);
        if (post == null) return NotFound();

        post.Title = dto.Title;
        post.Description = dto.Description;
        post.Location = dto.Location;
        post.Condition = dto.Condition;
        post.PaymentMethod = dto.PaymentMethod;
        post.Price = dto.Price;
        post.ImageUrl = dto.ImageUrl;
        post.PhoneNumber = dto.PhoneNumber;
        post.Email = dto.Email;
        post.WhatsAppLink = dto.WhatsAppLink;
        post.Category = dto.Category;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Admin_Post")]
    public async Task<IActionResult> Delete(int id)
    {
        var post = await _context.NewsPosts.FirstOrDefaultAsync(p => p.Id == id);
        if (post == null) return NotFound();

        var userId = int.Parse(User.Identity!.Name!);
        if (post.AuthorId != userId && !User.IsInRole("Admin"))
            return Forbid();

        _context.NewsPosts.Remove(post);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("mine")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<NewsPostDto>>> GetMyPosts()
    {
        var userId = int.Parse(User.Identity!.Name!);

        var posts = await _context.NewsPosts
            .Where(p => p.AuthorId == userId)
            .Select(p => new NewsPostDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Location = p.Location,
                Condition = p.Condition,
                PaymentMethod = p.PaymentMethod,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                PublishDate = p.PublishDate,
                PhoneNumber = p.PhoneNumber,
                Email = p.Email,
                WhatsAppLink = p.WhatsAppLink,
                Category = p.Category
            })
            .ToListAsync();

        return Ok(posts);
    }
}
