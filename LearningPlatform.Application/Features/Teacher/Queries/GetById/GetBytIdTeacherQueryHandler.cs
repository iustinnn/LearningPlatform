using AutoMapper;
using LearningPlatform.Application.Persistence;
using LearningPlatform.Domain.Common;
using MediatR;

using Microsoft.Extensions.Logging;

namespace LearningPlatform.Application.Features.Teacher.Queries.GetById
{
    public class  GetByIdTeacherHandler : IRequestHandler<GetByIdTeacherQuery, Result<TeacherDto>>
    {
        private readonly ITeacherRepository teacherRepository;
        private readonly IMapper mapper;
        private readonly ILogger<GetByIdTeacherHandler> _logger;
        //private readonly IUserRepository userRepository;

        public GetByIdTeacherHandler(ITeacherRepository teacherRepository, IMapper mapper, ILogger<GetByIdTeacherHandler> logger)
        {
            this.teacherRepository = teacherRepository;
            this.mapper = mapper;
            this._logger = logger;
           // this.userRepository = userRepository;
        }

        public async Task<Result<TeacherDto>> Handle(GetByIdTeacherQuery request, CancellationToken cancellationToken)
        {
            var teacher = await teacherRepository.FindByIdAsync(request.Id);
            Console.WriteLine("teacher: " + teacher);
            _logger.LogDebug("Teacher: {@Teacher}", teacher);
            if (teacher == null)
            {
                return Result<TeacherDto>.Failure($"No teacher found for id {request.Id}");
            }

            var resultingTeacher = new TeacherDto()
            {
                TeacherId = teacher.Value.TeacherId,
                FirstName = teacher.Value.FirstName,
                LastName = teacher.Value.LastName,
                Email = teacher.Value.Email,
                PhoneNumber = teacher.Value.PhoneNumber,
                Description = teacher.Value.Description,
                ProfilePictureUrl = teacher.Value.ProfilePictureUrl

            };

      
           // var user = await userRepository.GetByIdAsync(teacher.UserId);
           // teacherDto.Email = user.Email;
            return Result<TeacherDto>.Success(resultingTeacher);
        }


    }
}
