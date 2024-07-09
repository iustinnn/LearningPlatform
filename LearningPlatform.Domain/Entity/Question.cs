using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LearningPlatform.Domain.Common;

namespace LearningPlatform.Domain.Entities
{
    public class Question 
    {
       
        public Question(string text,int order, string answer1, string answer2, string answer3, string correctAnswer, Guid quizId)
        {
           Id = Guid.NewGuid();
            Text = text;
            Order = order;
            Answer1 = answer1;
            Answer2 = answer2;
            Answer3 = answer3;
            CorrectAnswer = correctAnswer;
            QuizId = quizId;
        }

        [Key]
        public Guid Id { get; set; }

        public Guid QuizId { get; set; }

        public string Text { get; set; }

        public int Order { get; set; }

        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string CorrectAnswer { get; set; }


        public static Result<Question> Create(string text, int order, string answer1, string answer2, string answer3, string correctAnswer, Guid quizId)
        {
            if (string.IsNullOrEmpty(text))
            {
                return Result<Question>.Failure("Question text is required");
            }
            if (order < 0)
            {
                return Result<Question>.Failure("Order must be a non-negative integer");
            }
            return Result<Question>.Success(new Question(text, order, answer1, answer2, answer3, correctAnswer, quizId));
        }
    }
}
