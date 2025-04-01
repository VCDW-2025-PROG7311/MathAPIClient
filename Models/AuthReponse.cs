using System.ComponentModel.DataAnnotations;

namespace MathAPIClient.Models
{
    public class AuthResponse
    {
        [Required]
        public string Token { get; set; }
        
    }
}