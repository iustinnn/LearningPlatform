namespace LearningPlatform.App.ViewModels
{
    public class UpdateLessonDataViewModel
    {
        public Guid LessonId { get; private set; }

        public string Title { get; private set; } = string.Empty;

        public string? Description { get; private set; } = string.Empty;

        public string? VideoUrl { get; private set; } = string.Empty;

        public Guid CourseId { get; private set; } = Guid.Empty;

        public int Order { get; private set; } = 0;

        public string? Content { get; private set; } = string.Empty;
    }
}
