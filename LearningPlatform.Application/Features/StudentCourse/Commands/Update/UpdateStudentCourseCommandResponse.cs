namespace LearningPlatform.Application.Features.StudentCourse.Commands.Update
{
    public class UpdateStudentCourseCommandResponse
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public UpdateStudentCourseDto StudentCourse { get; set; }
    }

    public class UpdateStudentCourseDto
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public int Score { get; set; }
    }
}
