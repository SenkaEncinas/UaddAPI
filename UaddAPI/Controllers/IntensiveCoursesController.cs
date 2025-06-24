using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UaddAPI.Data;
using UaddAPI.Dto.IntensiveCourse;
using UaddAPI.Models;

namespace UaddAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IntensiveCoursesController : ControllerBase
{
    private readonly UaddDbContext _context;

    public IntensiveCoursesController(UaddDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IntensiveCourseDto>>> GetAll()
    {
        var courses = await _context.IntensiveCourses
            .Select(c => new IntensiveCourseDto
            {
                Id = c.Id,
                Title = c.Title,
                Instructor = c.Instructor,
                Modality = c.Modality,
                PublishDate = c.PublishDate,
                Category = c.Category
            })
            .ToListAsync();

        return Ok(courses);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IntensiveCourseDetailDto>> Get(int id)
    {
        var course = await _context.IntensiveCourses.FindAsync(id);

        if (course == null)
            return NotFound();

        var dto = new IntensiveCourseDetailDto
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description,
            Instructor = course.Instructor,
            Modality = course.Modality,
            StartDate = course.StartDate,
            WhatsAppLink = course.WhatsAppLink,
            PublishDate = course.PublishDate,
            Category = course.Category
        };

        return Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Admin_IntensiveCourse")]
    public async Task<ActionResult> Create(IntensiveCourseCreateDto dto)
    {
        var course = new IntensiveCourse
        {
            Title = dto.Title,
            Description = dto.Description,
            Instructor = dto.Instructor,
            Modality = dto.Modality,
            StartDate = dto.StartDate,
            WhatsAppLink = dto.WhatsAppLink,
            PublishDate = DateTime.UtcNow,
            Category = dto.Category
        };

        _context.IntensiveCourses.Add(course);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = course.Id }, null);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Admin_IntensiveCourse")]
    public async Task<ActionResult> Update(int id, IntensiveCourseCreateDto dto)
    {
        var course = await _context.IntensiveCourses.FindAsync(id);

        if (course == null)
            return NotFound();

        course.Title = dto.Title;
        course.Description = dto.Description;
        course.Instructor = dto.Instructor;
        course.Modality = dto.Modality;
        course.StartDate = dto.StartDate;
        course.WhatsAppLink = dto.WhatsAppLink;
        course.Category = dto.Category;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Admin_IntensiveCourse")]
    public async Task<ActionResult> Delete(int id)
    {
        var course = await _context.IntensiveCourses.FindAsync(id);

        if (course == null)
            return NotFound();

        _context.IntensiveCourses.Remove(course);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
