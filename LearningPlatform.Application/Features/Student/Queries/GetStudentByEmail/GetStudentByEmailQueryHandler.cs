using LearningPlatform.Application.Features.Student.Queries.GetById;
using LearningPlatform.Application.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Features.Student.Queries.GetStudentByEmail
{
    public class GetStudentByEmailQueryHandler : IRequestHandler<GetStudentByEmailQuery, StudentDto>
    {
        private readonly IStudentRepository repository;

        public GetStudentByEmailQueryHandler(IStudentRepository repository)
        {
            this.repository = repository;
        }

        public async Task<StudentDto> Handle(GetStudentByEmailQuery request, CancellationToken cancellationToken)
        {
            var student = await repository.GetStudentByEmail(request.email);
            if (student.IsSuccess)
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
