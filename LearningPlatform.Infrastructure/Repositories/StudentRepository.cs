using Infrastructure;
using LearningPlatform.Application.Persistence;
using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Numerics;


namespace LearningPlatform.Infrastructure.Repositories
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(LearningPlatformSystemContext context) : base(context)
        {
        }

        public async Task<Result<Student>> GetStudentByUserId(Guid id)
        {
            var result = await context.Set<Student>().FirstOrDefaultAsync(e => e.UserId == id);
            return result == null ? Result<Student>.Failure("Entity not found") : Result<Student>.Success(result);
        }

        public async Task<Result<Student>> GetStudentByEmail(string email)
        {
            var result = await context.Set<Student>().FirstOrDefaultAsync(e => e.Email == email);
            return result == null ? Result<Student>.Failure("Entity not found") : Result<Student>.Success(result);
        }

        public async Task<Result<Student>> DoesStudentExist(Guid Id)
        {
            var result = await context.Set<Student>().FirstOrDefaultAsync(e => e.UserId == Id);
            return result == null ? Result<Student>.Failure("Entity not found") : Result<Student>.Success(result);
        }
    }
}
