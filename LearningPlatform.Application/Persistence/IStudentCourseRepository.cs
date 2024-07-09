

using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.Entities;
using System.Numerics;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Persistence
{
    public interface IStudentCourseRepository : IAsyncRepository<StudentCourse>
    {
       Task<Result<List<Guid>>> GetStudentsByCourseId(Guid courseId);

       Task<Result<List<Guid>>> GetCourseIdsByStudentId(Guid studentId);

       Task<Result<StudentCourse>> FindByIdAsync(Guid courseId, Guid studentId);
       Task DeleteAsyncStudentCourse(StudentCourse studentCourse);





    }
}
