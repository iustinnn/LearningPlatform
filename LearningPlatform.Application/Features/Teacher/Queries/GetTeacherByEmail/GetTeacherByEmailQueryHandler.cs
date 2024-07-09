using LearningPlatform.Application.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Features.Teacher.Queries.GetTeacherByEmail
{
    public class GetTeacherByEmailQueryHandler : IRequestHandler<GetTeacherByEmailQuery, TeacherDto>
    {
        private readonly ITeacherRepository repository;

        public GetTeacherByEmailQueryHandler(ITeacherRepository repository)
        {
            this.repository = repository;
        }

        public async Task<TeacherDto> Handle(GetTeacherByEmailQuery request, CancellationToken cancellationToken)
        {
            var teacher = await repository.GetTeacherByEmail(request.email);
            if (teacher.IsSuccess)
            {
                return new TeacherDto
                {
                    TeacherId = teacher.Value.TeacherId,
                    FirstName = teacher.Value.FirstName,
                    LastName = teacher.Value.LastName,
                    Email = teacher.Value.Email,
                    PhoneNumber = teacher.Value.PhoneNumber,
                    Description = teacher.Value.Description,
                    ProfilePictureUrl = teacher.Value.ProfilePictureUrl

                };
            }
            return new TeacherDto();
        }
    }
}
