
using LearningPlatform.Application.Features.Teacher.Queries.GetAll;
using LearningPlatform.Application.Persistence;
using MediatR;

namespace LearningPlatform.Application.Features.Student.Queries.GetAll
{
    public class GetAllStudentssQueryHandler : IRequestHandler<GetAllStudentsQuery, GetAllStudentsResponse>
    {
        private readonly IStudentRepository repository;

        public GetAllStudentssQueryHandler(IStudentRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetAllStudentsResponse> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            GetAllStudentsResponse response = new();
            var result = await repository.GetAllAsync();

            if (result.IsSuccess)
            {
                response.Students = result.Value.Select(c => new StudentDto
                {
                    StudentId = c.UserId,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email

                 

                }).ToList();

            }
            return response;
        }
    }
}
