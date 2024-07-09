using LearningPlatform.Application.Features.Course.Queries.GetCoursesIdsbyTeacherId;
using MediatR;

namespace LearningPlatform.Application.Features.Course.Queries.GetByIdTeacher
{
    public record GetCoursesIdsbyTeacherIdQuery(Guid Id): IRequest<GetCoursesIdsbyTeacherIdQueryResponse>;
   
}
