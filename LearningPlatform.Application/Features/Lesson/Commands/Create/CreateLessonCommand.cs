using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Features.Lesson.Commands.Create
{
    public class CreateLessonCommand : IRequest<CreateLessonCommandResponse>
    {

        public string Title { get; set; } = default!;

        public string Description { get;set; } = default!;

        public string VideoUrl { get; set; } = default!;

        public string Content { get; set; } = default!;

        public Guid CourseId { get; set; }

        public string Thumbnail { get; set; } = default!;

        public int Order { get; set; } = default!;

     

    }


}
