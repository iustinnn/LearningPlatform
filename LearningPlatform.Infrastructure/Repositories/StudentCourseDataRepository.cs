using Infrastructure;
using LearningPlatform.Application.Persistence;
using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace LearningPlatform.Infrastructure.Repositories
{
    public class StudentCourseDataRepository : BaseRepository<StudentCourse>, IStudentCourseRepository
    {
        public StudentCourseDataRepository(LearningPlatformSystemContext context) : base(context)
        {
        }
        public async Task<Result<List<Guid>>> GetStudentsByCourseId(Guid courseId)
        {
            var result = await context.Set<StudentCourse>()
              .Where(e => e.CourseId == courseId)
               .Select(e => e.StudentId)
              .ToListAsync();

            return result == null
        ? Result<List<Guid>>.Failure("Entity not found")
        : Result<List<Guid>>.Success(result);
        }

        public async Task<Result<List<Guid>>>GetCourseIdsByStudentId(Guid studentId)
        {
            var result = await context.Set<StudentCourse>()
              .Where(e => e.StudentId == studentId)
               .Select(e => e.CourseId)
              .ToListAsync();

            return result == null
        ? Result<List<Guid>>.Failure("Entity not found")
        : Result<List<Guid>>.Success(result);
        }

        public async Task<Result<StudentCourse>> FindByIdAsync(Guid courseId, Guid studentId)
        {
            var result = await context.Set<StudentCourse>()
        .FirstOrDefaultAsync(e => e.CourseId == courseId && e.StudentId == studentId);

            return result == null
                ? Result<StudentCourse>.Failure("Student course not found")
                : Result<StudentCourse>.Success(result);
        }

        public async Task DeleteAsyncStudentCourse(StudentCourse studentCourse)
        {
            context.Set<StudentCourse>().Remove(studentCourse);
            await context.SaveChangesAsync();
        }
    }
}
