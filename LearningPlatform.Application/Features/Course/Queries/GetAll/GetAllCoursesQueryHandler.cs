using LearningPlatform.Application.Persistence;
using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Features.Course.Queries.GetAll
{
    public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, GetAllCoursesResponse>
    {
        private readonly ICourseRepository repository;

        public GetAllCoursesQueryHandler(ICourseRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetAllCoursesResponse> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            GetAllCoursesResponse response = new();
            var courses = await repository.GetAllAsync();
            if(courses.IsSuccess)
            {
                response.Courses = courses.Value.Select(course => new CourseDto
                {
                    CourseId = course.CourseId,
                    Title = course.Title,
                    Description = course.Description,
                    VideoUrl = course.VideoUrl,
                    TeacherId = course.TeacherId,
                    StartDate = course.StartDate,
                    EndDate = course.EndDate,
                    Thumbnail = course.Thumbnail
                }).ToList();
            }

           return response;
        }
    }
}
