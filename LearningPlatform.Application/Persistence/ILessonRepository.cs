using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.Entities;
using LearningPlatform.Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Persistence
{
    public interface ILessonRepository : IAsyncRepository<Lesson>
    {

        Task<Result<List<Guid>>> GetLessonsByCourseId(Guid lessonId);

        Task<Result<Lesson>> GetLessonByLessonId(Guid lessonId);

    }
}
