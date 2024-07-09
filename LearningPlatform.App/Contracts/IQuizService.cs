using LearningPlatform.App.ViewModels;
using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.Entities;

namespace LearningPlatform.App.Contracts
{
    public interface IQuizService
    {
        Task<List<Quiz>> GetQuizzesByCourseId(Guid courseId);
        Task<Quiz> GetQuizById(Guid quizId);
        Task<Quiz> CreateQuiz(CreateQuizViewModel quiz);
    }
}
