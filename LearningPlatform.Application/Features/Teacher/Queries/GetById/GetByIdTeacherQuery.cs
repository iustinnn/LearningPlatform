using LearningPlatform.Domain.Common;
using MediatR;


namespace LearningPlatform.Application.Features.Teacher.Queries.GetById
{
    public record GetByIdTeacherQuery(Guid Id) : IRequest<Result<TeacherDto>>;
  
}
