using FluentValidation;
using LearningPlatform.Application.Persistence;


namespace LearningPlatform.Application.Features.Lesson.Commands.Create
{
    public class CreateLessonCommandValidator : AbstractValidator<CreateLessonCommand>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ILessonRepository _lessonRepository;

        public CreateLessonCommandValidator(ICourseRepository courseRepository, ILessonRepository lessonRepository)
        {
            _courseRepository = courseRepository;
            _lessonRepository = lessonRepository;

            RuleFor(x => x.CourseId)
          .MustAsync(async (courseId, cancellation) =>
          {
              var courseExistsResult = await _courseRepository.DoesCourseExist(courseId);
              return courseExistsResult.IsSuccess;
          })
          .WithMessage("Course does not exist.");
        }
    }
}
