using AutoMapper;
using LearningPlatform.Application.Persistence;
using MediatR;


namespace LearningPlatform.Application.Features.Lesson.Queries.GetAllLessonsbyCourseId
{
    public class GetAllLessonsbyCourseIdQueryHandler : IRequestHandler<GetAllLessonsbyCourseIdQuery, GetAllLessonsbyCourseIdQueryResponse>
    {

       private readonly ILessonRepository _lessonRepository;
        private readonly IMapper _mapper;

        public GetAllLessonsbyCourseIdQueryHandler(ILessonRepository lessonRepository, IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            _mapper = mapper;
        }

        public async Task<GetAllLessonsbyCourseIdQueryResponse> Handle(GetAllLessonsbyCourseIdQuery request, CancellationToken cancellationToken)
        {
            var response = new GetAllLessonsbyCourseIdQueryResponse();
            var result = await _lessonRepository.GetLessonsByCourseId(request.id);
            if (result.IsSuccess)
            {
                response.Lessons = result.Value;
                return response;
            }
            return response;

        }
    }
}
