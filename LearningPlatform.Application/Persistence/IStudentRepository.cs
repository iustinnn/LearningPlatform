using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.Entities;

namespace LearningPlatform.Application.Persistence
{
    public interface IStudentRepository : IAsyncRepository<Student>
    {
        public Task<Result<Student>> GetStudentByUserId(Guid id);

        public Task<Result<Student>> DoesStudentExist(Guid Id);

        public Task<Result<Student>> GetStudentByEmail(string email);


    }
}
