using LearningPlatform.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Features.Lesson.Commands.Create
{
    public class CreateLessonCommandResponse : BaseResponse
    {
        public CreateLessonCommandResponse() : base()
        {
        }

        public CreateLessonDto Lesson { get; set; } = default!;
    }
}
