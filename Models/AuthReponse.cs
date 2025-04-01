using System.ComponentModel.DataAnnotations;

namespace MathAPIClient.Models
{
    public class AuthResponse
    {
        [Required]
        public string Token { get; set; }        
        public string UserId { get; set; }

        public AuthResponse(string token, string currentUserId){
            this.Token = token;
            this.UserId = currentUserId;
        }
        
    }
}