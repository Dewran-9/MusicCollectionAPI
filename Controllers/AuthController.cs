using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public ActionResult Login([FromBody] LoginRequest request)
    {
        // Hardkodet bruger til test
        if (request.Username != "admin" || request.Password != "1234")
            return Unauthorized("Forkert brugernavn eller adgangskode");

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("dinHemmeligeNøgleSomErMindst32TegnLang!"));

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddHours(1),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}