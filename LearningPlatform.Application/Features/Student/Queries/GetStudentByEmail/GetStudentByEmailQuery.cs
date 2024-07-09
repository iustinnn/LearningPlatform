using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Features.Student.Queries.GetStudentByEmail
{
    public record GetStudentByEmailQuery(string email) : IRequest<StudentDto>;
}
