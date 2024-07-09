using LearningPlatform.App.Services.Responses;
using LearningPlatform.App.ViewModels;
using LearningPlatform.Domain.Entities;

namespace LearningPlatform.App.Contracts
{
    public interface ICourseService
    {

        Task<CourseDataViewModel> GetCourseByIdAsync(Guid courseId);
        Task<List<CourseDataViewModel>> GetAllCoursesAsync();
        //Task<List<CourseDataViewModel>> GetEnrolledCoursesAsync(Guid studentId);
        //Task UpdateCourseAsync(Course course);

        Task<List<CourseDataViewModel>> GetCoursesByTeacherId(Guid teacherId);

        Task<ApiResponse<ViewCourseDto>> CreateCourseAsync(CreateCourseViewModel courseDataViewModel);

        Task<ApiResponse<UpdateCourseViewModel>> UpdateCourseAsync(UpdateCourseViewModel courseViewModel);



    }
}
