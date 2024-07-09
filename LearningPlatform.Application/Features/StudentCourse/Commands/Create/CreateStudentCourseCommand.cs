using MediatR;

namespace LearningPlatform.Application.Features.StudentCourse.Commands.Create
{
    public class CreateStudentCourseCommand : IRequest<CreateStudentCourseCommandResponse>
    {
        public Guid StudentId { get; set; } = default!;
        public Guid CourseId { get; set; } = default!;

    }
}
