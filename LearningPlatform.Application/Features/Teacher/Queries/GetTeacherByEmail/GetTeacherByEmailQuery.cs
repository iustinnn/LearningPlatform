using LearningPlatform.Application.Features.Student.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Features.Teacher.Queries.GetTeacherByEmail
{
    public record GetTeacherByEmailQuery(string email) : IRequest<TeacherDto>;
}
