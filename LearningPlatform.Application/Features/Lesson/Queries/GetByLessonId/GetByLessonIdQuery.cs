using LearningPlatform.Application.Features.Course.Queries;
using MediatR;


namespace LearningPlatform.Application.Features.Lesson.Queries.GetByLessonId
{
    public record class GetByLessonIdQuery(Guid Id) : IRequest<LessonDto>;
 
}
