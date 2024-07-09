using LearningPlatform.Application.Responses;

namespace LearningPlatform.Application.Features.Teacher.Commands.Update
{
    public class UpdateTeacherCommandResponse : BaseResponse
    {
        public UpdateTeacherCommandResponse() : base()
        {
        }

        public UpdateTeacherDto TeacherDTO { get; set; }
    }

}