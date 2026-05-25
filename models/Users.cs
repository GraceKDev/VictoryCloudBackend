namespace VictoryCloudApi.Models
{
    public class User
    {
        public int Id {get;set;}
        public required string Email {get;set;}
        public required string Password {get;set;}
        public string? LastSignInDate {get;set;}
        public string? PasswordResetToken {get;set;}
        public DateTime? PasswordResetTokenExpiry {get;set;}
    }
}