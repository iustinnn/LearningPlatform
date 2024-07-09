using LearningPlatform.App.Contracts;
using LearningPlatform.App.Services.Responses;
using LearningPlatform.App.ViewModels;
using LearningPlatform.Domain.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace LearningPlatform.App.Services
{
    public class LessonService : ILessonService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public LessonService(HttpClient httpClient, ITokenService tokenService, IUserService userService)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;
            _userService = userService;
        }

        public async Task<List<LessonDataViewModel>> GetLessonsByCourseId(Guid courseId)
        {
            var response = await _httpClient.GetAsync($"api/v1/Lesson/getbycourse/{courseId}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            Console.WriteLine("content: " + content);

            using (JsonDocument doc = JsonDocument.Parse(content))
            {
                JsonElement root = doc.RootElement;

                JsonElement lessonsElement = root.GetProperty("lessons");

                var lessonGuids = new List<Guid>();

                foreach (JsonElement element in lessonsElement.EnumerateArray())
                {
                    lessonGuids.Add(Guid.Parse(element.GetString()));
                }

                var lessonTasks = new List<Task<LessonDataViewModel>>();

                foreach (var lessonGuid in lessonGuids)
                {
                    lessonTasks.Add(GetLessonById(lessonGuid));
                }

                var lessons = await Task.WhenAll(lessonTasks);
                Console.WriteLine("lessons: " + lessons[0].LessonId);
                return lessons.ToList();
            }
        }

        public async Task<LessonDataViewModel> GetLessonById(Guid lessonId)
        {
            Console.WriteLine("les: " + lessonId);
            var response = await _httpClient.GetAsync($"api/v1/Lesson/{lessonId}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var lessonNode = JsonNode.Parse(content);
            if (lessonNode == null)
            {
                throw new ApplicationException("Failed to parse lesson content.");
            }

            LessonDataViewModel lesson = new LessonDataViewModel
            {
                LessonId = Guid.Parse(lessonNode["lessonId"]?.ToString() ?? Guid.Empty.ToString()),
                Title = lessonNode["title"]?.ToString(),
                Description = lessonNode["description"]?.ToString(),
                VideoUrl = lessonNode["videoUrl"]?.ToString(),
                CourseId = Guid.Parse(lessonNode["courseId"]?.ToString() ?? Guid.Empty.ToString()),
                Order = int.Parse(lessonNode["order"]?.ToString() ?? "0"),
                Content = lessonNode["content"]?.ToString(),
                Thumbnail = lessonNode["thumbnail"]?.ToString()

            };

            Console.WriteLine("kill: " + lesson.LessonId);
            return lesson;
        }

        public async Task<ApiResponse<LessonDataViewModel>> CreateLessonAsync(CreateLessonViewModel lessonDataViewModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/v1/Lesson", lessonDataViewModel);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadFromJsonAsync<ApiResponse<LessonDataViewModel>>();
            content!.IsSuccess = response.IsSuccessStatusCode;
            return content!;
        }



    }
}
