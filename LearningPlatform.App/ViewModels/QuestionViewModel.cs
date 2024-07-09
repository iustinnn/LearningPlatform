using System;
using System.Collections.Generic;

namespace LearningPlatform.App.ViewModels
{
    public class QuestionViewModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string CorrectAnswer { get; set; }
        public string UserAnswer { get; set; }
        public List<string> ShuffledAnswers { get; set; }

        public void ShuffleAnswers()
        {
            ShuffledAnswers = new List<string> { Answer1, Answer2, Answer3, CorrectAnswer };
            var rnd = new Random();
            for (int i = ShuffledAnswers.Count - 1; i > 0; i--)
            {
                int j = rnd.Next(i + 1);
                var temp = ShuffledAnswers[i];
                ShuffledAnswers[i] = ShuffledAnswers[j];
                ShuffledAnswers[j] = temp;
            }
        }
    }
}
