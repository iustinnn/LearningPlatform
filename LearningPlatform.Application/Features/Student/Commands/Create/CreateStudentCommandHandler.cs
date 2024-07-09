using LearningPlatform.Application.Persistence;
using LearningPlatform.Domain.Entities;
using MediatR;

namespace LearningPlatform.Application.Features.Student.Commands.Create
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, CreateStudentCommandResponse>
    {
     private readonly IStudentRepository _studentRepository;
        public CreateStudentCommandHandler(IStudentRepository studentRepository)
        {
            this._studentRepository = studentRepository;
        }
        public async Task<CreateStudentCommandResponse> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateStudentCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if(!validationResult.IsValid)
            {
                return new CreateStudentCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }
            var student = Domain.Entities.Student.Create(request.FirstName,request.LastName, request.Email);
            if(!student.IsSuccess)
            {
                return new CreateStudentCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { student.Error }
                };
            }

            await _studentRepository.AddAsync(student.Value);
            return new CreateStudentCommandResponse
            {
                Success = true,
                Student = new CreateStudentDto
                {
                    StudentId = student.Value.UserId,
                    FirstName = student.Value.FirstName,
                    LastName = student.Value.LastName,
                    Email = student.Value.Email,
              

                }
            };
        }
    }
}
