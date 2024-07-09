using System;
using System.ComponentModel.DataAnnotations;
using LearningPlatform.Domain.Common;

namespace LearningPlatform.Domain.Entities
{
    public class Answer : AuditableEntity
    {
        public Answer(string text, bool isCorrect, Guid questionId)
        {
            AnswerId = Guid.NewGuid();
            Text = text;
            IsCorrect = isCorrect;
            QuestionId = questionId;
        }

        [Key]
        public Guid AnswerId { get; private set; }

        public string Text { get; private set; }

        public bool IsCorrect { get; private set; }

        public Guid QuestionId { get; private set; }

        public Question Question { get; private set; }
    }
}
