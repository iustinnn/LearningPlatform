using LearningPlatform.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Features.StudentCourse.Commands.Create
{
    public class CreateStudentCourseCommandResponse : BaseResponse
    {
        public CreateStudentCourseCommandResponse() : base()
        {
        }

        public CreateStudentCourseDto StudentCourse { get; set; } = default!;
    }
}
