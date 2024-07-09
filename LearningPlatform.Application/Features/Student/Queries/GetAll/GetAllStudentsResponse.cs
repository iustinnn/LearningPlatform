namespace LearningPlatform.Application.Features.Student.Queries.GetAll
{
    public class GetAllStudentsResponse
    {
        public List<StudentDto> Students { get; set; } = default!;
    }
}