using MediatR;


namespace LearningPlatform.Application.Features.Course.Queries.GetById
{
  public record class GetByIdCourseQuery(Guid Id) : IRequest<CourseDto>;
}
