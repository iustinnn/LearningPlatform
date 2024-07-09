using LearningPlatform.App.Services.Responses;
using LearningPlatform.App.ViewModels;

namespace LearningPlatform.App.Contracts
{
    public interface ILessonService
    {
        Task<List<LessonDataViewModel>> GetLessonsByCourseId(Guid courseId);
        //Task<List<LessonDataViewModel>> GetLessonsAsync(Guid courseId);
        //ask<LessonDataViewModel> CreateLessonAsync(CreateLessonViewModel createLessonViewModel);
        //Task<ApiResponse<LessonDataViewModel>> UpdateLessonAsync(UpdateLessonViewModel updateLessonViewModel);
        //Task DeleteLessonAsync(Guid lessonId);

        Task<LessonDataViewModel> GetLessonById(Guid lessonId);

        Task<ApiResponse<LessonDataViewModel>> CreateLessonAsync(CreateLessonViewModel lessonDataViewModel);
    }
}
