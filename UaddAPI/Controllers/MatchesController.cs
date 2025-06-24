using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UaddAPI.Data;
using UaddAPI.Dto.Match;
using UaddAPI.Models;

namespace UaddAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchesController : ControllerBase
    {
        private readonly UaddDbContext _context;

        public MatchesController(UaddDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatchDto>>> GetAll()
        {
            var matches = await _context.Matches
                .Select(m => new MatchDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    TeamA = m.TeamA,
                    TeamB = m.TeamB,
                    MatchDate = m.MatchDate,
                    Location = m.Location,
                    SportType = m.SportType,
                    ImageUrl = m.ImageUrl,
                    ChampionshipName = m.ChampionshipName
                })
                .ToListAsync();

            return Ok(matches);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MatchDetailDto>> Get(int id)
        {
            var match = await _context.Matches.FirstOrDefaultAsync(m => m.Id == id);
            if (match == null) return NotFound();

            var dto = new MatchDetailDto
            {
                Id = match.Id,
                Title = match.Title,
                TeamA = match.TeamA,
                DescriptionA = match.DescriptionA,
                TeamB = match.TeamB,
                DescriptionB = match.DescriptionB,
                MatchDate = match.MatchDate,
                Location = match.Location,
                SportType = match.SportType,
                ImageUrl = match.ImageUrl,
                ChampionshipName = match.ChampionshipName
            };

            return Ok(dto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Admin_Matchs")]
        public async Task<ActionResult<MatchDto>> Create(MatchCreateDto dto)
        {
            var userId = int.Parse(User.Identity!.Name!);

            var match = new Match
            {
                Title = dto.Title,
                TeamA = dto.TeamA,
                DescriptionA = dto.DescriptionA,
                TeamB = dto.TeamB,
                DescriptionB = dto.DescriptionB,
                MatchDate = dto.MatchDate,
                Location = dto.Location,
                SportType = dto.SportType,
                ImageUrl = dto.ImageUrl,
                ChampionshipName = dto.ChampionshipName,
                PublishDate = DateTime.UtcNow,
            };

            _context.Matches.Add(match);
            await _context.SaveChangesAsync();

            var result = new MatchDto
            {
                Id = match.Id,
                Title = match.Title,
                TeamA = match.TeamA,
                TeamB = match.TeamB,
                MatchDate = match.MatchDate,
                Location = match.Location,
                SportType = match.SportType,
                ImageUrl = match.ImageUrl,
                ChampionshipName = match.ChampionshipName
            };

            return CreatedAtAction(nameof(Get), new { id = match.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Admin_Matchs")]
        public async Task<IActionResult> Update(int id, MatchCreateDto dto)
        {
            var match = await _context.Matches.FirstOrDefaultAsync(m => m.Id == id);
            if (match == null) return NotFound();

            match.Title = dto.Title;
            match.TeamA = dto.TeamA;
            match.DescriptionA = dto.DescriptionA;
            match.TeamB = dto.TeamB;
            match.DescriptionB = dto.DescriptionB;
            match.MatchDate = dto.MatchDate;
            match.Location = dto.Location;
            match.SportType = dto.SportType;
            match.ImageUrl = dto.ImageUrl;
            match.ChampionshipName = dto.ChampionshipName;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Admin_Matchs")]
        public async Task<IActionResult> Delete(int id)
        {
            var match = await _context.Matches.FirstOrDefaultAsync(m => m.Id == id);
            if (match == null) return NotFound();

            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
