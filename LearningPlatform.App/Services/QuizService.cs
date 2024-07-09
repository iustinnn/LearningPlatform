using LearningPlatform.App.Contracts;
using LearningPlatform.App.ViewModels;
using LearningPlatform.Application.Persistence;
using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.Entities;
using System.Net.Http.Json;

namespace LearningPlatform.App.Services
{ 
     public class QuizService : IQuizService
    {
        private readonly HttpClient _httpClient;

        public QuizService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Quiz>> GetQuizzesByCourseId(Guid courseId)
        {
            var response = await _httpClient.GetFromJsonAsync<List<Quiz>>($"api/Quiz/course/{courseId}");
            return response ?? new List<Quiz>();
        }

        public async Task<Quiz> GetQuizById(Guid quizId)
        {
            var response = await _httpClient.GetFromJsonAsync<Quiz>($"api/Quiz/{quizId}");
            return response;
        }

        public async Task<Quiz> CreateQuiz(CreateQuizViewModel quiz)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Quiz", quiz);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Quiz>();
        }


    }
}
