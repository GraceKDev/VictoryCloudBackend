using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VictoryCloudApi.Data;
using VictoryCloudApi.Util;

[ApiController]
[Route("Api/[controller]")]

public class AuthController : ControllerBase
{
    private readonly MyDbContext _context;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _config;

    public AuthController(MyDbContext myDbContext, IEmailService emailService, IConfiguration config)
    {
        _context = myDbContext;
        _emailService = emailService;
        _config = config;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequestData loginRequestData)
    {
        if (loginRequestData == null)
        {
            return BadRequest("Login data is required.");
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == loginRequestData.Email);

        if (user == null || !PasswordHelper.Verify(loginRequestData.Password, user.Password))
        {
            return Unauthorized("Invalid email or password.");
        }

        var jwtKey = _config["Jwt:Key"]
            ?? throw new InvalidOperationException("JWT Key is not configured.");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(
            double.Parse(_config["Jwt:ExpiresInMinutes"] ?? "60"));

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: creds);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        Response.Cookies.Append("auth", tokenString, new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Strict,
            Expires = expires
        });
        return Ok(new { token = tokenString, userId = user.Id, email = user.Email });
    }

    [HttpPost("Logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("auth");
        return Ok(new { message = "Logged out successfully." });
    }

    [HttpPost("Forgot-Password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordRequestData forgotPasswordData)
    {
        if (forgotPasswordData == null)
        {
            return BadRequest("Pass Request Failed");
        }
        if (string.IsNullOrWhiteSpace(forgotPasswordData.Email))
        {
            return BadRequest("No Email provided");
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == forgotPasswordData.Email);

        if (user != null)
        {
            Console.WriteLine("User found: " + user.Email);
            var token = Convert.ToHexString(System.Security.Cryptography.RandomNumberGenerator.GetBytes(32));
            user.PasswordResetToken = token;
            user.PasswordResetTokenExpiry = DateTime.UtcNow.AddHours(1);
            await _context.SaveChangesAsync();
            var baseUrl = _config["AppBaseUrl"];
            var resetLink = $"{baseUrl}/reset-password?token={token}&email={Uri.EscapeDataString(user.Email)}";
            await _emailService.SendPasswordResetEmailAsync(user.Email, resetLink);
        }

        return Ok(new { message = "If the user exists they will be sent password reset instructions" });
    }

    [HttpPost("Reset-Password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequestData resetData)
    {
        if (resetData == null || string.IsNullOrWhiteSpace(resetData.Token) || string.IsNullOrWhiteSpace(resetData.NewPassword))
        {
            return BadRequest("Invalid reset request.");
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == resetData.Email);

        if (user == null || user.PasswordResetToken != resetData.Token || user.PasswordResetTokenExpiry < DateTime.UtcNow)
        {
            return BadRequest("Invalid or expired reset token.");
        }
        user.Password = PasswordHelper.Hash(resetData.NewPassword);
        user.PasswordResetToken = null;
        user.PasswordResetTokenExpiry = null;
        await _context.SaveChangesAsync();
        return Ok(new { message = "Password reset successful." });
    }
}


public record LoginRequestData(string Email, string Password);
public record ForgotPasswordRequestData(string Email);
public record ResetPasswordRequestData(string Email, string Token, string NewPassword);