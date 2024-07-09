using AutoMapper;
using LearningPlatform.Application.Features.Student.Commands.Create;
using LearningPlatform.Application.Persistence;
using MediatR;

namespace LearningPlatform.Application.Features.Course.Commands.Create
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, CreateCourseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICourseRepository _courseRepository;

        public CreateCourseCommandHandler(IMapper mapper, ICourseRepository courseRepository)
        {
            _mapper = mapper;
            _courseRepository = courseRepository;
        }

        public async Task<CreateCourseCommandResponse> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateCourseCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new CreateCourseCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var course = Domain.Entities.Course.Create(request.Title, request.Description, request.StartDate, request.EndDate, request.TeacherId, request.VideoUrl, request.Thumbnail);
      

            await _courseRepository.AddAsync(course.Value);

            if (!course.IsSuccess)
            {
                return new CreateCourseCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { course.Error }
                };
            }

            return new CreateCourseCommandResponse
            {
                Success = true,
                Course = new CreateCourseDTO
                {
                    CourseId = course.Value.CourseId,
                    Title = course.Value.Title,
                    Description = course.Value.Description,
                    StartDate = course.Value.StartDate,
                    EndDate = course.Value.EndDate,
                    TeacherId = course.Value.TeacherId,
                    VideoUrl = course.Value.VideoUrl,
                    Thumbnail = course.Value.Thumbnail
                }
            };


        }




    }
}
