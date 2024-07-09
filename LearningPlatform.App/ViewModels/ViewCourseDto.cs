namespace LearningPlatform.App.ViewModels
{
    public class ViewCourseDto
    {

        public Guid CourseId { get; set; } 
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;

        public Guid TeacherId { get; set; } = Guid.Empty;

        public DateTime StartDate { get; set; } = DateTime.MinValue;

        public DateTime EndDate { get; set; } = DateTime.MinValue;
    }
}
