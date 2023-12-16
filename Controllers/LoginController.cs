using BankApi.Services;
using BankApi.Data.BankModels;
using Microsoft.AspNetCore.Mvc;
using BankApi.Data.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BankApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class LoginController : ControllerBase
{
    private readonly LoginServices _loginService;

    private IConfiguration config;

    public LoginController(LoginServices loginService, IConfiguration config)
    {
        this._loginService = loginService;
        this.config = config;
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Login(AdminDto adminDto)
    {
        var admin = await _loginService.GetAdmin(adminDto);

        if (admin is null)
            return BadRequest(new { message = "Credenciales invalidas." });

        //general token
        string jwtToken = GenerateToken(admin);

        return Ok(new { token = jwtToken });
    }

    private string GenerateToken(Administrator admin)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, admin.Name),
            new Claim(ClaimTypes.Email, admin.Email),
            new Claim("AdminType", admin.Admintype)
        };

#pragma warning disable CS8604 // Possible null reference argument.
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JWT:Key").Value));
#pragma warning restore CS8604 // Possible null reference argument.

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var securityToken = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: creds);

        string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return token;
    }
}