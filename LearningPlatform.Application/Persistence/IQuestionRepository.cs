using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Persistence
{
    public interface IQuestionRepository : IAsyncRepository<Question>
    {
        Task<Result<List<Question>>> GetQuestionsByQuizId(Guid quizId);
        Task<Result<Question>> GetQuestionById(Guid questionId);

        Task<Result<Question>> CreateQuestionAsync(Question question);

    }
}
