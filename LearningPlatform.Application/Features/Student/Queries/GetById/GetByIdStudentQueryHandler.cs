using LearningPlatform.Application.Persistence;
using MediatR;

namespace LearningPlatform.Application.Features.Student.Queries.GetById
{
    public class GetByIdStudentQueryHandler : IRequestHandler<GetByIdStudentQuery, StudentDto>
    {
        private readonly IStudentRepository repository;

        public GetByIdStudentQueryHandler(IStudentRepository repository)
        {
            this.repository = repository;
        }

        public async Task<StudentDto> Handle(GetByIdStudentQuery request, CancellationToken cancellationToken)
        {
            var student = await repository.FindByIdAsync(request.Id);
            if(student.IsSuccess)
            {
                return new StudentDto
                {
                    StudentId = student.Value.UserId,
                    FirstName = student.Value.FirstName,
                    LastName = student.Value.LastName,
                    Email = student.Value.Email,
                    PhoneNumber = student.Value.PhoneNumber,
                    Description = student.Value.Description,
                    ProfilePictureUrl = student.Value.ProfilePictureUrl
                };
            }
            return new StudentDto();
        }
    }
}
