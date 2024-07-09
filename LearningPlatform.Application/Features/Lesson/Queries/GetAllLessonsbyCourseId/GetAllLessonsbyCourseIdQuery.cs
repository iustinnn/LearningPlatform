using MediatR;


namespace LearningPlatform.Application.Features.Lesson.Queries.GetAllLessonsbyCourseId
{
    public record GetAllLessonsbyCourseIdQuery(Guid id) : IRequest<GetAllLessonsbyCourseIdQueryResponse>; 
    
}


