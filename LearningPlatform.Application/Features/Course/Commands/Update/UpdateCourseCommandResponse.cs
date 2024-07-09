
using LearningPlatform.Application.Responses;

namespace LearningPlatform.Application.Features.Course.Commands.Update
{
    public class UpdateCourseCommandResponse : BaseResponse
    {
        public UpdateCourseCommandResponse() : base()
        {
        }
        public UpdateCourseDto CourseDto { get; set; }
    }

}
