using LearningPlatform.App.Contracts;
using LearningPlatform.App.Services.Responses;
using LearningPlatform.App.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace LearningPlatform.App.Services
{
    public class StudentCourseDataService : IStudentCourseDataService
    {
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;
        private readonly IUserService userService;

        public StudentCourseDataService(HttpClient httpClient, ITokenService tokenService, IUserService userService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
            this.userService = userService;
        }

       
        public async Task<List<CourseDataViewModel>> GetCourseIdsByStudentId(Guid id)
        {

            var requestUri =
              $"api/v1/StudentCourse/studentid/{id}";
            var result = await httpClient.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            Console.WriteLine(content);
            using (JsonDocument doc = JsonDocument.Parse(content))
            {
                JsonElement root = doc.RootElement;

                JsonElement studentsElement = root.GetProperty("courses");

                var studentGuids = new List<Guid>();

                foreach (JsonElement element in studentsElement.EnumerateArray())
                {
                    studentGuids.Add(Guid.Parse(element.GetString()));
                }

                var studentTasks = new List<Task<CourseDataViewModel>>();

                foreach (var studentGuid in studentGuids)
                {
                    studentTasks.Add(GetCourseById(studentGuid));
                }

                var students = await Task.WhenAll(studentTasks);


                return students.ToList();

            }
        }



        public async Task<List<StudentViewModel>> GetStudentsByCourseId(Guid id)
        {


            var requestUri =
                $"api/v1/StudentCourse/courseid/{id}";
            var result = await httpClient.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            Console.WriteLine(content);
            using (JsonDocument doc = JsonDocument.Parse(content))
            {
                JsonElement root = doc.RootElement;
   
                JsonElement studentsElement = root.GetProperty("students");
     
                var studentGuids = new List<Guid>();
     
                foreach (JsonElement element in studentsElement.EnumerateArray())
                {
                    studentGuids.Add(Guid.Parse(element.GetString()));
                }
              
                var studentTasks = new List<Task<StudentViewModel>>();

                foreach (var studentGuid in studentGuids)
                {
                    studentTasks.Add(GetStudentById(studentGuid));
                }

                var students = await Task.WhenAll(studentTasks);


                return students.ToList();

            }
        }

        public async Task<ApiResponse<StudentCourseEnrollViewModel>> CreateEnrollAsync(StudentCourseEnrollViewModel studentCourseEnrollViewModel)
        {
            httpClient.DefaultRequestHeaders.Authorization
                   = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
           
            var result = await httpClient.PostAsJsonAsync($"api/v1/StudentCourse", studentCourseEnrollViewModel);
            Console.WriteLine(result);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<StudentCourseEnrollViewModel>>();
            Console.WriteLine(response);
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }

        public async Task<StudentViewModel> GetStudentById(Guid studentId)
        {
            var studentRequestUri = $"api/v1/Student/{studentId}";
            var studentResult = await httpClient.GetAsync(studentRequestUri, HttpCompletionOption.ResponseHeadersRead);
            studentResult.EnsureSuccessStatusCode();
            var studentContent = await studentResult.Content.ReadAsStringAsync();
            if (!studentResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(studentContent);
            }
            var student = JsonSerializer.Deserialize<StudentViewModel>(studentContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return student;
        }


        public async Task<CourseDataViewModel> GetCourseById(Guid studentId)
        {
            var studentRequestUri = $"api/v1/Course/{studentId}";
            var studentResult = await httpClient.GetAsync(studentRequestUri, HttpCompletionOption.ResponseHeadersRead);
            studentResult.EnsureSuccessStatusCode();
            var studentContent = await studentResult.Content.ReadAsStringAsync();
            if (!studentResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(studentContent);
            }
            var student = JsonSerializer.Deserialize<CourseDataViewModel>(studentContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return student;
        }

    }
}
