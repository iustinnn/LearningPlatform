using Infrastructure;
using LearningPlatform.Application.Persistence;
using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Infrastructure.Repositories
{
   
    public class LessonRepository : BaseRepository<Lesson>, ILessonRepository
    {
        public LessonRepository(LearningPlatformSystemContext context) : base(context)
        {
        }
        /*
        public async Task<Result<Lesson>> GetLessonById(Guid id)
        {
            var result = await context.Set<Lesson>().FirstOrDefaultAsync(e => e.LessonId == id);
            return result == null ? Result<Lesson>.Failure("Entity not found") : Result<Lesson>.Success(result);
        }

        public async Task<Result<Lesson>> GetLessonByTitle(string title)
        {
            var result = await context.Set<Lesson>().FirstOrDefaultAsync(e => e.Title == title);
            return result == null ? Result<Lesson>.Failure("Entity not found") : Result<Lesson>.Success(result);
        }
        */
        public async Task<Result<List<Guid>>> GetLessonsByCourseId(Guid courseId)
        {
            var result = await context.Set<Lesson>()
              .Where(e => e.CourseId == courseId)
               .Select(e => e.LessonId)
              .ToListAsync();

            return result == null || result.Count == 0
                    ? Result<List<Guid>>.Failure("No courses found for the given instructor.")
                    : Result<List<Guid>>.Success(result);
        }
    }
   
}
