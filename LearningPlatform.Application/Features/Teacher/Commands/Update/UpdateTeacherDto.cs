namespace LearningPlatform.Application.Features.Teacher.Commands.Update
{
    public class UpdateTeacherDto
    {
        public Guid TeacherId { get; set; }
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;

        public string? Email { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public string? ProfilePictureUrl { get; set; } = string.Empty;
    
    }
}