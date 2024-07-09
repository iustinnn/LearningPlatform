using System.ComponentModel.DataAnnotations;


namespace LearningPlatform.Application.Models.Identity
{
    public class RegistrationTeacher
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string? LastName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Birth date is required")]
        public string? BirthDate { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string? Gender { get; set; }
    }
}
