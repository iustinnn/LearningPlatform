﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Features.Lesson.Queries
{
    public class LessonDto
    {
        public Guid LessonId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string VideoUrl { get; set; } = string.Empty;

        public string Thumbnail { get; set; } = string.Empty;
        public Guid CourseId { get; set; }

        public int Order { get; set; }

        public string Content { get; set; } = string.Empty;


    }
}
