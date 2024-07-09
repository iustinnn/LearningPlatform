using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Features.Course.Queries
{
    public class CourseDto
    {

        public Guid CourseId { get; set; }
        public string? Title { get; set; } = string.Empty;
        
        public string? Description { get; set; } = string.Empty;

        public string? VideoUrl { get; set; } = string.Empty;

        public Guid TeacherId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? Thumbnail { get; set; } = string.Empty;


    }
}
