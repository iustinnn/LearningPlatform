using AutoMapper;
using LearningPlatform.Application.Features.Course.Queries.GetCoursesIdsbyTeacherId;
using LearningPlatform.Application.Features.StudentCourse.Queries.GetAllStudentsByCourseId;
using LearningPlatform.Application.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Features.Course.Queries.GetByIdTeacher
{
    public class GetCoursesIdsbyTeacherIdQueryHandler : IRequestHandler<GetCoursesIdsbyTeacherIdQuery, GetCoursesIdsbyTeacherIdQueryResponse>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public GetCoursesIdsbyTeacherIdQueryHandler(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }


        public async Task<GetCoursesIdsbyTeacherIdQueryResponse> Handle(GetCoursesIdsbyTeacherIdQuery request, CancellationToken cancellationToken)
        {
            var response = new GetCoursesIdsbyTeacherIdQueryResponse();
            Console.WriteLine(request.Id);
            var result = await _courseRepository.GetCoursesByInstructorId(request.Id);
            Console.WriteLine(result.Value);
            if (result.IsSuccess)
            {
                response.Courses = result.Value;
                return response;
            }
            return response;

        }
    }
    
}
