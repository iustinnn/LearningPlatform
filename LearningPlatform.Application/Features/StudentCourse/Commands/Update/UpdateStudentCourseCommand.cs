using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Features.StudentCourse.Commands.Update
{
    public class UpdateStudentCourseCommand : IRequest<UpdateStudentCourseCommandResponse>
    {

        private UpdateStudentCourseCommand()
        {
        }


        public Guid StudentId { get; set; }


        public Guid CourseId { get; set; }

   
        public int Score { get; set; }

        [JsonConstructor]
        public UpdateStudentCourseCommand(Guid studentId, Guid courseId, int score)
        {
            StudentId = studentId;
            CourseId = courseId;
            Score = score;
        }
    }
}
