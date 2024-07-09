using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Features.Course.Commands.Update
{
    public class UpdateCourseCommand : IRequest<UpdateCourseCommandResponse>
    {
        private UpdateCourseCommand()
        {
        }

        public Guid CourseId { get; private set; }

        public string Title { get; private set; } = string.Empty;

        public string Description { get; private set; } = string.Empty;

        public string VideoUrl { get; private set; } = string.Empty;

        public DateTime StartDate { get; private set; } = DateTime.MinValue;

        public DateTime EndDate { get; private set; } = DateTime.MinValue;

        public string Thumbnail { get; private set; } = string.Empty;



        [JsonConstructor]
        public UpdateCourseCommand(Guid courseId, string title, string description, string videoUrl, DateTime startDate, DateTime endDate, string thumbnail)
        {
            CourseId = courseId;
            Title = title;
            Description = description;
            VideoUrl = videoUrl;
            StartDate = startDate;
            EndDate = endDate;
            Thumbnail = thumbnail;

 
        }

    


    }
}
