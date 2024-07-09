using MediatR;
using System.Text.Json.Serialization;

namespace LearningPlatform.Application.Features.StudentCourse.Commands.Delete
{
    public class DeleteStudentCourseCommand : IRequest<DeleteStudentCourseCommandResponse>
    {
    
        private DeleteStudentCourseCommand()
        {

        }

        public Guid StudentId { get; set; } = default!;
        public Guid CourseId { get; set; } = default!;

        [JsonConstructor]
        public DeleteStudentCourseCommand(Guid studentId, Guid courseId)
        {
            StudentId = studentId;
            CourseId = courseId;
        }
    }
}
