using System;
using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.App.ViewModels
{
    public class CreateQuestionViewModel
    {
        [Required(ErrorMessage = "Question text is required")]
        public string Text { get; set; }

        public int Order { get; set; } = 1;

        [Required(ErrorMessage = "Answer 1 is required")]
        public string Answer1 { get; set; }

        [Required(ErrorMessage = "Answer 2 is required")]
        public string Answer2 { get; set; }

        [Required(ErrorMessage = "Answer 3 is required")]
        public string Answer3 { get; set; }

        [Required(ErrorMessage = "Correct Answer is required")]
        public string CorrectAnswer { get; set; }

        [Required(ErrorMessage = "Quiz ID is required")]
        public Guid QuizId { get; set; }
    }
}
