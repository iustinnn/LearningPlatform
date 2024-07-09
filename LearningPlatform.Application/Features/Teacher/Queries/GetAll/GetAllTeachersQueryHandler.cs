using LearningPlatform.Application.Persistence;
using MediatR;

namespace LearningPlatform.Application.Features.Teacher.Queries.GetAll
{
    public class GetAllTeachersQueryHandler : IRequestHandler<GetAllTeachersQuery, GetAllTeachersResponse>
    {
        private readonly ITeacherRepository repository;

        public GetAllTeachersQueryHandler(ITeacherRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetAllTeachersResponse> Handle(GetAllTeachersQuery request, CancellationToken cancellationToken)
        {
            GetAllTeachersResponse response = new();
            var result = await repository.GetAllAsync();

            if (result.IsSuccess)
            {
                response.Teachers = result.Value.Select(c => new TeacherDto
                {
                    TeacherId = c.TeacherId,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    Description = c.Description,
                    ProfilePictureUrl = c.ProfilePictureUrl



                }).ToList();

            }
            return response;
        }
    }
}
