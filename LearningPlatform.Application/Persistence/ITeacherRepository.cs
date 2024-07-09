using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.Entities;
using LearningPlatform.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Application.Persistence
{
    public interface ITeacherRepository : IAsyncRepository<Teacher>
    {
        Task<Result<Teacher>> GetTeacherByUserId(Guid id);

        Task<Result<Teacher>> DoesTeacherExist(Guid Id);

        Task<Result<Teacher>> GetTeacherByEmail(string email);
    }
}
