using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Persistence
{
    public interface IQuizRepository : IAsyncRepository<Quiz>
    {
        Task<Result<List<Quiz>>> GetQuizzesByCourseId(Guid courseId);
        Task<Result<Quiz>> GetQuizById(Guid quizId);
        Task<Result<Quiz>> CreateQuizAsync(Quiz quiz);
    }
}
