using System.ComponentModel.DataAnnotations;

namespace fluttyBackend.Service.services.AuthService.signUp.DTO.request
{
    public class UserRegistrationDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}