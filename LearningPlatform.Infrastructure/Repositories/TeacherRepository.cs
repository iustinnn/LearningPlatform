using Infrastructure;
using LearningPlatform.Application.Persistence;
using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.Entities;
using LearningPlatform.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Infrastructure.Repositories
{
    public class TeacherRepository : BaseRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(LearningPlatformSystemContext context) : base(context)
        {
        }

        public async Task<Result<Teacher>> GetTeacherByUserId(Guid id)
        {
            var result = await context.Set<Teacher>().FirstOrDefaultAsync(e => e.TeacherId == id);
            return result == null ? Result<Teacher>.Failure("Entity not found") : Result<Teacher>.Success(result);
        }

        public async Task<Result<Teacher>> GetTeacherByEmail(string email)
        {
            var result = await context.Set<Teacher>().FirstOrDefaultAsync(e => e.Email == email);
            return result == null ? Result<Teacher>.Failure("Entity not found") : Result<Teacher>.Success(result);
        }

        public async Task<Result<Teacher>> DoesTeacherExist(Guid Id)
        {
            var result = await context.Set<Teacher>().FirstOrDefaultAsync(e => e.TeacherId == Id);
            return result == null ? Result<Teacher>.Failure("Entity not found") : Result<Teacher>.Success(result);
        }
    }
}
