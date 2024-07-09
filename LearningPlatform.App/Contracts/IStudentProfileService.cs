
using LearningPlatform.App.Services.Responses;
using LearningPlatform.App.ViewModels;

namespace LearningPlatform.App.Contracts
{
    public interface IStudentProfileService
    {
       Task<List<StudentViewModel>> GetStudentAsync();

        Task<ApiResponse<ViewStudentDto>> CreateStudentAsync(StudentViewModel studentViewModel);

        Task<StudentViewModel> GetStudentGuidByEmailAsync(string email);

        Task<ApiResponse<UpdateStudentViewModel>> UpdateStudentAsync(UpdateStudentViewModel studentViewModel);

        Task<StudentViewModel> GetStudentProfileAsync();
    }
}