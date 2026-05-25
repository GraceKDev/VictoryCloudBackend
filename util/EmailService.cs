using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace VictoryCloudApi.Util
{
    public interface IEmailService
    {
        Task SendPasswordResetEmailAsync(string toEmail, string resetLink);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        
        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendPasswordResetEmailAsync(string toEmail, string resetLink)
        {
            var smtp = _config.GetSection("Smtp");
            if(CheckConfig(_config)) {
                
            }
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(smtp["FromName"], smtp["FromEmail"]));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = "Password Reset Request";

            message.Body = new TextPart("html")
            {
                Text = $"""
                    <h2>Password Reset</h2>
                    <p>Click the link below to reset your password. This link expires in 1 hour.</p>
                    <a href="{resetLink}">Reset Password</a>
                    <p>If you did not request a password reset, ignore this email.</p>
                """
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(smtp["Host"], int.Parse(smtp["Port"]!), SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(smtp["ApiKey"], smtp["SecretsKey"]);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
        public bool CheckConfig(IConfiguration configuration) {
            var smtp = configuration.GetSection("Smtp");
            return (
                smtp["FromName"] == null || 
                smtp["FromEmail"] == null ||
                smtp["Host"] == null ||
                smtp["Port"] == null ||
                smtp["ApiKey"] == null || 
                smtp["SecretsKey"] == null
            );
        }
    }
}
