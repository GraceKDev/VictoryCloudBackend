using System.Text.Json.Serialization;

namespace VictoryCloudApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Email { get; set; }
    
        public required string Password { get; set; } 
        [JsonIgnore]
        public string? LastSignInDate { get; set; }
        [JsonIgnore]
        public string? PasswordResetToken { get; set; }
        [JsonIgnore]
        public DateTime? PasswordResetTokenExpiry { get; set; }
    }
}