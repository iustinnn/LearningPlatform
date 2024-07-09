using AutoMapper;
using LearningPlatform.Application.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Features.Lesson.Commands.Create
{
    public class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand, CreateLessonCommandResponse>
    {
      

        private readonly ILessonRepository lessonRepository;
        private readonly IMapper mapper;
        private readonly ICourseRepository courseRepository;


        public CreateLessonCommandHandler(ILessonRepository lessonRepository, IMapper mapper, ICourseRepository courseRepository)
        {
            this.lessonRepository = lessonRepository;
            this.mapper = mapper;
            this.courseRepository = courseRepository;
        }

        public async Task<CreateLessonCommandResponse> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLessonCommandValidator(courseRepository, lessonRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new CreateLessonCommandResponse()
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var course = await courseRepository.FindByIdAsync(request.CourseId);    
            if (course == null)
            {
                return new CreateLessonCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { "Course not found" }
                };
            }
            var lessonCreated = Domain.Entity.Lesson.Create(request.Title, request.Description, request.VideoUrl, request.CourseId, request.Order, request.Content, request.Thumbnail);
            if(!lessonCreated.IsSuccess)
            {
                return new CreateLessonCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { lessonCreated.Error }
                };
            }
            await lessonRepository.AddAsync(lessonCreated.Value);
            return new CreateLessonCommandResponse
            {
                Success = true,
                Lesson = new CreateLessonDto
                {
                    LessonId = lessonCreated.Value.LessonId,
                    Title = lessonCreated.Value.Title,
                    Description = lessonCreated.Value.Description,
                    VideoUrl = lessonCreated.Value.VideoUrl,
                    Content = lessonCreated.Value.Content,
                    CourseId = lessonCreated.Value.CourseId,
                    Order = lessonCreated.Value.Order,
                    Thumbnail = lessonCreated.Value.Thumbnail
                }
                
            };
        }
        
    }
  
}
