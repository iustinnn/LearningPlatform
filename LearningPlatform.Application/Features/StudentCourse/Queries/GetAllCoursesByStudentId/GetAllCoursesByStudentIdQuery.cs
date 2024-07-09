using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Features.StudentCourse.Queries.GetAllCoursesByStudentId
{
    public record GetAllCoursesByStudentIdQuery(Guid Id) : IRequest<GetAllCoursesByStudentIdQueryResponse>;

}
