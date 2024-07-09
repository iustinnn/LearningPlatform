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
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(LearningPlatformSystemContext context) : base(context)
        {
        }

        public async Task<Result<List<Question>>> GetQuestionsByQuizId(Guid quizId)
        {
            var result = await context.Set<Question>()
                .Where(q => q.QuizId == quizId)
                .OrderBy(q => q.Order)
                .ToListAsync();

            return result == null
                ? Result<List<Question>>.Failure("No questions found for the specified quiz")
                : Result<List<Question>>.Success(result);
        }

        public async Task<Result<Question>> GetQuestionById(Guid questionId)
        {
            var result = await context.Set<Question>().FindAsync(questionId);

            return result == null
                ? Result<Question>.Failure("Question not found")
                : Result<Question>.Success(result);
        }

        public async Task<Result<Question>> CreateQuestionAsync(Question question)
        {
            context.Set<Question>().Add(question);
            await context.SaveChangesAsync();

            return Result<Question>.Success(question);
        }

    }
}

