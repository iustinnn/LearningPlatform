using LearningPlatform.App.Contracts;
using LearningPlatform.App.Services.Responses;
using LearningPlatform.App.ViewModels;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace LearningPlatform.App.Services
{
    public class StudentProfileService : IStudentProfileService
    {

        private const string RequestUri = "api/v1/patients";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;
        private readonly IUserService userService;

        public StudentProfileService(HttpClient httpClient, ITokenService tokenService, IUserService userService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
            this.userService = userService;
        }

        public async Task<List<StudentViewModel>> GetStudentAsync()
        {
           
            var result = await httpClient.GetAsync($"api/v1/Student", HttpCompletionOption.ResponseHeadersRead);
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
                JsonElement studentsElement;

                if (root.TryGetProperty("students", out studentsElement))
                {
                    
                    var students = JsonSerializer.Deserialize<List<StudentViewModel>>(studentsElement.GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return students ?? new List<StudentViewModel>();
                }
                else
                {
                    throw new JsonException("The JSON structure does not contain a 'students' key.");
                }
            }

        }

        public async Task<ApiResponse<ViewStudentDto>> CreateStudentAsync(StudentViewModel createStudentViewModel)
        {
           
            httpClient.DefaultRequestHeaders.Authorization
                   = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.PostAsJsonAsync($"api/v1/Student", createStudentViewModel);
            Console.WriteLine(result);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<ViewStudentDto>>();
            Console.WriteLine(response);
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }
        
        public async Task<ApiResponse<UpdateStudentViewModel>> UpdateStudentAsync(UpdateStudentViewModel studentViewModel)
        {
            var student = await userService.GetCurrentUser();
            Console.WriteLine("user"+student.UserName);
            var studentGuid = await GetStudentGuidByEmailAsync(student.UserName);
            Console.WriteLine("haha" + studentGuid.FirstName);

            /*
            var email = (string)jObject["claims"]["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
            var userId = (string)jObject["claims"]["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
            var role = (string)jObject["claims"]["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
           
            Console.WriteLine($"Email: {email}, User ID: {userId}, Role: {role}");
           */


            /*  httpClient.DefaultRequestHeaders.Authorization
                  = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            */
            studentViewModel.StudentId = studentGuid.StudentId;
            var result = await httpClient.PatchAsync($"api/v1/Student/{studentGuid.StudentId}",
           new StringContent(JsonSerializer.Serialize(studentViewModel), Encoding.UTF8, "application/json"));
            result.EnsureSuccessStatusCode();
            Console.WriteLine(result);
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<UpdateStudentViewModel>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            Console.WriteLine(response);
            return response!;
        }


        public async Task<StudentViewModel> GetStudentGuidByEmailAsync(string email)
        {
            var requestUri =
              $"api/v1/Student/getbyemail/{email}";
            var result = await httpClient.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead);
      
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
           
            JsonNode jsonNode = JsonNode.Parse(content);

            
            Guid studentId = Guid.Parse(jsonNode["studentId"].ToString());
            string firstName = jsonNode["firstName"].ToString();
            string lastName = jsonNode["lastName"].ToString();
            string phoneNumber = jsonNode["phoneNumber"]?.ToString();
            string description = jsonNode["description"]?.ToString();
            string profilePictureUrl = jsonNode["profilePictureUrl"]?.ToString();

            
            StudentViewModel student = new StudentViewModel
            {
                StudentId = studentId,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                Description = description,
                ProfilePictureUrl = profilePictureUrl
            };

            
            Console.WriteLine($"StudentId: {student.StudentId}");
            Console.WriteLine($"FirstName: {student.FirstName}");
            Console.WriteLine($"LastName: {student.LastName}");
            Console.WriteLine($"Email: {student.Email}");
            Console.WriteLine($"PhoneNumber: {student.PhoneNumber}");
            Console.WriteLine($"Description: {student.Description}");
            Console.WriteLine($"ProfilePictureUrl: {student.ProfilePictureUrl}");
            Console.WriteLine(student.PhoneNumber);
            return student;
        }


        public async Task<StudentViewModel> GetStudentProfileAsync()
        {
            var userEmail = await userService.GetCurrentUser();
            var student = await GetStudentGuidByEmailAsync(userEmail.UserName);
                /*
            Console.WriteLine("studentId" + studentId);
            var result = await httpClient.GetAsync($"api/v1/Student/{student.StudentId}", HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();
           
            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            Console.WriteLine("bbbb" + content);
                */

            return student;


        }
    }
}
