using LearningPlatform.Application.Responses;

namespace LearningPlatform.Application.Features.Teacher.Commands.Create
{
    public class CreateTeacherCommandResponse : BaseResponse
    {
        public CreateTeacherCommandResponse() : base()
        {
        }

        public CreateTeacherDto Teacher { get; set; } = default!;
    }
}