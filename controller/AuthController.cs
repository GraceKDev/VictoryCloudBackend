using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VictoryCloudApi.Data;
using VictoryCloudApi.Util;

[ApiController]
[Route("api/[controller]")]

public class AuthController:ControllerBase
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
        if(loginRequestData == null)
        {
            return BadRequest("Login data is required.");
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == loginRequestData.Email);

        if(user == null || !PasswordHelper.Verify(loginRequestData.Password, user.Password))
        {
            return Unauthorized("Invalid email or password.");
        }

        return Ok(new { message = "Login successful.", userId = user.Id, email = user.Email });
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordRequestData forgotPasswordData) {
        if(forgotPasswordData == null) {
            return BadRequest("Pass Request Failed");
        }
        if(string.IsNullOrWhiteSpace(forgotPasswordData.Email)) {
            return BadRequest("No Email provided");
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == forgotPasswordData.Email);

        if(user != null)
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

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequestData resetData)
    {
        if(resetData == null || string.IsNullOrWhiteSpace(resetData.Token) || string.IsNullOrWhiteSpace(resetData.NewPassword))
        {
            return BadRequest("Invalid reset request.");
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == resetData.Email);

        if(user == null || user.PasswordResetToken != resetData.Token || user.PasswordResetTokenExpiry < DateTime.UtcNow)
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