using AutoMapper;
using LearningPlatform.Application.Persistence;
using MediatR;


namespace LearningPlatform.Application.Features.Course.Commands.Update
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, UpdateCourseCommandResponse>
    {
        private readonly ICourseRepository courseRepository;
        private readonly ITeacherRepository teacherRepository;
        private readonly IMapper mapper;

        public UpdateCourseCommandHandler(ICourseRepository repository, IMapper mapper, ITeacherRepository teacherRepository)
        {
            this.courseRepository = repository;
            this.mapper = mapper;
            this.teacherRepository = teacherRepository;
        }


        public async Task<UpdateCourseCommandResponse> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateCourseCommandValidator(teacherRepository,courseRepository);
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return new UpdateCourseCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
                };
            }

            var existingCourse = await courseRepository.GetCoursesByCourseId(request.CourseId);
            if (!existingCourse.IsSuccess)
            {
                return new UpdateCourseCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { "Course not found" }
                };
            }

            var updatedCourse = mapper.Map(request, existingCourse.Value);
            var result = await courseRepository.UpdateAsync(updatedCourse);

            if(!result.IsSuccess)
            {
                return new UpdateCourseCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { result.Error }
                };
            }

            return new UpdateCourseCommandResponse
            {
                Success = true,
                CourseDto = new UpdateCourseDto()
                {
                    CourseId = result.Value.CourseId,
                    Title = result.Value.Title,
                    Description = result.Value.Description,
                    VideoUrl = result.Value.VideoUrl,
                    TeacherId = result.Value.TeacherId,
                    StartDate = result.Value.StartDate,
                    EndDate = result.Value.EndDate,
                    Thumbnail = result.Value.Thumbnail
          
                }
            };
            
        }
    }
}
