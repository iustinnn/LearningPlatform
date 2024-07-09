using LearningPlatform.App.ViewModels;

namespace LearningPlatform.App.Contracts
{
    public interface IAuthenticationService
    {
        Task Login(LoginViewModel loginRequest);
        Task Register(RegisterStudentViewModel registerRequest);
        Task RegisterTeacher(RegisterTeacherViewModel registerTeacherRequest);
        Task Logout();
    }
}
