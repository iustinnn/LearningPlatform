using Infrastructure;
using LearningPlatform.Application.Persistence;
using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.Entities;
using LearningPlatform.Domain.Entity;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                var result = await context.Set<Lesson>()
                    .Where(e => e.CourseId == courseId)
                    .Select(e => e.LessonId)
                    .ToListAsync();

                return result == null || result.Count == 0
                    ? Result<List<Guid>>.Failure("No lessons found for the given course.")
                    : Result<List<Guid>>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<List<Guid>>.Failure($"An error occurred: {ex.Message}");
            }
        }
        public async Task<Result<Lesson>> GetLessonByLessonId(Guid lessonId)
        {
            var result = await context.Set<Lesson>().FirstOrDefaultAsync(e => e.LessonId == lessonId);
            return result == null ? Result<Lesson>.Failure("Entity not found") : Result<Lesson>.Success(result);
        }
    }

}
