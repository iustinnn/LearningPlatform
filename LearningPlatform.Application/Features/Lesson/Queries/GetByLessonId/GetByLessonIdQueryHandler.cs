using LearningPlatform.Application.Features.Course.Queries;
using LearningPlatform.Application.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Features.Lesson.Queries.GetByLessonId
{
    public class GetByLessonIdQueryHandler : IRequestHandler<GetByLessonIdQuery, LessonDto>
    {
        private readonly ILessonRepository _lessonRepository;

        public GetByLessonIdQueryHandler(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public async Task<LessonDto> Handle(GetByLessonIdQuery request, CancellationToken cancellationToken)
        {
            var lesson = await _lessonRepository.GetLessonByLessonId(request.Id);
            if(lesson.IsSuccess)
            {
                return new LessonDto
                {
                    LessonId = lesson.Value.LessonId,
                    Title = lesson.Value.Title,
                    Description = lesson.Value.Description,
                    VideoUrl = lesson.Value.VideoUrl,
                    CourseId = lesson.Value.CourseId,
                    Order = lesson.Value.Order,
                    Content = lesson.Value.Content,
                    Thumbnail = lesson.Value.Thumbnail
                };
            }
            return new LessonDto();
        }


    }
}
