using AutoMapper;
using LearningPlatform.Application.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Features.StudentCourse.Commands.Update
{
    public class UpdateStudentCourseCommandHandler : IRequestHandler<UpdateStudentCourseCommand, UpdateStudentCourseCommandResponse>
    {
        private readonly IStudentCourseRepository _studentCourseRepository;
        private readonly IMapper _mapper;

        public UpdateStudentCourseCommandHandler(IStudentCourseRepository studentCourseRepository, IMapper mapper)
        {
            _studentCourseRepository = studentCourseRepository;
            _mapper = mapper;
        }

        public async Task<UpdateStudentCourseCommandResponse> Handle(UpdateStudentCourseCommand request, CancellationToken cancellationToken)
        {
            var studentCourse = await _studentCourseRepository.FindByIdAsync(request.StudentId, request.CourseId);

            if (studentCourse == null)
            {
                return new UpdateStudentCourseCommandResponse
                {
                    Success = false,
                    Error = "StudentCourse record not found."
                };
            }

            studentCourse.Value.Score = request.Score;

            await _studentCourseRepository.UpdateAsync(studentCourse.Value);

            return new UpdateStudentCourseCommandResponse
            {
                Success = true,
                StudentCourse = _mapper.Map<UpdateStudentCourseDto>(studentCourse)
            };
        }
    }
}
