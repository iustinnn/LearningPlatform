using LearningPlatform.App.ViewModels;

namespace LearningPlatform.App.Contracts
{
    public interface IUserService
    {
        Task<UserViewModel> GetCurrentUser();
    }
}
