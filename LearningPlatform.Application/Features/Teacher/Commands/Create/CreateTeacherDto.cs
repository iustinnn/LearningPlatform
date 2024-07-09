namespace LearningPlatform.Application.Features.Teacher.Commands.Create
{
    public class CreateTeacherDto
    {
        public Guid TeacherId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public string? Description { get; set; }

        public string? ProfilePictureUrl { get; set; }
    }
}
