using LearningPlatform.App.ViewModels;
using LearningPlatform.Domain.Entities;

namespace LearningPlatform.App.Contracts
{
    public interface IQuestionService
    {
        Task<List<QuestionViewModel>> GetQuestionsByQuizId(Guid quizId);
        Task<QuestionViewModel> GetQuestionById(Guid questionId);
        Task<QuestionViewModel> CreateQuestion(CreateQuestionViewModel question);
    }
}
