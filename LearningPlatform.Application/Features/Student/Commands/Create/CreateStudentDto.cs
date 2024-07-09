namespace LearningPlatform.Application.Features.Student.Commands.Create
{
    public class CreateStudentDto
    {
        public Guid StudentId { get; set; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? ProfilePictureUrl { get; set; }




    }
}