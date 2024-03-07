using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using fluttyBackend.Domain.Models.Utils;
using Microsoft.EntityFrameworkCore;

namespace fluttyBackend.Domain.Models.UserRoleEntities
{
    [Table(EntityNamesConstants.User)]
    [Index(nameof(User.Email), IsUnique = true)]
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password min lenght is 8")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(325)]
        [MinLength(1)]
        public string Email { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [RegularExpression(@"^\S.*$", ErrorMessage = "First name cannot consist of only spaces")]
        [MaxLength(120)]
        [MinLength(1)]
        public string FirstName { get; set; }

        [MaxLength(120)]
        [MinLength(1)]
        public string LastName { get; set; }
    }
}
