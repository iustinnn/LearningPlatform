using Domain.AuthEntity;
using Infrastructure;
using LearningPlatform.Application.Contracts;
using LearningPlatform.Application.Contracts.Identity;
using LearningPlatform.Application.Persistence;
using LearningPlatform.Identity;
using LearningPlatform.Identity.Services;
using LearningPlatform.Infrastructure.Repositories;
using LearningPlatform.Infrastructure.Services;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LearningPlatform.Infrastructure
{
    public static class InfrastructureRegistrationDI
    {
        public static IServiceCollection AddInfrastrutureToDI(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<LearningPlatformSystemContext>(
                options =>
                options.UseNpgsql(
                    configuration.GetConnectionString
                    ("LearningPlatformConnection"),
                    builder =>
                    builder.MigrationsAssembly(
                        typeof(LearningPlatformSystemContext)
                        .Assembly.FullName)));

     
            services.AddScoped
                (typeof(IAsyncRepository<>),
                typeof(BaseRepository<>));
            services.AddScoped
                <IStudentRepository, StudentRepository>();
            services.AddScoped
                <ITeacherRepository, TeacherRepository>();
            services.AddScoped
                <ICourseRepository, CourseRepository>();
            services.AddScoped
                <IStudentCourseRepository, StudentCourseDataRepository>();
            services.AddScoped
                <ILessonRepository, LessonRepository>();
            services.AddScoped
                <IAzureBlobService, AzureBlobService>();
            services.AddScoped
                <IQuestionRepository, QuestionRepository>();
            services.AddScoped
                <IQuizRepository, QuizRepository>();
            




            return services;
        }
    }
}
