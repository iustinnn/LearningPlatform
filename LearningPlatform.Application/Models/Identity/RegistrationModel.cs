using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.Application.Models.Identity
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "First Name is required")]
        public string? Firstname { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string? Lastname { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
