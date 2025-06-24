using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UaddAPI.Data;
using UaddAPI.Dto.Internship;
using UaddAPI.Models;

namespace UaddAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InternshipsController : ControllerBase
{
    private readonly UaddDbContext _context;

    public InternshipsController(UaddDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InternshipDto>>> GetAll()
    {
        var internships = await _context.Internships
            .Select(i => new InternshipDto
            {
                Id = i.Id,
                Title = i.Title,
                Company = i.Company,
                Location = i.Location,
                PublishDate = i.PublishDate,
                Category = i.Category
            })
            .ToListAsync();

        return Ok(internships);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InternshipDetailDto>> Get(int id)
    {
        var internship = await _context.Internships.FindAsync(id);

        if (internship == null)
            return NotFound();

        var dto = new InternshipDetailDto
        {
            Id = internship.Id,
            Title = internship.Title,
            Description = internship.Description,
            Company = internship.Company,
            Location = internship.Location,
            WhatsAppLink = internship.WhatsAppLink,
            PublishDate = internship.PublishDate,
            Category = internship.Category
        };

        return Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Admin_Internship")]
    public async Task<ActionResult> Create(InternshipCreateDto dto)
    {
        var internship = new Internship
        {
            Title = dto.Title,
            Description = dto.Description,
            Company = dto.Company,
            Location = dto.Location,
            WhatsAppLink = dto.WhatsAppLink,
            PublishDate = DateTime.UtcNow,
            Category = dto.Category
        };

        _context.Internships.Add(internship);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = internship.Id }, null);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Admin_Internship")]
    public async Task<ActionResult> Update(int id, InternshipCreateDto dto)
    {
        var internship = await _context.Internships.FindAsync(id);

        if (internship == null)
            return NotFound();

        internship.Title = dto.Title;
        internship.Description = dto.Description;
        internship.Company = dto.Company;
        internship.Location = dto.Location;
        internship.WhatsAppLink = dto.WhatsAppLink;
        internship.Category = dto.Category;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Admin_Internship")]
    public async Task<ActionResult> Delete(int id)
    {
        var internship = await _context.Internships.FindAsync(id);

        if (internship == null)
            return NotFound();

        _context.Internships.Remove(internship);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
