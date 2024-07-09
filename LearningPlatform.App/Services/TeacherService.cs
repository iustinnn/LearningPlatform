using LearningPlatform.App.Contracts;
using LearningPlatform.App.Services.Responses;
using LearningPlatform.App.ViewModels;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace LearningPlatform.App.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;
        private readonly IUserService userService;

    public TeacherService(HttpClient httpClient, ITokenService tokenService, IUserService userService)
        {
        this.httpClient = httpClient;
        this.tokenService = tokenService;
        this.userService = userService;
        }

        public async Task<List<TeacherViewModel>> GetTeachersAsync()
        {
            var result = await httpClient.GetAsync($"api/v1/Teacher", HttpCompletionOption.ResponseHeadersRead);
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
                JsonElement teachersElement;

                if (root.TryGetProperty("teachers", out teachersElement))
                {
                   
                    var teachers = JsonSerializer.Deserialize<List<TeacherViewModel>>(teachersElement.GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return teachers ?? new List<TeacherViewModel>();
                }
                else
                {
                    throw new JsonException("The JSON structure does not contain a 'teachers' key.");
                }
            }
        }


        public async Task<ApiResponse<UpdateTeacherViewModel>> UpdateTeacherAsync(UpdateTeacherViewModel teacherViewModel)
        {
            {
                var teacher = await userService.GetCurrentUser();
                var teacherData = await GetTeacherGuidByEmailAsync(teacher.UserName);

                Console.WriteLine("MIJ");
                teacherViewModel.TeacherId = teacherData.TeacherId;
                var result = await httpClient.PatchAsync($"api/v1/Teacher/{teacherData.TeacherId}",
               new StringContent(JsonSerializer.Serialize(teacherViewModel), Encoding.UTF8, "application/json"));
                result.EnsureSuccessStatusCode();
                Console.WriteLine(result);
                var response = await result.Content.ReadFromJsonAsync<ApiResponse<UpdateTeacherViewModel>>();
                response!.IsSuccess = result.IsSuccessStatusCode;
                Console.WriteLine(response);
                return response!;

            }
        }
        public async Task<TeacherViewModel> GetTeacherGuidByEmailAsync(string email)
        {
            var requestUri = $"api/v1/Teacher/getbyemail/{email}";
            var result = await httpClient.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead);

            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            Console.WriteLine("cont         " + content);

  
            JsonNode jsonNode = JsonNode.Parse(content);

            
            Guid teacherId = Guid.Parse(jsonNode["teacherId"].ToString());
            string firstName = jsonNode["firstName"].ToString();
            string lastName = jsonNode["lastName"].ToString();
            string phoneNumber = jsonNode["phoneNumber"]?.ToString();
            string description = jsonNode["description"]?.ToString();
            string profilePictureUrl = jsonNode["profilePictureUrl"]?.ToString();

           
            TeacherViewModel teacher = new TeacherViewModel
            {
                TeacherId = teacherId,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                Description = description,
                ProfilePictureUrl = profilePictureUrl
            };

           
            Console.WriteLine($"TeacherId: {teacher.TeacherId}");
            Console.WriteLine($"FirstName: {teacher.FirstName}");
            Console.WriteLine($"LastName: {teacher.LastName}");
            Console.WriteLine($"Email: {teacher.Email}");
            Console.WriteLine($"PhoneNumber: {teacher.PhoneNumber}");
            Console.WriteLine($"Description: {teacher.Description}");
            Console.WriteLine($"ProfilePictureUrl: {teacher.ProfilePictureUrl}");
            return teacher;
        }

        public async Task<TeacherViewModel> GetTeacherProfileAsync()
        {
            var userEmail = await userService.GetCurrentUser();
            var teacher = await GetTeacherGuidByEmailAsync(userEmail.UserName);

            return teacher;
        }

    }
}
