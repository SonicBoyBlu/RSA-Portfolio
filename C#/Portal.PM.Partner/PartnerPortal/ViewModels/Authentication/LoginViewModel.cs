using System.ComponentModel.DataAnnotations;

namespace Acme.ViewModels.Authentication
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsGoogleAuthorized { get; set; }
    }
}