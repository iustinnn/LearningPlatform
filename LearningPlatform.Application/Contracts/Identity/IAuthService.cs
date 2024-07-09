using LearningPlatform.Application.Models.Identity;

namespace LearningPlatform.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<(int, string)> Registeration(RegistrationModel model, string role);

        Task<(int, string)> RegisterationStudent(RegistrationModel model, string role);

        Task<(int, string)> RegisterationTeacher(RegistrationModel model, string role);

        Task<(int, string)> Login(LoginModel model);

        Task<(int, string)> Logout();
    }
}
