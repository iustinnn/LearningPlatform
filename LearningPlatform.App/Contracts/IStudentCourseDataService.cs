using LearningPlatform.App.Services.Responses;
using LearningPlatform.App.ViewModels;

namespace LearningPlatform.App.Contracts
{
    public interface IStudentCourseDataService
    {
        Task<List<StudentViewModel>> GetStudentsByCourseId(Guid courseId);

        Task<List<CourseDataViewModel>> GetCourseIdsByStudentId(Guid studentId);

        Task <ApiResponse<StudentCourseEnrollViewModel>> CreateEnrollAsync(StudentCourseEnrollViewModel studentCourseEnrollViewModel);

    }
}
