using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Features.Course.Commands.Update
{
    public class UpdateCourseDto
    {
        public Guid CourseId { get; set; }
        public string? Title { get; set; } = string.Empty;
        
        public string? Description { get; set; } = string.Empty;

        public string? VideoUrl { get; set; } = string.Empty;

        public Guid TeacherId { get; set; } = Guid.Empty;

        public DateTime StartDate { get; set; } = DateTime.MinValue;

        public DateTime EndDate { get; set; } = DateTime.MinValue;

        public string? Thumbnail { get; set; } = string.Empty;
    }
}
