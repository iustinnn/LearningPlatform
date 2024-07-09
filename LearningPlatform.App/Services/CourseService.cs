using LearningPlatform.App.Contracts;
using LearningPlatform.Domain.Entities;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Text;
using System.Drawing.Printing;
using LearningPlatform.App.ViewModels;
using LearningPlatform.Domain.Common;
using System.Net.Http;
using LearningPlatform.App.Services.Responses;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Nodes;




namespace LearningPlatform.App.Services
{
    public class CourseService  : ICourseService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenService tokenService;
        private readonly IUserService userService;
        private readonly ITeacherService teacherService;

        public CourseService(HttpClient httpClient, ITokenService tokenService, IUserService userService, ITeacherService teacherService)
        {
            _httpClient = httpClient;
            this.tokenService = tokenService;
            this.userService = userService;
            this.teacherService = teacherService;
        }


        public async Task<CourseDataViewModel> GetCourseByIdAsync(Guid courseId)
        {
            var response = await _httpClient.GetAsync($"api/v1/Course/{courseId}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var jsonNode = JsonNode.Parse(content);

            if (jsonNode == null)
            {
                throw new ApplicationException("Failed to parse course content.");
            }

            var courseDataViewModel = new CourseDataViewModel
            {
                CourseId = Guid.Parse(jsonNode["courseId"]?.ToString() ?? Guid.Empty.ToString()),
                Title = jsonNode["title"]?.ToString(),
                Description = jsonNode["description"]?.ToString(),
                StartDate = DateTime.Parse(jsonNode["startDate"]?.ToString() ?? DateTime.MinValue.ToString()),
                EndDate = DateTime.Parse(jsonNode["endDate"]?.ToString() ?? DateTime.MinValue.ToString()),
                VideoUrl = jsonNode["videoUrl"]?.ToString(),
                TeacherId = Guid.Parse(jsonNode["teacherId"]?.ToString() ?? Guid.Empty.ToString()),
                Thumbnail = jsonNode["thumbnail"]?.ToString()
            };

            return courseDataViewModel;
        }

        public async Task<List<CourseDataViewModel>> GetCoursesByTeacherId(Guid teacherId)
        {
            var response = await _httpClient.GetAsync($"api/v1/Course/teacher/{teacherId}");
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

               
                JsonElement coursesElement = root.GetProperty("courses");

                var courseGuids = new List<Guid>();

                foreach (JsonElement element in coursesElement.EnumerateArray())
                {
                    courseGuids.Add(Guid.Parse(element.GetString()));
                }

                var courseTasks = new List<Task<CourseDataViewModel>>();

                foreach (var courseGuid in courseGuids)
                {
                    courseTasks.Add(GetCourseById(courseGuid));
                }

                var courses = await Task.WhenAll(courseTasks);

                return courses.ToList();
            }
        }

        private async Task<CourseDataViewModel> GetCourseById(Guid courseId)
        {
            var response = await _httpClient.GetAsync($"api/v1/Course/{courseId}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            Console.WriteLine("course content: " + content);

            var course = JsonSerializer.Deserialize<CourseDataViewModel>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return course;
        }



        public async Task<List<CourseDataViewModel>> GetAllCoursesAsync()
        {
            var response = await _httpClient.GetAsync("api/v1/Course");
            response.EnsureSuccessStatusCode();
            Console.WriteLine(response);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine("JSON Response: " + content); // Log JSON response for debugging

            var courses = new List<CourseDataViewModel>();
            using (JsonDocument doc = JsonDocument.Parse(content))
            {
                JsonElement root = doc.RootElement;

               
                JsonElement coursesElement = root.GetProperty("courses");

                foreach (JsonElement element in coursesElement.EnumerateArray())
                {
                    var course = new CourseDataViewModel
                    {
                        CourseId = element.GetProperty("courseId").GetGuid(),
                        Title = element.GetProperty("title").GetString(),
                        Description = element.GetProperty("description").GetString(),
                        VideoUrl = element.GetProperty("videoUrl").GetString(),
                        TeacherId = element.GetProperty("teacherId").GetGuid(),
                        StartDate = element.GetProperty("startDate").GetDateTime(),
                        EndDate = element.GetProperty("endDate").GetDateTime(),
                        Thumbnail = element.GetProperty("thumbnail").GetString()
                    };
                    courses.Add(course);
                }
            }

            return courses;
        }

        private CourseDataViewModel MapToCourseDataViewModel(Course course)
        {
            return new CourseDataViewModel
            {
                CourseId = course.CourseId,
                Title = course.Title,
                Description = course.Description,
                VideoUrl = course.VideoUrl,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                TeacherId = course.TeacherId,
                Thumbnail = course.Thumbnail
            };
        }
        private List<CourseDataViewModel> MapToCourseDataViewModels(List<Course> courses)
        {
            var courseDataViewModels = new List<CourseDataViewModel>();
            foreach (var course in courses)
            {
                courseDataViewModels.Add(MapToCourseDataViewModel(course));
            }
            return courseDataViewModels;
        }


        public async Task<IEnumerable<Course>> GetEnrolledCoursesAsync(Guid studentId)
        {
            
            var response = await _httpClient.GetAsync($"api/v1/Student/{studentId}/courses");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<Course>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }


        public async Task<ApiResponse<ViewCourseDto>> CreateCourseAsync(CreateCourseViewModel createCourseViewModel)
        {
            /*
            _httpClient.DefaultRequestHeaders.Authorization
                   = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            */
            var user = await userService.GetCurrentUser();
            var teacher = await teacherService.GetTeacherGuidByEmailAsync(user.UserName);
            createCourseViewModel.TeacherId = teacher.TeacherId;
            Console.WriteLine(createCourseViewModel.Title);
            Console.WriteLine(createCourseViewModel.Description);
            Console.WriteLine(createCourseViewModel.VideoUrl);
            Console.WriteLine(createCourseViewModel.TeacherId);
            Console.WriteLine(createCourseViewModel.StartDate);
            Console.WriteLine(createCourseViewModel.EndDate);

            var result = await _httpClient.PostAsJsonAsync($"api/v1/Course", createCourseViewModel);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<ViewCourseDto>>();
            Console.WriteLine("raspuns"+ response);
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }
        /*
        public async Task<ApiResponse<UpdateStudentViewModel>> UpdateStudentAsync(UpdateStudentViewModel studentViewModel)
        {
            var student = await userService.GetCurrentUser();
            Console.WriteLine("user" + student.UserName);
            var studentGuid = await GetStudentGuidByEmailAsync(student.UserName);
            Console.WriteLine("haha" + studentGuid.FirstName);

            
            var email = (string)jObject["claims"]["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
            var userId = (string)jObject["claims"]["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
            var role = (string)jObject["claims"]["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
           
            Console.WriteLine($"Email: {email}, User ID: {userId}, Role: {role}");
           


              httpClient.DefaultRequestHeaders.Authorization
                  = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            
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
        */
        public async Task<ApiResponse<UpdateCourseViewModel>> UpdateCourseAsync(UpdateCourseViewModel courseViewModel)
        {
            var requestUri = $"api/v1/Course/{courseViewModel.CourseId}";
           /* httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
           */
            var result = await _httpClient.PatchAsync(requestUri,
                new StringContent(JsonSerializer.Serialize(courseViewModel), Encoding.UTF8, "application/json"));
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<UpdateCourseViewModel>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }




    }
}
