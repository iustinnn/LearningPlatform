using LearningPlatform.Application.Responses;

namespace LearningPlatform.Application.Features.Student.Commands.Update
{
    public class UpdateStudentCommandResponse : BaseResponse
    {
        public UpdateStudentCommandResponse() : base()
        {
        }

        public UpdateStudentDto StudentDTO { get; set; }
    }

}