using MediatR;

namespace LearningPlatform.Application.Features.Student.Queries.GetById
{
    public record GetByIdStudentQuery(Guid Id) : IRequest<StudentDto>;
}
