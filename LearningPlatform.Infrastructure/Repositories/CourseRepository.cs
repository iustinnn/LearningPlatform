using Infrastructure;
using LearningPlatform.Application.Persistence;
using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace LearningPlatform.Infrastructure.Repositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(LearningPlatformSystemContext context) : base(context)
        {
        }
        public async Task<Result<List<Guid>>> GetCoursesByInstructorId(Guid instructorId)
        {
            var result = await context.Set<Course>()
                .Where(e => e.TeacherId == instructorId)
                .Select(e => e.CourseId)
                .ToListAsync();

            return result == null || result.Count == 0
                ? Result<List<Guid>>.Failure("No courses found for the given instructor.")
                : Result<List<Guid>>.Success(result);
        }


        public async Task<Result<Course>> GetCoursesByCourseId(Guid courseId)
        {
            var result = await context.Set<Course>().FirstOrDefaultAsync(e => e.CourseId == courseId);
            return result == null ? Result<Course>.Failure("Entity not found") : Result<Course>.Success(result);
        }

        public async Task<Result<Course>> DoesCourseExist(Guid Id)
        {
            var result = await context.Set<Course>().FirstOrDefaultAsync(e => e.CourseId == Id);
            return result == null ? Result<Course>.Failure("Entity not found") : Result<Course>.Success(result);
        }
        /*
        public async Task<Result<Course>> GetCourseByUserId(Guid id)
        {
            var result = await context.Set<Course>().FirstOrDefaultAsync(e => e.UserId == id);
            return result == null ? Result<Course>.Failure("Entity not found") : Result<Course>.Success(result);
        }

        public async Task<Result<Course>> GetCourseByEmail(string email)
        {
            var result = await context.Set<Course>().FirstOrDefaultAsync(e => e.Email == email);
            return result == null ? Result<Course>.Failure("Entity not found") : Result<Course>.Success(result);
        }
        */
    }
}
