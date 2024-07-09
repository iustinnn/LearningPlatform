namespace LearningPlatform.App.ViewModels
{
    public class CreateCourseViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }
        public string VideoUrl { get; set; }

        public Guid TeacherId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Thumbnail { get; set; }
    }
}
