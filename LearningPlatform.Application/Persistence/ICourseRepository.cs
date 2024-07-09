using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Persistence
{
    public interface ICourseRepository : IAsyncRepository<Course>
    {
        Task<Result<List<Guid>>> GetCoursesByInstructorId(Guid instructorId);

        Task<Result<Course>> GetCoursesByCourseId(Guid courseId);

        Task<Result<Course>> DoesCourseExist(Guid Id);
      
    }
}
