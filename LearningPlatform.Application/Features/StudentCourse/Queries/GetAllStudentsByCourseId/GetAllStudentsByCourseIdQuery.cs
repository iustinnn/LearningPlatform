using MediatR;

namespace LearningPlatform.Application.Features.StudentCourse.Queries.GetAllStudentsByCourseId
{
    public record GetAllStudentsByCourseIdQuery(Guid id) : IRequest<GetAllStudentsByCourseIdQueryResponse>; 
    
}
