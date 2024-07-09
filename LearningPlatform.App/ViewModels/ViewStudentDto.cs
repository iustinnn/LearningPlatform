namespace LearningPlatform.App.ViewModels
{
    public class ViewStudentDto
    {
        public Guid StudentId { get; set; }
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
    }
}
