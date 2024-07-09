﻿using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.App.ViewModels
{
    public class CreateQuizViewModel
    {
        public string? Title { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public Guid CourseId { get; set; }
    }
}
