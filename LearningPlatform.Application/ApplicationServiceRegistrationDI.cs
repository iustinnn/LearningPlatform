using LearningPlatform.Application.Features.Mapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LearningPlatform.Application
{
    public static class ApplicationServiceRegistrationDI
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR
                (
                    cfg => cfg.RegisterServicesFromAssembly(

                        Assembly.GetExecutingAssembly())
                );
            services.AddAutoMapper(typeof(StudentProfile).Assembly);
            services.AddAutoMapper(typeof(CourseProfile).Assembly);

        }
    }
}
