
namespace LearningPlatform.Application.Features.Lesson.Commands.Create
{
    public class CreateLessonDto
    {
        public Guid LessonId { get; set; } 

        public string Title { get; set; } 

        public string Description { get; set; } 

        public string VideoUrl { get; set; } 

        public string Thumbnail { get; set; }

        public string Content { get; set; }

        public Guid CourseId { get; set; }

        public int Order { get; set; }
    }
}
