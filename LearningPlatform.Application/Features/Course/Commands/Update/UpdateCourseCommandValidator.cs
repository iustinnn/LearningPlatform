using FluentValidation;
using LearningPlatform.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Features.Course.Commands.Update
{
    public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
    {
        private readonly ITeacherRepository teacherRepository;
        private readonly ICourseRepository courseRepository;

        public UpdateCourseCommandValidator(ITeacherRepository teacherRepository, ICourseRepository courseRepository)
        {
            this.teacherRepository = teacherRepository;
            this.courseRepository = courseRepository;

            RuleFor(x => x.VideoUrl)
                .MaximumLength(200).WithMessage("Video URL must not exceed 200 characters");

        
            /*
            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date is required")
                .Must(BeAValidDate).WithMessage("Invalid start date");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("End date is required")
                .Must(BeAValidDate).WithMessage("Invalid end date")
                .GreaterThan(x => x.StartDate).WithMessage("End date must be after start date");
            */
        }   

    }
}
