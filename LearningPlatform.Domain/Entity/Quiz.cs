using System.ComponentModel.DataAnnotations;
using LearningPlatform.Domain.Common;

namespace LearningPlatform.Domain.Entities
{
    public class Quiz
    {
        public Quiz(string title, Guid courseId, string description)
        {
            Id = Guid.NewGuid();
            Title = title;
            CourseId = courseId;
            Description = description;
            Questions = new List<Question>();
        }

        [Key]
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string Title { get; set; }

        public string Description { get; set;}

        public ICollection<Question> Questions { get; set; }


        public static Result<Quiz> Create(string title, Guid courseId, string description)
        {
            if (string.IsNullOrEmpty(title))
            {
                return Result<Quiz>.Failure("Quiz title is required");
            }
            return Result<Quiz>.Success(new Quiz(title, courseId, description));
        }

    }
}
