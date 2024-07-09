using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Features.Course.Queries.GetAll
{
    public class GetAllCoursesResponse
    {
        public List<CourseDto> Courses { get; set; } = default!;
    }
}
