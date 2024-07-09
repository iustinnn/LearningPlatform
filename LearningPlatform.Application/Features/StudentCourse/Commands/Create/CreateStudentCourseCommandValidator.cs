using FluentValidation;
using LearningPlatform.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Features.StudentCourse.Commands.Create
{
    public class CreateStudentCourseCommandValidator : AbstractValidator<CreateStudentCourseCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentCourseRepository _studentCourseRepository;

        public CreateStudentCourseCommandValidator(IStudentRepository studentRepository, ICourseRepository courseRepository, IStudentCourseRepository studentCourseRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _studentCourseRepository = studentCourseRepository;

            RuleFor(x => x.CourseId)
          .MustAsync(async (courseId, cancellation) =>
          {
              var courseExistsResult = await _courseRepository.DoesCourseExist(courseId);
              return courseExistsResult.IsSuccess;
          })
          .WithMessage("Course does not exist.");


            RuleFor(x => x.StudentId)
       .MustAsync(async (studentId, cancellation) =>
       {
           var studentExistsResult = await _studentRepository.DoesStudentExist(studentId);
           return studentExistsResult.IsSuccess;
       })
       .WithMessage("Student does not exist.");

        }
        

    }
}
