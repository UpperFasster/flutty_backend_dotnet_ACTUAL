using System.ComponentModel.DataAnnotations;

namespace fluttyBackend.Service.services.AuthService.signIn.DTO.request
{
    public class UserSignInDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}