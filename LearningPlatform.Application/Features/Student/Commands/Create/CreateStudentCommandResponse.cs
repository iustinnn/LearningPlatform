using LearningPlatform.Application.Responses;

namespace LearningPlatform.Application.Features.Student.Commands.Create
{
    public class CreateStudentCommandResponse : BaseResponse
    {
        public CreateStudentCommandResponse() : base()
        {
        }

        public CreateStudentDto Student { get; set; } = default!;
    }
}
