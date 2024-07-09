namespace LearningPlatform.App.ViewModels
{
    public class LessonDataViewModel
    {

        public Guid LessonId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get;set; } = string.Empty;

        public string? VideoUrl { get; set; } = string.Empty;

        public Guid CourseId { get; set; } = Guid.Empty;

        public string? Thumbnail { get; set; } = string.Empty;

        public int Order { get; set; } = 0;

        public string? Content { get; set; } = string.Empty;

    }
}
