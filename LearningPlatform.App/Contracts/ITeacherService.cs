using LearningPlatform.App.Services.Responses;
using LearningPlatform.App.ViewModels;

namespace LearningPlatform.App.Contracts
{
    public interface ITeacherService
    {
        Task<List<TeacherViewModel>> GetTeachersAsync();

        Task<TeacherViewModel> GetTeacherGuidByEmailAsync(string email);

        Task<ApiResponse<UpdateTeacherViewModel>> UpdateTeacherAsync(UpdateTeacherViewModel teacherViewModel);

        Task<TeacherViewModel> GetTeacherProfileAsync();
    }
}
