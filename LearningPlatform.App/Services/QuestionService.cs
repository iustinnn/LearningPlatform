using LearningPlatform.App.Contracts;
using LearningPlatform.App.ViewModels;
using LearningPlatform.Application.Persistence;
using LearningPlatform.Domain.Entities;
using System.Net.Http.Json;

namespace LearningPlatform.App.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly HttpClient _httpClient;

        public QuestionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<QuestionViewModel>> GetQuestionsByQuizId(Guid quizId)
        {
            var response = await _httpClient.GetFromJsonAsync<List<Question>>($"api/question/quiz/{quizId}");
            return response.Select(q => new QuestionViewModel
            {
                Id = q.Id,
                Text = q.Text,
                Answer1 = q.Answer1,
                Answer2 = q.Answer2,
                Answer3 = q.Answer3,
                CorrectAnswer = q.CorrectAnswer
            }).ToList();
        }

        public async Task<QuestionViewModel> GetQuestionById(Guid questionId)
        {
            var response = await _httpClient.GetFromJsonAsync<Question>($"api/question/{questionId}");
            return new QuestionViewModel
            {
                Id = response.Id,
                Text = response.Text,
                Answer1 = response.Answer1,
                Answer2 = response.Answer2,
                Answer3 = response.Answer3,
                CorrectAnswer = response.CorrectAnswer
            };
        }

        public async Task<QuestionViewModel> CreateQuestion(CreateQuestionViewModel question)
        {
            var response = await _httpClient.PostAsJsonAsync("api/question", question);
            response.EnsureSuccessStatusCode();

            var createdQuestion = await response.Content.ReadFromJsonAsync<Question>();
            return new QuestionViewModel
            {
                Id = createdQuestion.Id,
                Text = createdQuestion.Text,
                Answer1 = createdQuestion.Answer1,
                Answer2 = createdQuestion.Answer2,
                Answer3 = createdQuestion.Answer3,
                CorrectAnswer = createdQuestion.CorrectAnswer
            };
        }
    }
}
