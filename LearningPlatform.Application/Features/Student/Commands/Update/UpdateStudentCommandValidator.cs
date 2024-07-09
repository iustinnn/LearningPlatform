using Domain.Entity;
using FluentValidation;

namespace LearningPlatform.Application.Features.Student.Commands.Update
{
    public class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
    {
        public UpdateStudentCommandValidator()
        {
            RuleFor(t => t.StudentId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(t => t.PhoneNumber)
    .NotEmpty().WithMessage("{PropertyName} is required.")
    .Length(10).WithMessage("{PropertyName} must be 10 digits long.")
    .Matches(@"^\d{10}$").WithMessage("{PropertyName} must consist of 10 digits.");





        }
    }
}
