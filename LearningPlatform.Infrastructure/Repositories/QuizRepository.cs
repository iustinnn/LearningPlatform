using Infrastructure;
using LearningPlatform.Application.Persistence;
using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Infrastructure.Repositories
{
    public class QuizRepository : BaseRepository<Quiz>, IQuizRepository
    {
        public QuizRepository(LearningPlatformSystemContext context) : base(context)
        {
        }

        public async Task<Result<List<Quiz>>> GetQuizzesByCourseId(Guid courseId)
        {
            var result = await context.Set<Quiz>()
                .Where(q => q.CourseId == courseId)
                .ToListAsync();

            return result == null
                ? Result<List<Quiz>>.Failure("No quizzes found for the specified course")
                : Result<List<Quiz>>.Success(result);
        }

        public async Task<Result<Quiz>> GetQuizById(Guid quizId)
        {
            var result = await context.Set<Quiz>().FindAsync(quizId);

            return result == null
                ? Result<Quiz>.Failure("Quiz not found")
                : Result<Quiz>.Success(result);
        }

        public async Task<Result<Quiz>> CreateQuizAsync(Quiz quiz)
        {
            context.Set<Quiz>().Add(quiz);
            await context.SaveChangesAsync();

            return Result<Quiz>.Success(quiz);
        }


    }
}
