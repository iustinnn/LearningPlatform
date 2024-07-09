using LearningPlatform.App.Contracts;
using LearningPlatform.App.Services.Responses;
using LearningPlatform.App.ViewModels;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text.Json;


namespace LearningPlatform.App.Services
{
    public class UserService : IUserService
    {
        private const string RequestUri = "api/v1/Authentication/currentuserinfo";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public UserService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<UserViewModel> GetCurrentUser()
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            var result = await httpClient.GetAsync(RequestUri, HttpCompletionOption.ResponseHeadersRead);
            var content = await result.Content.ReadAsStringAsync();

            if (!result.IsSuccessStatusCode)
            {
                return new UserViewModel
                {
                    IsAuthenticated = false,
                    UserName = string.Empty,
                    UserRole = string.Empty
                };
            }

            Console.WriteLine("KKK" + content);

            try
            {
                using (JsonDocument document = JsonDocument.Parse(content))
                {
                    var root = document.RootElement;

                    bool isAuthenticated = root.GetProperty("isAuthenticated").GetBoolean();
                    string userName = root.GetProperty("userName").GetString();

                    string userRole = string.Empty;
                    if (root.TryGetProperty("claims", out JsonElement claims) &&
                        claims.TryGetProperty("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", out JsonElement roleElement))
                    {
                        userRole = roleElement.GetString();
                    }

                    return new UserViewModel
                    {
                        IsAuthenticated = isAuthenticated,
                        UserName = userName,
                        UserRole = userRole
                    };
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error parsing JSON: {ex.Message}");
              
                return new UserViewModel
                {
                    IsAuthenticated = false,
                    UserName = string.Empty,
                    UserRole = string.Empty
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
               
                return new UserViewModel
                {
                    IsAuthenticated = false,
                    UserName = string.Empty,
                    UserRole = string.Empty
                };
            }
        }

    }
}