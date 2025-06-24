using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using UaddAPI.Data;
using UaddAPI.Dto.User;
using UaddAPI.Models;
using UaddAPI.Services;

namespace UaddAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UaddDbContext _context;
    private readonly TokenService _tokenService;

    public AuthController(UaddDbContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    [AllowAnonymous] // cualquier persona puede registrarse
    public async Task<ActionResult<TokenDto>> Register(UserRegisterDto dto)
    {
        if (await _context.Users.AnyAsync(u => u.Username == dto.Username))
        {
            return BadRequest("Ese nombre de usuario ya existe.");
        }

        using var hmac = new HMACSHA512();

        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordSalt = hmac.Key,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
            Role = Role.User // 👈 se asigna automáticamente como User
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var token = _tokenService.CreateToken(user);

        return Ok(new TokenDto { Token = token });
    }


    [HttpPost("login")]
    public async Task<ActionResult<TokenDto>> Login(UserLoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);

        if (user == null) return Unauthorized("Usuario no encontrado");

        using var hmac = new HMACSHA512(user.PasswordSalt);
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));

        if (!hash.SequenceEqual(user.PasswordHash))
            return Unauthorized("Contraseña incorrecta");

        var token = _tokenService.CreateToken(user);

        return Ok(new TokenDto { Token = token });
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<UserInfoDto>> GetCurrentUser()
    {
        var userId = int.Parse(User.Identity!.Name!);
        var user = await _context.Users.FindAsync(userId);

        if (user == null) return NotFound();

        return new UserInfoDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role.ToString()
        };
    }
}
