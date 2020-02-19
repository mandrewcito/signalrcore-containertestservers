using System.ComponentModel.DataAnnotations;

namespace src {
     public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}