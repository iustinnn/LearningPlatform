using LearningPlatform.Application.Persistence;
using LearningPlatform.Domain.Entities;
using MediatR;

namespace LearningPlatform.Application.Features.Teacher.Commands.Create
{
    public class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, CreateTeacherCommandResponse>
    {
        private readonly ITeacherRepository repository;

        public CreateTeacherCommandHandler(ITeacherRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateTeacherCommandResponse> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateTeacherCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new CreateTeacherCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var teacher = Domain.Entity.Teacher.Create(request.FirstName, request.LastName, request.Email, request.PhoneNumber, request.Description, request.ProfilePictureUrl);
            if (!teacher.IsSuccess)
            {
                return new CreateTeacherCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { teacher.Error }
                };
            }

            await repository.AddAsync(teacher.Value);

            return new CreateTeacherCommandResponse
            {
                Success = true,
                Teacher = new CreateTeacherDto
                {
                    TeacherId = teacher.Value.TeacherId,
                    FirstName = teacher.Value.FirstName,
                    LastName = teacher.Value.LastName,
                    Email = teacher.Value.Email,
                    PhoneNumber = teacher.Value.PhoneNumber,
                    Description = teacher.Value.Description,
                    ProfilePictureUrl = teacher.Value.ProfilePictureUrl


                }
            };
        }
    }
}
