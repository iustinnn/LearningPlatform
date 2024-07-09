using LearningPlatform.Application.Responses;

namespace LearningPlatform.Application.Features.Course.Commands.Create
{
    public class CreateCourseCommandResponse : BaseResponse
    {
        public CreateCourseCommandResponse() : base()
        {

        }

        public CreateCourseDTO Course { get; set; } = default!;
    }
}
