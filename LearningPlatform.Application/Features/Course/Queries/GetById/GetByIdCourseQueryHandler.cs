using LearningPlatform.Application.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Features.Course.Queries.GetById
{
    public class GetByIdCourseQueryHandler : IRequestHandler<GetByIdCourseQuery, CourseDto>
    {
        private readonly ICourseRepository _courseRepository;

        public GetByIdCourseQueryHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<CourseDto> Handle(GetByIdCourseQuery request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetCoursesByCourseId(request.Id);
            if(course.IsSuccess)
            {
                return new CourseDto
                {
                    CourseId = course.Value.CourseId,
                    Title = course.Value.Title,
                    Description = course.Value.Description,
                    VideoUrl = course.Value.VideoUrl,
                    TeacherId = course.Value.TeacherId,
                    StartDate = course.Value.StartDate,
                    EndDate = course.Value.EndDate,
                    Thumbnail = course.Value.Thumbnail
                };
            }
            return new CourseDto();
        }
    }
}
