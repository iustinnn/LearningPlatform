using AutoMapper;
using LearningPlatform.Application.Persistence;
using MediatR;


namespace LearningPlatform.Application.Features.StudentCourse.Queries.GetAllCoursesByStudentId
{
    public class GetAllCoursesByStudentIdQueryHandler : IRequestHandler<GetAllCoursesByStudentIdQuery, GetAllCoursesByStudentIdQueryResponse>
    {
        private readonly IStudentCourseRepository studentCourseRepository;
        private readonly IMapper mapper;

        public GetAllCoursesByStudentIdQueryHandler(IStudentCourseRepository studentCourseRepository, IMapper mapper)
        {
            this.studentCourseRepository = studentCourseRepository;
            this.mapper = mapper;
        }

        public async Task<GetAllCoursesByStudentIdQueryResponse> Handle(GetAllCoursesByStudentIdQuery request, CancellationToken cancellationToken)
        {
            var response = new GetAllCoursesByStudentIdQueryResponse();
            var result = await studentCourseRepository.GetCourseIdsByStudentId(request.Id);
            if (result.IsSuccess)
            {
                response.Courses = result.Value;
                return response;
            }
            return response;
          
        }
    }
}
