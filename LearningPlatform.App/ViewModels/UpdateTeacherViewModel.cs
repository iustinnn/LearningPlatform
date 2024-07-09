namespace LearningPlatform.App.ViewModels
{
    public class UpdateTeacherViewModel
    {
        public Guid TeacherId { get; set; }
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public string? ProfilePictureUrl { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}
