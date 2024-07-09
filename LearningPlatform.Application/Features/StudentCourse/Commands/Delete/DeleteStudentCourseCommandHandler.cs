using LearningPlatform.Application.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Features.StudentCourse.Commands.Delete
{
    public class DeleteStudentCourseCommandHandler : IRequestHandler<DeleteStudentCourseCommand, DeleteStudentCourseCommandResponse>
    { 
        private readonly IStudentCourseRepository _studentCourseRepository;

        public DeleteStudentCourseCommandHandler(IStudentCourseRepository studentCourseRepository)
        {
            _studentCourseRepository = studentCourseRepository;
        }

        public async Task<DeleteStudentCourseCommandResponse> Handle(DeleteStudentCourseCommand request, CancellationToken cancellationToken)
        {
            var studentCourse = await _studentCourseRepository.FindByIdAsync(request.CourseId, request.StudentId);
            if(!studentCourse.IsSuccess)
            {
                return new DeleteStudentCourseCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { "StudentCourse not found" }
                };
            }

            var resultRelation =studentCourse.Value;

            await _studentCourseRepository.DeleteAsyncStudentCourse(resultRelation);

            return new DeleteStudentCourseCommandResponse
            {
                Success = true
            };
        }
    }
}
