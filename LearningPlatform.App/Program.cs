using Blazored.LocalStorage;
using LearningPlatform.App.Auth;
using LearningPlatform.App.Contracts;
using LearningPlatform.App.Services;
using LearningPlatform.Application.Persistence;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<LearningPlatform.App.App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage(config =>
{
    config.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    config.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    config.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
    config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    config.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
    config.JsonSerializerOptions.WriteIndented = false;
});
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<CustomStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomStateProvider>());
builder.Services.AddHttpClient<IStudentProfileService, StudentProfileService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7020/");
});
builder.Services.AddHttpClient<ITeacherService, TeacherService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7020/");
});
builder.Services.AddHttpClient<IUserService, UserService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7020/");
});
builder.Services.AddHttpClient<ChatGPTService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7020/");
}

    );

builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7020/");
});

builder.Services.AddHttpClient<IStudentCourseDataService, StudentCourseDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7020/");
});

builder.Services.AddHttpClient<ICourseService, CourseService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7020/");
});
builder.Services.AddHttpClient<ILessonService, LessonService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7020/");
});
builder.Services.AddHttpClient<IAzureDataService, AzureDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7020/");
});
builder.Services.AddHttpClient<IQuizService, QuizService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7020/");
});
builder.Services.AddHttpClient<IQuestionService, QuestionService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7020/");
});


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
