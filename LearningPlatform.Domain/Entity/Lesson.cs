
using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LearningPlatform.Domain.Entity
{
    public class Lesson : AuditableEntity
    {
        public Lesson(string title, string description, string videoUrl, Guid courseId, int order, string content, string thumbnail)
        {
            LessonId = Guid.NewGuid();
            Title = title;
            Description = description;
            VideoUrl = videoUrl;
            CourseId = courseId;
            Order = order;
            Content = content;
            Thumbnail = thumbnail;
            
        }

        [Key]
        public Guid LessonId { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public string VideoUrl { get; private set; }

        public string Thumbnail { get; private set; }

        public Guid CourseId { get; private set; }

        public int Order { get; private set; }

        public string Content { get; private set; } 

        public Course Course { get; private set; }

      
      
        public void UpdateVideoUrl(string videoUrl)
        {
            VideoUrl = videoUrl;
        }

        public static Result<Lesson> Create(string title, string description, string videoUrl, Guid courseId, int order, string content, string thumbnail)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return Result<Lesson>.Failure("Title is required");
            }
            if (string.IsNullOrWhiteSpace(description))
            {
                return Result<Lesson>.Failure("Description is required");
            }
            if (string.IsNullOrWhiteSpace(videoUrl))
            {
                return Result<Lesson>.Failure("Video URL is required");
            }
            if (courseId == Guid.Empty)
            {
                return Result<Lesson>.Failure("Course ID is required");
            }
            if (order < 0)
            {
                return Result<Lesson>.Failure("Order must be greater than or equal to 0");
            }
            return Result<Lesson>.Success(new Lesson(title, description, videoUrl, courseId, order,content,thumbnail));
        }
    }
}
