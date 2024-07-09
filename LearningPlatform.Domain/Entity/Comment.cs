using System;
using System.ComponentModel.DataAnnotations;
using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.Entity;

namespace LearningPlatform.Domain.Entities
{
    public class Comment : AuditableEntity
    {
        public Comment(Guid userId, string content, Guid lessonId)
        {
            CommentId = Guid.NewGuid();
            UserId = userId;
            Content = content;
            CreatedAt = DateTime.UtcNow;
            LessonId = lessonId;
        }

        [Key]
        public Guid CommentId { get; private set; }

        public Guid UserId { get; private set; }

        public string Content { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public Guid LessonId { get; private set; }

        public Lesson Lesson { get; private set; }
    }
}
