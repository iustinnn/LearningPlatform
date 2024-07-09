using AutoMapper;
using LearningPlatform.Application.Features.StudentCourse.Queries.GetAllCoursesByStudentId;
using LearningPlatform.Application.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Features.StudentCourse.Queries.GetAllStudentsByCourseId
{
    public class GetAllStudentsByCourseIdQueryHandler : IRequestHandler<GetAllStudentsByCourseIdQuery, GetAllStudentsByCourseIdQueryResponse>
    {
        private readonly IStudentCourseRepository _studentCourseRepository;
        private readonly IMapper _mapper;
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;

        public GetAllStudentsByCourseIdQueryHandler(IStudentCourseRepository studentCourseRepository, IMapper mapper, ICourseRepository courseRepository, IStudentRepository studentRepository)
        {
            _studentCourseRepository = studentCourseRepository;
            _mapper = mapper;
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
        }

        public async Task<GetAllStudentsByCourseIdQueryResponse> Handle(GetAllStudentsByCourseIdQuery request, CancellationToken cancellationToken)
        {
            var response = new GetAllStudentsByCourseIdQueryResponse();
            var result = await _studentCourseRepository.GetStudentsByCourseId(request.id);
            if (result.IsSuccess)
            {
                response.Students = result.Value;
                return response;
            }
            return response;

        }
    }
    
    
}
